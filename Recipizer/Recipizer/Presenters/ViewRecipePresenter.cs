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
    public class ViewRecipePresenter : IPresenter
    {
        IRecipizerView view;

        //Recipe instance
        public Recipe CurrentRecipe;

        public ViewRecipePresenter(IRecipizerView _view, int _RecipeID)
        {
            this.view = _view;

            //Get the Recipe object
            CurrentRecipe = Constants.Conn.Get<Recipe>(_RecipeID);
            CurrentRecipe.SetIngredients();
        }

        public void Share_Click()
        {
            view.Navigate(Constants.SHARE, new Intent().PutExtra(Constants.RECIPE_ID, CurrentRecipe.id).PutExtra("type", "recipe"));
        }

        public void Edit_Click()
        {
            view.Navigate(Constants.EDIT_RECIPE, new Intent().PutExtra(Constants.RECIPE_ID, CurrentRecipe.id));
        }

        public void onCreate()
        {
            view.UpdateView();
        }
        
        public void onPause() { }

        public void onDestroy() { }

        public void onResume() { }

        public void onBackPressed() { }

        public void onActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == Constants.EDIT_RECIPE && resultCode == Result.Ok)
            {
                CurrentRecipe = CurrentRecipe = Constants.Conn.Get<Recipe>(data.GetIntExtra(Constants.RECIPE_ID, 0));
                CurrentRecipe.SetIngredients();
                view.UpdateView();
            }
        }
    }
}