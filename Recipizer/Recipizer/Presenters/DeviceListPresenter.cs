using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Recipizer.Models;
using Recipizer.Activities;
using Android.Bluetooth;

namespace Recipizer.Presenters
{
    public class DeviceListPresenter : IPresenter
    {
        IRecipizerView view;
        Dictionary<string, BluetoothDevice> DeviceDict;

        public DeviceListPresenter(IRecipizerView _view)
        {
            this.view = _view;
            
        }

        public void onActivityResult(int requestCode, Result resultCode, Intent data)
        {
            throw new NotImplementedException();
        }

        public void onBackPressed()
        {
            throw new NotImplementedException();
        }

        public void onCreate()
        {
            throw new NotImplementedException();
        }

        public void onDestroy()
        {
            throw new NotImplementedException();
        }

        public void onPause()
        {
            throw new NotImplementedException();
        }

        public void onResume()
        {
            throw new NotImplementedException();
        }
    }
}