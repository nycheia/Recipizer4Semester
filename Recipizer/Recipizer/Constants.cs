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

namespace Recipizer
{
    public class Constants
    {
        public static SQLiteConnection Conn;

        public const int NEW_RECIPE = 1;
        public const int SHOW_RECIPE = 2;
        public const int EDIT_RECIPE = 3;

        public const string RECIPE_ID = "recipeid";

    }
}