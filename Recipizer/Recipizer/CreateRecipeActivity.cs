using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Recipizer.Adapters;
using Recipizer.Models;

namespace Recipizer
{
    [Activity(Label = "CreateRecipeActivity")]
    public class CreateRecipeActivity : Activity
    {
        List<Ingredient> ingredients;
        //List<String> ingredients;
        //ArrayAdapter<String> ingredientAdapter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CreateRecipe);
            // Create your application here

            //Initialize UI components
            EditText editTextIngredientName = FindViewById<EditText>(Resource.Id.editTextIngredientName);
            EditText editTextIngredientAmount = FindViewById<EditText>(Resource.Id.editTextIngredientAmount);
            EditText editTextRecipeDescription = FindViewById<EditText>(Resource.Id.editTextRecipeDescription);
            Spinner spinnerUnits = FindViewById<Spinner>(Resource.Id.spinnerUnits);
            Button btnAddIngredient = FindViewById<Button>(Resource.Id.btnAddIngredient);
            ListView listIngredients = FindViewById<ListView>(Resource.Id.listIngredients);
            //listIngredients.SetBackgroundColor(Color.CadetBlue);

            //Initialize lists
            ingredients = new List<Ingredient>();

            //UI interaction
            spinnerUnits.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinnerUnits_ItemSelected);
            var unitAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.unit_array, Android.Resource.Layout.SimpleSpinnerItem);
            unitAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerItem);
            spinnerUnits.Adapter = unitAdapter;

            //ArrayAdapter<String> adapter = new ArrayAdapter<string>( this, Android.Resource.Layout.SimpleListItem1);
            IngredientAdapter ingredientAdapter = new IngredientAdapter(this, ingredients);
            listIngredients.Adapter = ingredientAdapter;
            btnAddIngredient.Click += (sender, e) =>
            {

                Ingredient ingredient = new Ingredient(editTextIngredientName.Text, editTextIngredientAmount.Text, 
                    Ingredient.StringToUnit((String)spinnerUnits.GetItemAtPosition(spinnerUnits.SelectedItemPosition)));
                ingredients.Add(ingredient);
                //ingredientAdapter.Add(ingredient.name);
                //adapter.Add(ingredient.name);
                //var ingredientAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, ingredients);
                ingredientAdapter.NotifyDataSetChanged();

                editTextIngredientName.Text = "";
                editTextIngredientAmount.Text = "";
                spinnerUnits.SetSelection(0);
            };
        }

        private void spinnerUnits_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinnerUnits = (Spinner)sender;
            //string toast = string.Format("Measuring unit: {0}", spinnerUnits.GetItemAtPosition(e.Position));
            //Toast.MakeText(this, toast, ToastLength.Long).Show();
        }

        private void addIngredient()
        {
            //Ingredient ingredient = new Ingredient(editTextIngredientName.Text(), );
        }
    }
}