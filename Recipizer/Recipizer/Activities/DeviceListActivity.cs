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

namespace Recipizer.Activities
{
    [Activity(Label = "DeviceListActivity")]
    public class DeviceListActivity : Activity, IRecipizerView
    {
        //UI components
        ListView DeviceList;

        //Adapters
        BluetoothAdapter thisPhone;

        //Variable to connect to presenter
        DeviceListPresenter presenter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CreateRecipe);

            //Instantiate the presenter
            presenter = new DeviceListPresenter(this);

            //Get UI components for global use.
            DeviceList = FindViewById<ListView>(Resource.Id.listViewDeviceList);

            //Get UI components for local use.
            Button btnUpdate = FindViewById<Button>(Resource.Id.btnUpdate);
            Switch switchVisible = FindViewById<Switch>(Resource.Id.switchVisible);

            //Setup lists.


            //Setup Button Events.
            /*btnAddIngredient.Click += (object sender, EventArgs e) => {
            };

            btnCreateRecipe.Click += (object sender, EventArgs e) => {
            };*/

            //Call the presenters OnCreate Method.
            presenter.onCreate();



            //TODO Refactor this
            /*********************************************************************************/

            BluetoothAdapter bluetooth = BluetoothAdapter.DefaultAdapter;

            /*Enabling Bluetooth the nice way*/
            if (!bluetooth.IsEnabled)
            {
                String enableBT = BluetoothAdapter.ActionRequestEnable;

                StartActivityForResult(new Intent(enableBT), Constants.ENABLE_BLUETOOTH);
            }
            else
            {
                //TODO if you get here we don goofed
            }

            thisPhone = bluetooth;
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

        public void RequestPermission()
        {
            /* Remember to change magic int */
            //RequestPermissions(new string[] { Manifest.Permission.AccessCoarseLocation}, 1);
        }
    }
}