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
        public List<string> hej = new List<string>();

        public ViewRecipePresenter(IRecipizerView _view, int _RecipeID)
        {
            this.view = _view;

            //Get the Recipe object
            CurrentRecipe = Constants.Conn.Get<Recipe>(_RecipeID);
            CurrentRecipe.setIngredients();
            //TODO do better
            foreach (Ingredient item in CurrentRecipe.Ingredients)
            {
                hej.Add(item.name);
            }
        }

        public void Share_Click()
        {
            //TODO Share functionality
        }

        public void Edit_Click()
        {
            //TODO Edit Functionality
        }

        public void onCreate() { }
        
        public void onPause() { }

        public void onDestroy() { }

        public void onResume() { }
        public void onBackPressed() { }

        public void onActivityResult(int requestCode, Result resultCode, Intent data) { }
    }
}