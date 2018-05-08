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
        public static readonly int CREATE_RECIPE = 1;
        public static SQLiteConnection Conn;
    }
}