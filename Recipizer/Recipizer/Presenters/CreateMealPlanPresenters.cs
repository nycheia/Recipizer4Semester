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
using Recipizer.Models;
using Recipizer.Activities;

namespace Recipizer.Presenters
{
    public class CreateMealPlanPresenters : IPresenter
    {
        public List<string> RecipeList;
        IRecipizerView view;
        public List<string> pickedRecipes;

        public CreateMealPlanPresenters(IRecipizerView _view)
        {
            this.view = _view;
            RecipeList = new List<string>();
            pickedRecipes = new List<string>();
            loadRecipesFromStorage();
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

        public void loadRecipesFromStorage()
        {
            foreach (Recipe item in Constants.Conn.Table<Recipe>().ToList())
            {
                RecipeList.Add(item.Title);
            }
        }



        public void ChooseRecipe_OnList(int position)
        {
            pickedRecipes.Add(RecipeList[position]);
            view.UpdateView();
        }
    }
}