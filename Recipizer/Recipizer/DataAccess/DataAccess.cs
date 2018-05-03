using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using SQLite;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Recipizer.DataAccess
{
    public class DataAccess
    {
        private String dbPath;
        public DataAccess()
        {
            string dbPath = Path.Combine(
                System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Recipizer.db3");
        }

        public SQLiteConnection getDB()
        {
            var db = new SQLiteConnection(dbPath);
            return db;
        }

    }
}