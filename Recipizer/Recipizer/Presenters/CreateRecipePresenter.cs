using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        //Interface Methods
        public void onBackPressed() { }

        public void onCreate() { }

        public void onDestroy() { }

        public void onPause() { }

        public void onResume() { }

        //Regular Methods
        public void CreateRecipe(ICollection<Ingredient> _Ingredients, string _Title, string _Description)
        {
            //TODO Validate inputs

            //TODO Create and save recipe
            Recipe r = new Recipe(_Ingredients.ToList(), _Title, _Description, DateTime.Now);
            Temp.tempContainer.instance.RecipieContainer.Add(r);

            //Calls to the view
            view.MakeToast(r.Title + " created", Android.Widget.ToastLength.Short);
            view.FinishView(Android.App.Result.Ok);
            
            //TODO Go back and show the created recipe.
        }

        public void AddIngredient(string _Name, string _Amount, Ingredient.Unit _Unit)
        {
            Ingredient ing = new Ingredient(_Name, _Amount, _Unit);
            ingredientsList.Add(ing);

            view.UpdateView();
        }


        
    }
}