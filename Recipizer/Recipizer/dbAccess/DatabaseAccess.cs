using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using System.Data;
using System.IO;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Recipizer.dbAccess
{
    public class DatabaseAccess
    {
        public SQLiteConnection SQLiteDb { get; set; }

        public string DbPath { get; private set; }

        public DatabaseAccess()
        {
            String DbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Recipizer.db3");
            SQLiteConnection SQLiteDb = new SQLiteConnection(DbPath);
        }
    }
}