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
using Recipizer.Activities;
using Recipizer.Models;

namespace Recipizer.Presenters
{
    public class MealPlanPresenter : IPresenter
    {
        IRecipizerView view;

        public List<string> mealPlanList; 
        
        public MealPlanPresenter(IRecipizerView view)
        {
            this.view = view;
            mealPlanList = new List<string>();
            LoadMealPlanFromStorage();
        }

        private void LoadMealPlanFromStorage()
        {
            foreach (MealPlan mp in Constants.Conn.Table<MealPlan>().ToList())
            {
                mealPlanList.Add(mp.name);
            }
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
            LoadMealPlanFromStorage();
            view.UpdateView();
        }

        public void onDestroy()
        {
            throw new NotImplementedException();
        }

        public void onPause()
        {
            throw new NotImplementedException();
        }

        public void mealPlan_OnClick(int position)
        {
            view.MakeToast("Navigating to: " + mealPlanList[position], ToastLength.Short);

            //TODO lav lige noget intents, når enkelt meal day activity er lavet.
            int id = (Constants.Conn.Get<MealPlan>(position+1).id);

            view.Navigate(1, new Intent().PutExtra("mealPlanId", id));
        }

        public void onResume()
        {
            LoadMealPlanFromStorage();
            view.UpdateView();
        }
    }
}