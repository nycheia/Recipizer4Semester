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
        //UI components
        EditText editTextIngredientName;
        EditText editTextIngredientAmount;
        EditText editTextRecipeDescription;
        EditText editTextRecipeName;
        Spinner spinnerUnits;
        ListView listIngredients;

        //Adapters
        IngredientButtonAdapter ingredientAdapter;

        //Variable to connect to presenter
        CreateRecipePresenter presenter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CreateRecipe);

            //Instantiate the presenter
            presenter = new CreateRecipePresenter(this);

            //Get UI components for global use.
            editTextIngredientName =    FindViewById<EditText>(Resource.Id.editTextIngredientName_CreateRecipe);
            editTextIngredientAmount =  FindViewById<EditText>(Resource.Id.editTextIngredientAmount_CreateRecipe);
            editTextRecipeDescription = FindViewById<EditText>(Resource.Id.editTextRecipeDescription_CreateRecipe);
            editTextRecipeName =        FindViewById<EditText>(Resource.Id.editTextRecipeName_CreateRecipe);
            spinnerUnits =              FindViewById<Spinner>(Resource.Id.spinnerUnits_CreateRecipe);
            listIngredients =           FindViewById<ListView>(Resource.Id.listIngredients_CreateRecipe);

            //Get UI components for local use.
            Button btnAddIngredient = FindViewById<Button>(Resource.Id.btnAddIngredient_CreateRecipe);
            Button btnCreateRecipe = FindViewById<Button>(Resource.Id.btnCreateRecipe_CreateRecipe);

            //Instantiate Spinner (Dropdown).
            var unitAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.unit_array, Android.Resource.Layout.SimpleSpinnerItem);
            spinnerUnits.Adapter = unitAdapter;

            //Setup lists.
            ingredientAdapter = new IngredientButtonAdapter(this, presenter.ingredientsList);
            listIngredients.Adapter = ingredientAdapter;

            //Setup Button Events.
            btnAddIngredient.Click += (object sender, EventArgs e) => {
                presenter.AddIngredient(editTextIngredientName.Text, editTextIngredientAmount.Text, Ingredient.StringToUnit((string)spinnerUnits.SelectedItem));
            };

            btnCreateRecipe.Click += (object sender, EventArgs e) => {
                presenter.SaveRecipe(presenter.ingredientsList, editTextRecipeName.Text, editTextRecipeDescription.Text);
            };

            int i = Intent.GetIntExtra(Constants.RECIPE_ID, 0);
            if ( i != 0)
            {
                presenter.RecipeID = i;
            }

            //Call the presenters OnCreate Method.
            presenter.onCreate();
        }

        public void FinishView(Result _result, Intent intent)
        {
            SetResult(_result, intent);
            Finish();
        }

        public void MakeToast(string text, ToastLength length)
        {
            Toast.MakeText(this, text, length);
        }

        public void UpdateView()
        {
            ingredientAdapter.NotifyDataSetChanged();
        }

        public void ResetText()
        {
            //Reset textfields and drop down list.
            editTextIngredientName.Text = "";
            editTextIngredientAmount.Text = "";
            spinnerUnits.SetSelection(0);
        }

        public void Navigate(int code, Intent data) { }

        public void SetupView()
        {
            if (presenter.CurrentRecipe != null)
            {
                editTextRecipeName.Text = presenter.CurrentRecipe.Title;
                editTextRecipeDescription.Text = presenter.CurrentRecipe.Description;
            }
        }

        public void RequestPermission()
        {
            throw new NotImplementedException();
        }

        public void MakeOKButtonDialog()
        {
            throw new NotImplementedException();
        }

        public void MakeDialog(int code)
        {
            throw new NotImplementedException();
        }
    }
}