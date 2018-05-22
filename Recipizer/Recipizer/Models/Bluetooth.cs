using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Java.IO;
using System.IO;
using Recipizer.Presenters;
using Newtonsoft.Json;

namespace Recipizer.Models
{
    class Bluetooth
    {

        //We can work with static here because the app should only have one instance going of each thread.
        //It could be done as a singleton for more security and less coupling
        private static AcceptThread at;
        private static ConnectThread ct;
        private static ConnectedThread cnt;

        private static List<Recipe> _ShareList;

        public static List<Recipe> ShareList
        {
            get
            {
                if (_ShareList == null)
                {
                    _ShareList = new List<Recipe>();
                }

                return _ShareList;
            }
        }

        public static bool IsConnected {
            get
            {
                if (cnt != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static BluetoothDevice connectedDevice;
        public static IPresenter presenter { get; set; }
        
        public static BtHandler btHandler;

        public static void Setup(IPresenter presenter)
        { 
            Bluetooth.presenter = presenter;
        }
        

        public static void StartAcceptThread(BluetoothAdapter btAdpt)
        {
            at = new AcceptThread(btAdpt);
            at.Start();
        }

        public static void StopAcceptThread()
        {
            if (at != null)
            {
                at.Cancel();
                at = null;
            }
        }

        public static void StartConnectThread(BluetoothDevice bd, BluetoothAdapter adapter)
        {
            ConnectThread ct = new ConnectThread(bd, adapter);
            ct.Start();
        }

        public static void StopConnectThread()
        {
            if (ct != null)
            {
                ct.Cancel();
                ct = null;
            }
        }

        public static void StartConnectedThread(BluetoothSocket socket)
        {
            cnt = new ConnectedThread(socket);
            presenter.onActivityResult(Constants.CONN_OK, Result.Ok, new Intent().PutExtra("msg", "Connected to " + socket.RemoteDevice.Name));
            Bluetooth.connectedDevice = socket.RemoteDevice;
            cnt.Start();
        }

        public static void StopConnectedThread()
        {
            if (cnt != null)
            {
                cnt.Cancel();
                cnt = null;
            }
        }

        public static void Write(string message)
        {
            if (cnt != null)
            {
                var bytes = Encoding.ASCII.GetBytes(message);
                cnt.Write(bytes);
            }
        }

        //The thread parent class is from the Java.Lang Library, the .Net Thread class is a sealed class
        class AcceptThread : Thread
        {
            private readonly BluetoothServerSocket btServerSocket;
            private readonly BluetoothAdapter btAdpt;
            private bool done = false;


            public AcceptThread(BluetoothAdapter _btAdpt)
            {
                BluetoothServerSocket bss = null;
                btAdpt = _btAdpt;

                try
                {
                    bss = btAdpt.ListenUsingRfcommWithServiceRecord(Constants.APP_NAME, Constants.APP_UUID);

                }
                catch (Java.IO.IOException e)
                {
                    e.PrintStackTrace();
                }

                btServerSocket = bss;
            }

            public override void Run()
            {
                BluetoothSocket btSocket;

                while (!done)
                {
                    try
                    {
                        btSocket = btServerSocket.Accept();
                    }
                    catch (Java.IO.IOException e)
                    {
                        e.PrintStackTrace();

                        done = true;
                        break;
                    }

                    if (btSocket != null)
                    {
                        try
                        {
                            StartConnectedThread(btSocket);
                            btServerSocket.Close();
                            done = true;
                            break;
                        }
                        catch (Java.IO.IOException e)
                        {
                            e.PrintStackTrace();

                            done = true;
                            break;
                        }
                    }
                }

                btServerSocket.Close();

            }

            public void Cancel()
            {
                try
                {
                    done = true;
                }
                catch (Java.IO.IOException e)
                {
                    e.PrintStackTrace();
                }
            }
        }

        class ConnectThread : Thread
        {
            private readonly BluetoothSocket btSocket;
            private readonly BluetoothDevice btDevice;
            private readonly BluetoothAdapter thisPhone;

            public ConnectThread(BluetoothDevice device, BluetoothAdapter adapter)
            {
                BluetoothSocket tmp = null;

                thisPhone = adapter;
                btDevice = device;

                try
                {
                    tmp = device.CreateRfcommSocketToServiceRecord(Constants.APP_UUID);
                }
                catch (Java.IO.IOException e)
                {
                    e.PrintStackTrace();
                }

                btSocket = tmp;
            }

            public override void Run()
            {
                // Cancel discovery because it otherwise slows down the connection.
                thisPhone.CancelDiscovery();

                try
                {
                    // Connect to the remote device through the socket. This call blocks
                    // until it succeeds or throws an exception.
                    btSocket.Connect();
                }
                catch (Java.IO.IOException connectException)
                {
                    // Unable to connect; close the socket and return.
                    connectException.PrintStackTrace();
                    try
                    {
                        btSocket.Close();
                    }
                    catch (Java.IO.IOException closeException)
                    {
                        closeException.PrintStackTrace();
                    }

                    return;
                }
                // The connection attempt succeeded. Perform work associated with
                // the connection in a separate thread.

                StartConnectedThread(btSocket);
            }

            // Closes the client socket and causes the thread to finish.
            public void Cancel()
            {
                try
                {
                    btSocket.Close();
                }
                catch (Java.IO.IOException e)
                {
                    e.PrintStackTrace();
                }
            }
        }

        class ConnectedThread : Thread
        {
            private readonly BluetoothSocket btSocket;
            private readonly Stream btInStream;
            private readonly Stream btOutStream;
            private Handler btHandler;
            private bool done = false;


            public ConnectedThread(BluetoothSocket socket)
            {
                if (Bluetooth.btHandler == null)
                {
                    done = true;
                }
                else
                {
                    this.btHandler = Bluetooth.btHandler;
                }

                btSocket = socket;
                Stream tmpIn = null;
                Stream tmpOut = null;

                //getting the streams
                
                try
                {
                    tmpIn = socket.InputStream;
                    tmpOut = socket.OutputStream;
                }
                catch (Java.IO.IOException e)
                {
                    e.PrintStackTrace();
                }
               
                btInStream = tmpIn;
                btOutStream = tmpOut;
            }

            public override void Run()
            {
                byte[] btBuffer = new byte[1024]; // btBuffer store for the stream
                int bytes;

                // Keep listening to the InputStream while connected
                while (!done)
                {
                    
                    try
                    {
                        //Read from the stream
                        bytes = btInStream.Read(btBuffer,  0, btBuffer.Length);
                        string readMessage = ""+bytes;

                        Java.Lang.String rMessage = new Java.Lang.String(btBuffer, 0, bytes);
                        readMessage = (string)rMessage;

                        if (readMessage != null)
                        {
                            RecieverThread rct = new RecieverThread(readMessage);
                            rct.Start();
                        }
                        

                        //Send the message to the ui
                        btHandler.ObtainMessage(Constants.MESSAGE_READ, bytes, -1, btBuffer).SendToTarget();
                        
                        /*
                    BufferedReader br = null;
                    StringBuilder sb = new StringBuilder();

                    String line;
                    try {

                        br = new BufferedReader(new InputStreamReader(in));

                        while ((line = br.readLine()) != null) {
                            sb.append(line);
                        }

                    } catch (IOException e) {
                        e.printStackTrace();
                    } finally {
                        if (br != null) {
                            try {
                                br.close();
                            } catch (IOException e) {
                                e.printStackTrace();
                            }
                        }
                    }*/

                    }
                    catch (Java.IO.IOException e)
                    {
                        
                        e.PrintStackTrace();
                        break;
                    }
                }
            }

            public void Write(byte[] bytes)
            {
                try
                {
                    btOutStream.Write(bytes, 0, bytes.Length);

                    btHandler.ObtainMessage(Constants.MESSAGE_WRITE, -1, -1, bytes).SendToTarget();

                    //Message writtenMsg = btHandler.ObtainMessage(Constants.MESSAGE_WRITE, -1, -1, bytes);
                    //writtenMsg.SendToTarget();

                }
                catch (Java.IO.IOException e)
                {
                    e.PrintStackTrace();

                    /*
                    // Send a failure message back to the activity.
                    Message writeErrorMsg = btHandler.obtainMessage(MESSAGE_TOAST);
                    Bundle bundle = new Bundle();
                    bundle.putString("toast", "Couldn't send data to the other device");
                    writeErrorMsg.setData(bundle);
                    btHandler.sendMessage(writeErrorMsg);*/

                }
            }

            public void Cancel()
            {
                try
                {
                    btSocket.Close();
                }
                catch (Java.IO.IOException e)
                {
                    e.PrintStackTrace();
                }
            }
        }

        class RecieverThread : Thread
        {
            string JsonMsg;

            public RecieverThread(string JsonMsg)
            {
                this.JsonMsg = JsonMsg;
            }

            public override void Run()
            {
                List<Recipe> tmpList = JsonConvert.DeserializeObject<List<Recipe>>(JsonMsg);

                foreach (Recipe item in tmpList)
                {
                    try
                    {
                        Constants.Conn.Insert(item);
                    }
                    catch (System.Exception)
                    {
                    }
                }

            }
        }

        public class BtHandler : Handler
        {
            public override void HandleMessage(Message msg)
            {
                switch (msg.What)
                {
                    case Constants.MESSAGE_WRITE:
                        byte[] writeBuf = (byte[])msg.Obj;
                        string writeMessage = Encoding.ASCII.GetString(writeBuf);
                        // construct a string from the buffer
                        //string writeMessage = new string(writeBuf);
                        //Toast.makeText(this, "Write Message", Toast.LENGTH_SHORT).show();
                        break;
                    case Constants.MESSAGE_READ:
                        byte[] readBuf = (byte[])msg.Obj;
                        // construct a string from the valid bytes in the buffer
                        string readMessage = Encoding.ASCII.GetString(readBuf);
                        //string readMessage = new string(readBuf, 0, msg.Arg1);
                        //Toast.makeText(this, "Read Message", Toast.LENGTH_SHORT).show();
                        break;
                }
            }
        }
    }
}