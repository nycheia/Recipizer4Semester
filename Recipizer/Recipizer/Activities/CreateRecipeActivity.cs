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
using Recipizer.Presenters;

namespace Recipizer.Activities
{
    [Activity(Label = "Recipes")]
    public class CreateRecipeActivity : Activity, IRecipizerView
    {
        List<Ingredient> ingredients;

        //UI components
        EditText editTextIngredientName;
        EditText editTextIngredientAmount;
        EditText editTextRecipeDescription;
        EditText editTextRecipeName;
        Spinner spinnerUnits;
        ListView listIngredients;

        //Adapters
        IngredientAdapter ingredientAdapter;

        //Variable to connect to relevant presenter
        CreateRecipePresenter Presenter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CreateRecipe);

            //Instantiate the presenter
            Presenter = new CreateRecipePresenter(this);

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
            btnCreateRecipe.Click  += (object sender, EventArgs e) => { Presenter.CreateRecipe(ingredients, editTextRecipeName.Text, editTextRecipeDescription.Text); };

            //Instantiate Spinner (Dropdown)
            var unitAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.unit_array, Android.Resource.Layout.SimpleSpinnerItem);
            //unitAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerItem);
            spinnerUnits.Adapter = unitAdapter;

        }

        public void btnAddIngredient_Click(object sender, EventArgs e)
        {
            //Creates the ingredient from the relvant information
            Ingredient.Unit unit = Ingredient.StringToUnit((string)spinnerUnits.SelectedItem);
            Ingredient ingredient = new Ingredient(editTextIngredientName.Text, editTextIngredientAmount.Text, unit);

            //Adds the ingredient to the list and updates the listview.
            ingredients.Add(ingredient);
            ingredientAdapter.NotifyDataSetChanged();

            //Reset textfields and drop down list.
            editTextIngredientName.Text = "";
            editTextIngredientAmount.Text = "";
            spinnerUnits.SetSelection(0);
        }        

        public void FinishView(Result _result)
        {
            SetResult(_result);
            Finish();
        }

        public void MakeToast(string text, ToastLength length)
        {
            Toast.MakeText(this, text, length);
        }

        //Unused methods is placed here.
        public void UpdateView() { }
        public void Navigate() { }
    }
}