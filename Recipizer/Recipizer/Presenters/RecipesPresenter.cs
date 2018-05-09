using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Recipizer.Activities;
using Recipizer.Models;

namespace Recipizer.Presenters
{
    class RecipesPresenter : IPresenter
    {
        public List<string> RecipeList;

        IRecipizerView view;

        public RecipesPresenter(IRecipizerView _view)
        {
            this.view = _view;
            RecipeList = new List<string>();
            LoadRecipesFromStorage();
        }
        
        public void LoadRecipesFromStorage()
        {
            foreach (Recipe item in Constants.Conn.Table<Recipe>().ToList())
            {
                RecipeList.Add(item.Title);
            }
        }

        public void Recipes_OnItemClick(int position)
        {
            //gets the id of the recipe.
            int id = (Constants.Conn.Table<Recipe>().Skip(position).Take(1)).First().id;

            view.Navigate(Constants.SHOW_RECIPE, new Intent().PutExtra(Constants.RECIPE_ID, id));
        }

        public void NewRecipe_Click()
        {
            view.Navigate(Constants.NEW_RECIPE, null);
        }

        public void onBackPressed() { }

        public void onCreate() { }

        public void onDestroy() { }

        public void onPause() { }

        public void onResume()
        {
            LoadRecipesFromStorage();
            view.UpdateView();
        }

        public void onActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == Constants.NEW_RECIPE && resultCode == Result.Ok)
            {
                view.Navigate(Constants.SHOW_RECIPE, data);
            }
        }
    }
}