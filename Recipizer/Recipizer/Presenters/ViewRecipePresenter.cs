using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Recipizer.Activities;

namespace Recipizer.Presenters
{
    public class ViewRecipePresenter : IPresenter
    {
        IRecipizerView view;

        public ViewRecipePresenter(IRecipizerView _view)
        {
            this.view = _view;
        }
        
        public void onCreate() { }
        
        public void onPause() { }

        public void onDestroy() { }

        public void onResume() { }
        public void onBackPressed() { }

        public void onActivityResult(int requestCode, Result resultCode, Intent data) { }
    }
}