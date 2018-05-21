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

namespace Recipizer.Presenters
{
    public class ViewMealPlanPresenter : IPresenter
    {
        public List<string> mealDays;
        public List<Recipe> recipes;

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

        public void populateMealDaysFromStorage(int mealPlanId)
        {
            MealPlan mp = Constants.Conn.Get<MealPlan>(mealPlanId);
            var query = from md in Constants.Conn.Table<MealDay>() where md.mealPlanId == mealPlanId select md;
            foreach (MealDay item in query)
            {
                mealDays.Add(item.day.ToString());
                var recipeQuery = from mdr in Constants.Conn.Table<MealDayRecipe>() where mdr.id == item.id select mdr;
                foreach (MealDayRecipe i in recipeQuery)
                {
                    recipes.Add(Constants.Conn.Get<Recipe>(i.recipeId));
                }
            }

        }
    }
}