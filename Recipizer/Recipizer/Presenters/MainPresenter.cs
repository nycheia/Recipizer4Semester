using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Recipizer.Activities;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;
using SQLite;
using Recipizer.Models;

namespace Recipizer.Presenters
{
    public class MainPresenter : IPresenter
    {
        private IRecipizerView view;
        public MainPresenter(IRecipizerView _view)
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
            String DbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Recipizer.db3");
            Constants.Conn = new SQLiteConnection(DbPath);
            Constants.Conn.CreateTable<Recipe>();
            Constants.Conn.CreateTable<Ingredient>();
            //TODO DELETE DROP
            Constants.Conn.DropTable<MealPlan>();
            Constants.Conn.CreateTable<MealPlan>();
            Constants.Conn.CreateTable<MealDay>();

            //TODO DELETE THIS FOR TESTING
            MealPlan mp1 = new MealPlan();
            mp1.name = "1";
            MealPlan mp2 = new MealPlan();
            mp2.name = "2";
            MealPlan mp3 = new MealPlan();
            mp3.name = "3";

            Constants.Conn.Insert(mp1);
            Constants.Conn.Insert(mp2);
            Constants.Conn.Insert(mp3);
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