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
            CurrentRecipe = Temp.tempContainer.instance.RecipieContainer[_RecipeID];
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