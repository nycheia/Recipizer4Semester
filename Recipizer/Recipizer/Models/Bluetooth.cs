using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Recipizer.Models
{
    class Bluetooth
    {
        public Bluetooth(Activity ac)
        {
            activity = ac;

            btList = new HashMap<>();

            activity.requestPermissions(new String[] { Manifest.permission.ACCESS_COARSE_LOCATION }, 1);
            //TODO do something if we dont get or have permission.



            BluetoothAdapter bluetooth = BluetoothAdapter.getDefaultAdapter();

            /*Enabling Bluetooth the nice way*/
            if (!bluetooth.isEnabled())
            {
                String enableBT = BluetoothAdapter.ACTION_REQUEST_ENABLE;
                activity.startActivityForResult(new Intent(enableBT), ENABLE_BLUETOOTH);
            }


            if (bluetooth.isEnabled())
            {
                address = bluetooth.getAddress();
                name = bluetooth.getName();
            }
            else
            {
                //TODO if you get here we don goofed
            }

            thisPhone = bluetooth;
        }


        public void makeDiscoverable()
        {
            //Making your device discoverable
            String discoverBT = BluetoothAdapter.ACTION_REQUEST_DISCOVERABLE;

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

        BroadcastReceiver handleDiscoveryResult = new BroadcastReceiver(){
            @Override
            public void onReceive(Context context, Intent intent)
        {
            if (BluetoothAdapter.ACTION_DISCOVERY_STARTED.equals(intent.getAction()))
            {
                //discovery started
                discoverStatus = status.STARTED;
                (Toast.makeText(context, "Discovery started", Toast.LENGTH_SHORT)).show();
            }
            else if (BluetoothAdapter.ACTION_DISCOVERY_FINISHED.equals(intent.getAction()))
            {
                //discovery finished
                discoverStatus = status.STOPPED;
                (Toast.makeText(context, "Discovery finished", Toast.LENGTH_SHORT)).show();
                //startSocketConnection(); //connects to a specific device
            }
            else if (BluetoothDevice.ACTION_FOUND.equals(intent.getAction()))
            {
                //first lets get the name of the device
                String remoteDeviceName = intent.getStringExtra(BluetoothDevice.EXTRA_NAME);
                BluetoothDevice remoteDevice = intent.getParcelableExtra(BluetoothDevice.EXTRA_DEVICE);

                if (remoteDevice != null)
                {
                    if (remoteDeviceName == null)
                    {
                        remoteDeviceName = "Undefined";
                    }

                    btList.put(remoteDeviceName, remoteDevice);
                    Log.d("TAG", "Discovered: " + remoteDeviceName);

                    discoverStatus = status.FOUND;
                }
            }
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
    }
    }
}