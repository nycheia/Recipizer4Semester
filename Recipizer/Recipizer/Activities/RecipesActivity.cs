using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Recipizer.Models;
using Recipizer.Presenters;
using static Android.Widget.AdapterView;

namespace Recipizer.Activities
{
    [Activity(Label = "RecipesActivity")]
    public class RecipesActivity : Activity, IRecipizerView
    {

        //UI components
        ListView listViewRecipes;

        //Adapters
        ArrayAdapter<string> RecipeAdapter;

        //Variable to connect to presenter
        RecipesPresenter presenter;      

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Recipes);

            //Instantiate the presenter
            presenter = new RecipesPresenter(this);

            //Get UI components for global use.
            listViewRecipes = FindViewById<ListView>(Resource.Id.listViewRecipes);

            //Get UI components for local use.
            Button btnNewRecipe = FindViewById<Button>(Resource.Id.btnNewRecipe);

            //Setup lists.
            RecipeAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleExpandableListItem1, presenter.RecipeList);
            listViewRecipes.Adapter = RecipeAdapter;

            //Setup Click Events.
            btnNewRecipe.Click += (sender, e) => {
                presenter.NewRecipe_Click();
            };

            listViewRecipes.ItemClick += (sender, e) => {
                presenter.Recipes_OnItemClick(e.Position);
            };

            //Call the presenters OnCreate Method.
            presenter.onCreate();

        }

        // OnResume is called whenever the user returns to the activity or when the activity is created.
        protected override void OnResume()
        {
            base.OnResume();
            presenter.onResume();
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            presenter.onActivityResult(requestCode, resultCode, data);
        }

        public void UpdateView()
        {
            RecipeAdapter.NotifyDataSetChanged();
        }

        public void ResetText()
        {
            RecipeAdapter.Clear();
        }

        public void FinishView(Result result) { }

        public void MakeToast(string text, ToastLength length)
        {
            Toast.MakeText(this, text, length).Show();
        }

        public void Navigate(int code)
        {
            if (code == Constants.NEW_RECIPE)
            {
                Intent intent = new Intent(this, typeof(CreateRecipeActivity));
                StartActivityForResult(intent, Constants.NEW_RECIPE);

            }
            else if (code == Constants.SHOW_RECIPE)
            {
                //TODO somehow tell it what recipe to show.
                /*Intent intent = new Intent(this, typeof(ViewRecipeActivity));
                intent.PutExtra("RecipeID", 1);
                StartActivity(intent);*/
            }
        }
    }
}