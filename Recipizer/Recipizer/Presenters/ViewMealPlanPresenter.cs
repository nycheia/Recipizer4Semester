using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Recipizer.Models;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Recipizer.Activities;

namespace Recipizer.Presenters
{
    public class ViewMealPlanPresenter : IPresenter
    {
        IRecipizerView view;
        public Dictionary<string, MealDay> mealDays;
        public List<Recipe> recipeFill;
        public Dictionary<string, List<Recipe>> recipes;

        public ViewMealPlanPresenter(IRecipizerView view)
        {
            this.view = view;
            this.mealDays = new Dictionary<string, MealDay>();
            this.recipes = new Dictionary<string, List<Recipe>>();
            this.recipeFill = new List<Recipe>();
        }

        public void onCreate()
        {
            throw new NotImplementedException();
        }
        
        public void onActivityResult(int requestCode, Result resultCode, Intent data)
        {
            throw new NotImplementedException();
        }

        public void onBackPressed()
        {
            throw new NotImplementedException();
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

        public void PopulateMealDaysFromStorage(int mealPlanId)
        {
            MealPlan mp = Constants.Conn.Get<MealPlan>(mealPlanId);
            var query = (from md in Constants.Conn.Table<MealDay>() where md.mealPlanId == mealPlanId select md).ToList();
            foreach (MealDay item in query)
            {
                mealDays.Add(item.day.ToString(), item);
                var recipeQuery = from mdr in Constants.Conn.Table<MealDayRecipe>() where mdr.mealDayId == item.id select mdr;
                foreach (MealDayRecipe i in recipeQuery)
                {
                    recipeFill.Add(Constants.Conn.Get<Recipe>(i.recipeId));
                }
                recipes.Add("" + item.day, recipeFill);
            }
            view.UpdateView();
        }

        public void MealDay_onClick(string text)
        {
            Intent intent = new Intent();
            intent.PutExtra("id", mealDays[text].id);
            view.Navigate(1, intent);
        }
    }
}