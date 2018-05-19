using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Recipizer.Activities;

namespace Recipizer.Presenters
{
    class ShareBluetoothPresenter : IPresenter
    {
        //public List<string> ShareList;

        IRecipizerView view;

        public ShareBluetoothPresenter(IRecipizerView _view)
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