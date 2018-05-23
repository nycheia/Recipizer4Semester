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
using Recipizer.Models;
using Recipizer.Presenters;
using Recipizer.Adapters;

namespace Recipizer.Activities
{
    [Activity(Label = "ShareBluetoothActivity", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class ShareBluetoothActivity : Activity, IRecipizerView
    {

        //UI components
        ListView ShareList;
        TextView txtConnected;

        //Adapters
        ShareAdapter shareAdapter;

        //Variable to connect to presenter
        ShareBluetoothPresenter presenter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ShareBluetooth);

            //Instantiate the presenter
            presenter = new ShareBluetoothPresenter(this, Intent.GetIntExtra(Constants.RECIPE_ID, 0), Intent.GetStringExtra("type"));


            //Get UI components for global use.
            ShareList = FindViewById<ListView>(Resource.Id.listViewShareItems);
            txtConnected = FindViewById<TextView>(Resource.Id.textViewConnectedDevice);

            //Get UI components for local use.
            Button btnConnect = FindViewById<Button>(Resource.Id.btnConnect);
            Button btnAdd = FindViewById<Button>(Resource.Id.btnAddMore);
            Button btnClear = FindViewById<Button>(Resource.Id.btnClearItems);
            Button btnShare = FindViewById<Button>(Resource.Id.btnShareItems);


            //Setup lists.
            shareAdapter = new ShareAdapter(this, Bluetooth.ShareList);
            ShareList.Adapter = shareAdapter;

            //Setup Button Events.
            btnConnect.Click += (object sender, EventArgs e) => {
                presenter.Connect_Click();
            };

            btnAdd.Click += (object sender, EventArgs e) => {
                presenter.Add_Click();
            };

            btnClear.Click += (object sender, EventArgs e) => {
                presenter.Clear_Click();
            };

            btnShare.Click += (object sender, EventArgs e) => {
                presenter.Share_Click();
            };

            
            //Call the presenters OnCreate Method.
            presenter.onCreate();

        }

        public void FinishView(Result result, Intent intent)
        {
            SetResult(result, intent);
            Finish();
        }

        public void MakeToast(string text, ToastLength length) { }

        public void Navigate(int code, Intent data)
        {
            if (code == Constants.CONN_REQUEST)
            {
                StartActivityForResult(new Intent(this, typeof(DeviceListActivity)), Constants.CONN_REQUEST);
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            presenter.onResume();
        }

        public void ResetText() { }

        public void SetupView() { }

        public void UpdateView()
        {
            if (Bluetooth.IsConnected)
                txtConnected.Text = "Connected to " + Bluetooth.connectedDevice.Name;
            else
                txtConnected.Text = "Not connected";

            shareAdapter.NotifyDataSetChanged();
        }

        public void RequestPermission() { }

        public void MakeOKButtonDialog() { }

        public void MakeDialog(int code)
        {
            //TODO Remove magic strings (Move them to strings XML)
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.SetTitle("Warning:")
                .SetMessage("Already connected to " + Bluetooth.connectedDevice.Name +". \n Continue?")
                .SetNegativeButton("No", (sender, e) => { })
                .SetPositiveButton("Yes", (sender, e) => { presenter.NavToDeviceList(); });
            builder.Show();
        }
    }
}