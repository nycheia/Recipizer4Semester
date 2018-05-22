using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;


namespace Recipizer.Presenters
{
    public interface IPresenter
    {
        void onCreate();
        void onPause();
        void onResume();
        void onDestroy();
        void onBackPressed();
        void onActivityResult(int requestCode, Result resultCode, Intent data);

        //void RecieveMessage(int code, string msg);
    }
}