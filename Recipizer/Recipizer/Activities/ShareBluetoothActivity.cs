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
using Recipizer.Presenters;

namespace Recipizer.Activities
{
    [Activity(Label = "ShareBluetoothActivity")]
    public class ShareBluetoothActivity : Activity, IRecipizerView
    {

        //UI components
        ListView ShareList;

        //Adapters
        ArrayAdapter<string> ShareAdapter;

        //Variable to connect to presenter
        ShareBluetoothPresenter presenter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ShareBluetooth);

            //Instantiate the presenter
            presenter = new ShareBluetoothPresenter(this);

            //Get UI components for global use.
            ShareList = FindViewById<ListView>(Resource.Id.listViewShareItems);

            //Get UI components for local use.
            Button btnConnect = FindViewById<Button>(Resource.Id.btnConnect);
            Button btnAdd = FindViewById<Button>(Resource.Id.btnAddMore);
            Button btnClear = FindViewById<Button>(Resource.Id.btnClearItems);
            Button btnShare = FindViewById<Button>(Resource.Id.btnShareItems);

            //Instantiate Spinner (Dropdown).


            //Setup lists.
            ShareAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1);
            ShareList.Adapter = ShareAdapter;

            //Setup Button Events.
            btnConnect.Click += (object sender, EventArgs e) => {
                Toast.MakeText(this, "Connect Button Clicked", ToastLength.Short);
            };

            btnAdd.Click += (object sender, EventArgs e) => {
                Toast.MakeText(this, "Add More Button Clicked", ToastLength.Short);
            };

            btnClear.Click += (object sender, EventArgs e) => {
                Toast.MakeText(this, "Clear Button Clicked", ToastLength.Short);
            };

            btnShare.Click += (object sender, EventArgs e) => {
                Toast.MakeText(this, "Share Button Clicked", ToastLength.Short);
            };

            //Call the presenters OnCreate Method.
            presenter.onCreate();

        }

        public void FinishView(Result result, Intent intent)
        {
            throw new NotImplementedException();
        }

        public void MakeToast(string text, ToastLength length)
        {
            throw new NotImplementedException();
        }

        public void Navigate(int code, Intent data)
        {
            throw new NotImplementedException();
        }

        public void ResetText()
        {
            throw new NotImplementedException();
        }

        public void SetupView()
        {
            throw new NotImplementedException();
        }

        public void UpdateView()
        {
            throw new NotImplementedException();
        }
    }
}