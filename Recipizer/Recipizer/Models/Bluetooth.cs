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

namespace Recipizer.Models
{
    class Bluetooth
    {

        //We can work with static here because the app should only have one instance going of each thread.
        private static AcceptThread at;
        private static ConnectThread ct;
        private static ConnectedThread cnt;

        public static BtHandler BTHandler;

        private delegate void Callback();
        public static DeviceListPresenter dlp;

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
            private BtHandler btHandler;


            public ConnectedThread(BluetoothSocket socket)
            {
                btHandler = Bluetooth.BTHandler;
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
                while (true)
                {
                    
                    try
                    {
                        //Read from the stream
                        //bytes = btInStream.Read(btBuffer);
                        bytes = btInStream.Read(btBuffer,  0, btBuffer.Length);

                        //Send the message to the ui
                        btHandler.ObtainMessage(Constants.MESSAGE_READ, bytes, -1, btBuffer).SendToTarget();
                        //Message readMsg = btHandler.ObtainMessage(Constants.MESSAGE_READ, bytes, -1, btBuffer);

                        //readMsg.SendToTarget();
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
    }
}

//***************************************************************************************/

    /*
    public void makeDiscoverable()
    {
        //Making your device discoverable
        String discoverBT = BluetoothAdapter.ActionRequestDiscoverable;


        activity.startActivityForResult(new Intent(discoverBT), DISCOVERY_REQUEST);

        at = new AcceptThread();

        at.start();
    }

    public void cancelDiscoverable()
    {
        if (at != null)
        {
            at.cancel();
            at = null;
        }
    }

    protected void onActivityResult(int requestCode, int resultCode, Intent data)
    {
        if (requestCode == ENABLE_BLUETOOTH)
        {
            if (resultCode == RESULT_OK)
            {
                //initBluetooth();
                //method which handles the initialization of BT, we have to code it
            }
        }
        else if (requestCode == DISCOVERY_REQUEST)
        {
            if (resultCode == RESULT_CANCELLED)
            {
                Log.d("TAG", "Discovery aborted by user");
                //here we are logging errors in  discovery. We could also call a method in case of success
            }
        }
    }

    public HashMap<String, BluetoothDevice> getDevices()
    {
        return btList;
    }

    public Object startDiscoveryProcess()
    {
        activity.registerReceiver(handleDiscoveryResult, new IntentFilter(BluetoothDevice.ACTION_FOUND));
        activity.registerReceiver(handleDiscoveryResult, new IntentFilter(BluetoothAdapter.ACTION_DISCOVERY_STARTED));
        activity.registerReceiver(handleDiscoveryResult, new IntentFilter(BluetoothAdapter.ACTION_DISCOVERY_FINISHED));
        thisPhone.startDiscovery();
        return true;
    }

    public void connectionStuff(BluetoothDevice bd)
    {

        ConnectThread ct = new ConnectThread(bd);
        ct.start();
    }

};

public void manageMyConnectedSocket(BluetoothSocket socket)
{
    ct = new ConnectedThread(socket);
    ct.start();
}
}

private class AcceptThread extends Thread
    {
        private final BluetoothServerSocket btServerSocket;
        private boolean done = false;

    public AcceptThread()
    {
        BluetoothServerSocket bss = null;
        try
        {
            bss = thisPhone.listenUsingRfcommWithServiceRecord(APP_NAME, MY_UUID);

        }
        catch (IOException e)
        {
            e.printStackTrace();
        }
        btServerSocket = bss;
    }

    @Override
        public void run()
    {
        BluetoothSocket btSocket;

        while (!done)
        {
            try
            {
                btSocket = btServerSocket.accept();
            }
            catch (IOException e)
            {
                e.printStackTrace();
                done = true;
                break;
            }

            if (btSocket != null)
            {
                Log.e("TAG", " VI er her!!");
                try
                {
                    btServerSocket.close();
                    done = true;
                    break;
                }
                catch (IOException e)
                {
                    e.printStackTrace();
                    done = true;
                    break;
                }
            }
        }
    }

    public void cancel()
    {
        try
        {
            done = true;
            btServerSocket.close();
        }
        catch (IOException e)
        {
            Log.e("TAG", "Could not close the connect socket", e);
        }
    }
}
}

class ConnectThread extends Thread
{
    private final BluetoothSocket btSocket;
    private final BluetoothDevice btDevice;

    public ConnectThread(BluetoothDevice device)
{
BluetoothSocket tmp = null;
btDevice = device;

try
{
    tmp = device.createRfcommSocketToServiceRecord(MY_UUID);
}
catch (IOException e)
{
    Log.e("TAG", "Socket's create() method failed", e);
}
btSocket = tmp;
}

public void run()
{
// Cancel discovery because it otherwise slows down the connection.
thisPhone.cancelDiscovery();

try
{
    // Connect to the remote device through the socket. This call blocks
    // until it succeeds or throws an exception.
    btSocket.connect();
}
catch (IOException connectException)
{
    // Unable to connect; close the socket and return.
    connectException.printStackTrace();
    try
    {
        btSocket.close();
    }
    catch (IOException closeException)
    {
        Log.e("TAG", "Could not close the client socket", closeException);
    }

    return;
}
// The connection attempt succeeded. Perform work associated with
// the connection in a separate thread.
manageMyConnectedSocket(btSocket);
}

// Closes the client socket and causes the thread to finish.
public void cancel()
{
try
{
    btSocket.close();
}
catch (IOException e)
{
    Log.e("TAG", "Could not close the client socket", e);
}
}
}

class ConnectedThread extends Thread
{
    private final BluetoothSocket btSocket;
    private final InputStream btInStream;
    private final OutputStream btOutStream;


    public ConnectedThread(BluetoothSocket socket)
{
btSocket = socket;
InputStream tmpIn = null;
OutputStream tmpOut = null;

//getting the streams, as variables are final we use temps.

try
{
    tmpIn = socket.getInputStream();
}
catch (IOException e)
{
    e.printStackTrace();
}

try
{
    tmpOut = socket.getOutputStream();
}
catch (IOException e)
{
    e.printStackTrace();
}

btInStream = tmpIn;
btOutStream = tmpOut;
}

public void run()
{
byte[] btBuffer = new byte[1024]; // btBuffer store for the stream
int numBytes;

while (true)
{
    try
    {
        numBytes = btInStream.read(btBuffer);

        Message readMsg = btHandler.obtainMessage(MESSAGE_READ, numBytes, -1, btBuffer);

        readMsg.sendToTarget();
    }
    catch (IOException e)
    {
        e.printStackTrace();
        Log.d("TAG", "run: ");
        break;
    }
}
}

public void write(byte[] bytes)
{
try
{
    btOutStream.write(bytes);

    Message writtenMsg = btHandler.obtainMessage(MESSAGE_WRITE, -1, -1, bytes);
    writtenMsg.sendToTarget();

}
catch (IOException e)
{
    Log.e("TAG", "Error occurred when sending data", e);

    // Send a failure message back to the activity.
    Message writeErrorMsg = btHandler.obtainMessage(MESSAGE_TOAST);
    Bundle bundle = new Bundle();
    bundle.putString("toast", "Couldn't send data to the other device");
    writeErrorMsg.setData(bundle);
    btHandler.sendMessage(writeErrorMsg);

}
}

public void cancel()
{
try
{
    btSocket.close();
}
catch (IOException e)
{
    Log.e("TAG", "Could not close the connect socket", e);
}
}
}*/