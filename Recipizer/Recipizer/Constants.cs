using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util;

namespace Recipizer
{
    public class Constants
    {
        public static SQLiteConnection Conn;

        public const int TMP = 0;
        public const int NEW_RECIPE = 1;
        public const int SHOW_RECIPE = 2;
        public const int EDIT_RECIPE = 3;
        public const int SHARE = 4;
        public const int ENABLE_BLUETOOTH = 5;
        public const int DISCOVERY_REQUEST = 6;
        public const int MESSAGE_READ = 7;
        public const int MESSAGE_WRITE = 8;

        public const string RECIPE_ID = "recipeid";
        public const string POSITION = "position";
        public const string APP_NAME = "Recipizer";
        
        //TODO add our own UUID
        public static UUID APP_UUID = UUID.FromString("8ce255c0-200a-11e0-ac64-0800200c9a66");


    }
}