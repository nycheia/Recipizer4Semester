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

namespace Recipizer.Temp
{
    public class tempContainer
    {
        //TODO All of this should be deleted and replaced by database stuff
        private static tempContainer _instance;

        public static tempContainer instance
        {
            get {
                if (_instance == null)
                {
                    _instance = new tempContainer();
                }

                return _instance;
            }
            set { _instance = value; }
        }

        public List<Models.Recipe> RecipieContainer;

        public tempContainer()
        {
            RecipieContainer = new List<Models.Recipe>();
        }

    }
}