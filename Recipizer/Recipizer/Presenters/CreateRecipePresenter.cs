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
    class CreateRecipePresenter : IPresenter
    {
        private IRecipizerView view;

        public CreateRecipePresenter(IRecipizerView _view)
        {
            this.view = _view;
        }

        public void onBackPressed() { }

        public void onCreate() { }

        public void onDestroy() { }

        public void onPause() { }

        public void onResume() { }

        public void CreateRecipe(ICollection<Ingredient> _Ingredients, string _Title, string _Description)
        {
            //TODO Validate inputs

            //TODO Create and save recipe
            Recipe r = new Recipe(_Ingredients.ToList(), _Title, _Description, DateTime.Now);
            Temp.tempContainer.instance.RecipieContainer.Add(r);

            view.Maketoast(r.Title + " created", Android.Widget.ToastLength.Short);
            view.FinishView(Android.App.Result.Ok);
            
            //TODO Go back and maybe show the created recipe.
        }

        
    }
}