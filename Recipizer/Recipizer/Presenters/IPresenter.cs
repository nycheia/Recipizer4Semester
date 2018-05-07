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

namespace Recipizer.Presenters
{
    public interface IPresenter
    {
        void onCreate();
        void onPause();
        void onResume();
        void onDestroy();
        void onBackPressed();
    }
}