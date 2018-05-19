using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Recipizer.Models;
using Recipizer.Presenters;

using System.IO;
using System.Runtime.CompilerServices;
using Android.Util;
using Java.Lang;
using Java.Util;
using Recipizer.Adapters;

namespace Recipizer.Activities
{
    [Activity(Label = "DeviceListActivity")]
    public class DeviceListActivity : Activity, IRecipizerView
    {
        //UI components
        Button btnUpdate;
        ProgressBar loadDiscovering;

        //Adapters
        BTDeviceAdapter deviceListAdapter;
        //ArrayAdapter<string> DeviceListAdapter;
        
        //Variable to connect to presenter
        DeviceListPresenter presenter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.DeviceList);

            //Instantiate the presenter
            presenter = new DeviceListPresenter(this);

            //Get UI components for global use.
            btnUpdate = FindViewById<Button>(Resource.Id.btnUpdate);
            loadDiscovering = FindViewById<ProgressBar>(Resource.Id.progressCircleDiscovering);

            //Get UI components for local use.
            ListView deviceList = FindViewById<ListView>(Resource.Id.listViewDeviceList);

            //TODO VERY IMPORANTE move this switch and all its fundtionality to the main screen
            Switch switchVisible = FindViewById<Switch>(Resource.Id.switchVisible);

            //Setup lists.
            //DeviceListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleExpandableListItem1);
            deviceListAdapter = new BTDeviceAdapter(this, presenter.devices);
            deviceList.Adapter = deviceListAdapter;

            //Setup Button Events.
            btnUpdate.Click += ( sender, e) => 
            {
                presenter.Update_Click();
            };

            deviceList.ItemClick += (sender, e) => 
            {
                presenter.DeviceList_OnItemClick(e.Position);
            };

            switchVisible.CheckedChange += (sender, e) =>
            {
                presenter.Visible_CheckedChanged(e.IsChecked);
            };

            //Bluetooth reciever is in a new class below this class
            RegisterReceiver(new DeviceListPresenter.BluetoothReciever(this, presenter), new IntentFilter(BluetoothDevice.ActionFound));
            RegisterReceiver(new DeviceListPresenter.BluetoothReciever(this, presenter), new IntentFilter(BluetoothAdapter.ActionDiscoveryStarted));
            RegisterReceiver(new DeviceListPresenter.BluetoothReciever(this, presenter), new IntentFilter(BluetoothAdapter.ActionDiscoveryFinished));

            //Call the presenters OnCreate Method.
            presenter.onCreate();
        }

        protected override void OnStop()
        {
            base.OnStop();
            presenter.onStop();
        }

        protected override void OnResume()
        {
            base.OnResume();
            presenter.onResume();
        }

        public void FinishView(Result result, Intent intent)
        {
            //TODO send thr right information
            SetResult(result, intent);
            Finish();
        }

        public void MakeToast(string text, ToastLength length)
        {
            Toast.MakeText(this, text, length).Show();
        }

        public void Navigate(int code, Intent data)
        {
            if (code == Constants.ENABLE_BLUETOOTH)
            {
                StartActivityForResult(data, Constants.ENABLE_BLUETOOTH);
            }
            else if (code == Constants.DISCOVERY_REQUEST)
            {
                StartActivityForResult(data, Constants.DISCOVERY_REQUEST);
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            presenter.onActivityResult(requestCode, resultCode, data);
        }

        public void ResetText() { }

        public void SetupView() { }

        public void UpdateView()
        {
            if (presenter.IsDiscovering)
            {
                btnUpdate.Enabled = false;
                loadDiscovering.Visibility = ViewStates.Visible;
            }
            else
            {
                btnUpdate.Enabled = true;
                loadDiscovering.Visibility = ViewStates.Invisible;
            }


            deviceListAdapter.NotifyDataSetChanged();
        }

        public void RequestPermission()
        {
            //TODO Remember to change magic int
            RequestPermissions(new string[] { Manifest.Permission.Bluetooth}, 1);
        }

        public void MakeDialog(int code)
        {
            //TODO Remove magic strings (Move them to strings XML)
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.SetTitle("Notice:")
                .SetMessage("Bluetooth is required to share items. \n Activate bluetooth?")
                .SetNegativeButton("No",  (sender, e) => { })
                .SetPositiveButton("Yes", (sender, e) => { presenter.StartDiscoveryProcess(); });
            builder.Show();
        }
    }
}

