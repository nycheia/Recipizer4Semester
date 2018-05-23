using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Recipizer.Models;
using Recipizer.Activities;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Recipizer.Presenters
{
    public class ViewMealDayPresenter : IPresenter
    {
        public Dictionary<string, Recipe> recipes;

        IRecipizerView view;
        
        public ViewMealDayPresenter(IRecipizerView view)
        {
            this.view = view;
            recipes = new Dictionary<string, Recipe>();
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

        public void PopulateRecipeList(int mealDayId)
        {
            var query = from mdr in Constants.Conn.Table<MealDayRecipe>() where mdr.mealDayId == mealDayId select mdr;
            foreach (MealDayRecipe mdr in query)
            {
                Recipe r = Constants.Conn.Get<Recipe>(mdr.recipeId);
                recipes.Add(r.Title, r);
            }
            view.UpdateView();
        }

        public void Recipe_onClick(string text)
        {
            Intent intent = new Intent();
            intent.PutExtra(Constants.RECIPE_ID, recipes[text].id);
            view.Navigate(1, intent);
        }
    }
}