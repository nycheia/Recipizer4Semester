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
using static Android.Widget.AdapterView;

namespace Recipizer.Activities
{
    [Activity(Label = "RecipesActivity")]
    public class RecipesActivity : Activity
    {
        ArrayAdapter<string> adpt;
        ListView listViewRecipes;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Recipes);



            //Initialize UI components
            Button btnNewRecipe = FindViewById<Button>(Resource.Id.btnNewRecipe);
            listViewRecipes = FindViewById<ListView>(Resource.Id.listViewRecipes);

            //Initialize List
            //List<Recipe> ingredients = new List<Recipe>();

            //initialize adapters
            adpt = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleExpandableListItem1);
            listViewRecipes.Adapter = adpt;

            //UI interaction
            btnNewRecipe.Click += (sender, e) => { btnNewRecipe_Click(sender, e); }; ;
            listViewRecipes.ItemClick += (sender, e) => { listViewRecipes_OnITemClick(sender, e); }; ;
        }

        // OnResume is called whenever the user returns to the activity or when the activity is created.
        protected override void OnResume()
        {
            base.OnResume();

            //get all recipes and add to list
            adpt.Clear();
            foreach (Recipe item in Temp.tempContainer.instance.RecipieContainer)
            {
                adpt.Add(item.id);
            }
        }

        public void listViewRecipes_OnITemClick(object sender, ItemClickEventArgs e)
        {
            Toast.MakeText(this, "Navigating to: " + adpt.GetItem(e.Position), ToastLength.Short).Show();
        }

        public void btnNewRecipe_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CreateRecipeActivity));
            StartActivityForResult(intent, Constants.CREATE_RECIPE);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == Constants.CREATE_RECIPE && resultCode == Result.Ok)
            {
                //TODO send the user to show recipe screen with the created recipe
                Toast.MakeText(this, "Navigating to: Unknown", ToastLength.Short).Show();
            }
        }
    }
}