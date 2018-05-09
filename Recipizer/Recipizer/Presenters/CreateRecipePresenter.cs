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
    public class CreateRecipePresenter : IPresenter
    {
        private IRecipizerView view;

        public List<Ingredient> ingredientsList;
        public int RecipeID;
        public Recipe CurrentRecipe;


        public CreateRecipePresenter(IRecipizerView _view)
        {
            this.view = _view;
            ingredientsList = new List<Ingredient>();
            RecipeID = -1;
        }

        public void SaveRecipe(ICollection<Ingredient> _Ingredients, string _Title, string _Description)
        {
            //TODO Validate inputs

            Recipe r = new Recipe(_Ingredients.ToList(), _Title, _Description, DateTime.Now);
            Constants.Conn.Insert(r);
            foreach (Ingredient i in ingredientsList)
            {
                i.RecipeId = r.id;
                Constants.Conn.Insert(i);
            }

            //Calls to the view
            view.MakeToast(r.Title + " created", Android.Widget.ToastLength.Short);
            view.FinishView(Result.Ok, new Intent().PutExtra(Constants.RECIPE_ID, r.id));
        }

        public void AddIngredient(string _Name, string _Amount, Ingredient.Unit _Unit)
        {
            Ingredient ing = new Ingredient(_Name, _Amount, _Unit);
            ingredientsList.Add(ing); 
            view.UpdateView();
            view.ResetText();
        }

        //Interface Methods
        public void onBackPressed() { }

        public void onCreate()
        {
            if (RecipeID != -1)
            {
                CurrentRecipe = Constants.Conn.Get<Recipe>(RecipeID);
                ingredientsList.AddRange(CurrentRecipe.Ingredients);
                view.SetupView();
                view.UpdateView();
            }
        }

        public void onDestroy() { }

        public void onPause() { }

        public void onResume() { }

        public void onActivityResult(int requestCode, Result resultCode, Intent data) { }
    }
}