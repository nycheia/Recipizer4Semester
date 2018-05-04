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

namespace Recipizer.Activities
{
    [Activity(Label = "Recipes")]
    public class CreateRecipeActivity : Activity, IRecipizerView
    {
        List<Ingredient> ingredients;
        EditText editTextIngredientName;
        EditText editTextIngredientAmount;
        EditText editTextRecipeDescription;
        EditText editTextRecipeName;
        Spinner spinnerUnits;
        ListView listIngredients;
        IngredientAdapter ingredientAdapter;

        //List<String> ingredients;
        //ArrayAdapter<String> ingredientAdapter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CreateRecipe);

            //Get UI components for global use.
            editTextIngredientName =    FindViewById<EditText>(Resource.Id.editTextIngredientName_CreateRecipe);
            editTextIngredientAmount =  FindViewById<EditText>(Resource.Id.editTextIngredientAmount_CreateRecipe);
            editTextRecipeDescription = FindViewById<EditText>(Resource.Id.editTextRecipeDescription_CreateRecipe);
            editTextRecipeName =        FindViewById<EditText>(Resource.Id.editTextRecipeName_CreateRecipe);
            spinnerUnits =              FindViewById<Spinner>(Resource.Id.spinnerUnits_CreateRecipe);
            listIngredients =           FindViewById<ListView>(Resource.Id.listIngredients_CreateRecipe);

            //Get UI components for local use.
            Button btnAddIngredient = FindViewById<Button>(Resource.Id.btnAddIngredient_CreateRecipe);
            Button btnCreateRecipe =  FindViewById<Button>(Resource.Id.btnCreateRecipe_CreateRecipe);

            //Initialize lists
            ingredients = new List<Ingredient>();

            ingredientAdapter = new IngredientAdapter(this, ingredients);
            listIngredients.Adapter = ingredientAdapter;

            //Instantiate Button Events
            btnAddIngredient.Click += (object sender, EventArgs e) => { btnAddIngredient_Click(sender, e); };
            btnCreateRecipe.Click  += (object sender, EventArgs e) => { btnCreateRecipe_Click(sender, e); };

            //UI interaction
            spinnerUnits.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinnerUnits_ItemSelected);
            var unitAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.unit_array, Android.Resource.Layout.SimpleSpinnerItem);
            unitAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerItem);
            spinnerUnits.Adapter = unitAdapter;

            
            // Temp Test data.
            ingredients.Add(new Ingredient("Peber",     "5",    Ingredient.Unit.cup));
            ingredients.Add(new Ingredient("Salt", "    10",    Ingredient.Unit.kg));
            ingredients.Add(new Ingredient("Estragon",  "20",   Ingredient.Unit.tbsp));
            ingredients.Add(new Ingredient("Peber1",    "5",    Ingredient.Unit.cup));
            ingredients.Add(new Ingredient("Salt1",     "10",   Ingredient.Unit.kg));
            ingredients.Add(new Ingredient("Estragon1", "20",   Ingredient.Unit.tbsp));
            ingredients.Add(new Ingredient("Peber2",    "5",    Ingredient.Unit.cup));
            ingredients.Add(new Ingredient("Salt2",     "10",   Ingredient.Unit.kg));
            ingredients.Add(new Ingredient("Estragon2", "20",   Ingredient.Unit.tbsp));
        }

        /*public override void OnBackPressed()
        {

            Finish();
        }*/

        public void btnAddIngredient_Click(object sender, EventArgs e)
        {
            //Creates the ingredient from the relvant information
            Ingredient.Unit unit = Ingredient.StringToUnit((String)spinnerUnits.GetItemAtPosition(spinnerUnits.SelectedItemPosition));
            Ingredient ingredient = new Ingredient(editTextIngredientName.Text, editTextIngredientAmount.Text, unit);

            //Adds the ingredient to the list and updates the listview.
            ingredients.Add(ingredient);
            ingredientAdapter.NotifyDataSetChanged();

            //Reset textfields and drop down list.
            editTextIngredientName.Text = "";
            editTextIngredientAmount.Text = "";
            spinnerUnits.SetSelection(0);
        }

        public void btnCreateRecipe_Click(object sender, EventArgs e)
        {
            //TODO Validate inputs

            //TODO Create and save recipe
            Recipe r = new Recipe(ingredients, editTextRecipeName.Text, editTextRecipeDescription.Text, DateTime.Now);
            Temp.tempContainer.instance.RecipieContainer.Add(r);
            Toast.MakeText(this, r.Title + " created", ToastLength.Short).Show();
  
            SetResult(Result.Ok);
            Finish();

            //TODO Go back and maybe show the created recipe.
        }

        

        private void spinnerUnits_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinnerUnits = (Spinner)sender;
        }
    }
}