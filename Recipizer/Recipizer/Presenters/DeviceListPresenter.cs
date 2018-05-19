using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

using Android.App;
using Android.Content;
using Recipizer.Models;
using Recipizer.Activities;
using Android.Bluetooth;
using Android.Widget;
using Java.Lang;
using Java.Util;
using System.IO;

namespace Recipizer.Presenters
{
    public class DeviceListPresenter : IPresenter
    {
        IRecipizerView view;
        public Dictionary<string, BluetoothDevice> DeviceDict;
        public BluetoothAdapter thisPhone;
        public bool IsDiscovering = false;
        

        public DeviceListPresenter(IRecipizerView _view)
        {
            this.view = _view;
            DeviceDict = new Dictionary<string, BluetoothDevice>();

            view.RequestPermission();

            thisPhone = BluetoothAdapter.DefaultAdapter;

            StartDiscoveryProcess();
        }
        
        public object StartDiscoveryProcess()
        {
            /*Enabling Bluetooth the nice way*/
            if (!thisPhone.IsEnabled)
            {
                string enableBT = BluetoothAdapter.ActionRequestEnable;

                view.Navigate(Constants.ENABLE_BLUETOOTH, new Intent(enableBT));
            }
            else if (!IsDiscovering)
            {
                thisPhone.StartDiscovery();
            }
            return true;
        }

        public void Update_Click()
        {
            StartDiscoveryProcess();
        }

        public void DeviceList_OnItemClick(int position)
        {
            
            //view.FinishView(Result.Ok, new Intent().PutExtra(Constants.POSITION, position));
        }

        public void Visible_CheckedChanged(bool _checked)
        {
            if (_checked)
            {
                //Making the device discoverable
                view.Navigate(Constants.DISCOVERY_REQUEST, new Intent(BluetoothAdapter.ActionRequestDiscoverable) );

                Bluetooth.StartAcceptThread(thisPhone);
            }
            else
            {
                Bluetooth.StopAcceptThread();
            }
        }

        public void onActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == Constants.ENABLE_BLUETOOTH)
            {
                if (resultCode == Result.Canceled)
                {
                    view.MakeDialog(Constants.TMP);
                }
                else if (resultCode == Result.Ok)
                {
                    StartDiscoveryProcess();
                }
            }
        }

        public void onBackPressed() { }

        public void onCreate() { }

        public void onDestroy() { }

        public void onPause() { }

        public void onStop()
        {
            thisPhone.CancelDiscovery();
        }

        public void onResume() { }

        public class BluetoothReciever : BroadcastReceiver
        {
            IRecipizerView view;
            DeviceListPresenter presenter;

            public BluetoothReciever(IRecipizerView _view, DeviceListPresenter _presenter)
            {
                //this.dict1 = _dict;
                this.view = _view;
                this.presenter = _presenter;
            }

            public override void OnReceive(Context context, Intent intent)
            {
                if (BluetoothAdapter.ActionDiscoveryStarted.Equals(intent.Action))
                {
                    //discovery started
                    presenter.IsDiscovering = true;
                    view.MakeToast("Discovery started", ToastLength.Short);
                }
                else if (BluetoothAdapter.ActionDiscoveryFinished.Equals(intent.Action))
                {
                    //discovery finished
                    presenter.IsDiscovering = false;
                    view.MakeToast("Discovery finished", ToastLength.Short);
                }
                else if (BluetoothDevice.ActionFound.Equals(intent.Action))
                {
                    //first lets get the name of the device
                    string remoteDeviceName = intent.GetStringExtra(BluetoothDevice.ExtraName);
                    BluetoothDevice remoteDevice = (BluetoothDevice)intent.GetParcelableExtra(BluetoothDevice.ExtraDevice);

                    if (remoteDevice != null)
                    {
                        if (remoteDeviceName == null)
                        {
                            remoteDeviceName = "Undefined";
                        }
                        else
                        {
                            bool addressExists = false;
                            int i = 1;

                            foreach (BluetoothDevice item in presenter.DeviceDict.Values)
                            {
                                if (item.Address.Equals(remoteDevice.Address))
                                    addressExists = true;
                            }

                            if (!addressExists)
                            {
                                while (presenter.DeviceDict.ContainsKey(remoteDeviceName))
                                {
                                    i++;
                                    remoteDeviceName += " (" + i + ")";
                                }

                                presenter.DeviceDict.Add(remoteDeviceName, remoteDevice);
                            }
                            
                        }

                        
                    }
                }
                view.UpdateView();
            }
        }
    }
}