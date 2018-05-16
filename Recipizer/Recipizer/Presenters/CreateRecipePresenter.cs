using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;

/*using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;*/
using Recipizer.Activities;
using Recipizer.Models;

namespace Recipizer.Presenters
{
    public class CreateRecipePresenter : IPresenter
    {
        private IRecipizerView view;

        public List<Ingredient> ingredientsList;

        


        public CreateRecipePresenter(IRecipizerView _view)
        {
            this.view = _view;
            ingredientsList = new List<Ingredient>();
        }

        public void CreateRecipe(ICollection<Ingredient> _Ingredients, string _Title, string _Description)
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
            view.FinishView(Android.App.Result.Ok, new Intent().PutExtra("recipeId", r.id));
            
            //TODO Go back and show the created recipe.

        }

        public void AddIngredient(string _Name, string _Amount, Ingredient.Unit _Unit)
        {
            Ingredient ing = new Ingredient(_Name, _Amount, _Unit);
            ingredientsList.Add(ing); 
            view.UpdateView();
        }

        //Interface Methods
        public void onBackPressed() { }

        public void onCreate() { }

        public void onDestroy() { }

        public void onPause() { }

        public void onResume() { }

        public void onActivityResult(int requestCode, Result resultCode, Intent data) { }
    }
}