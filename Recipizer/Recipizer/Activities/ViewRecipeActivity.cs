﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Recipizer.Presenters;
using Recipizer.Models;
using Recipizer.Adapters;

namespace Recipizer.Activities
{
    [Activity(Label = "ViewRecipeActivity")]
    public class ViewRecipeActivity : Activity, IRecipizerView
    {

        //UI components

        //Adapters
        IngredientAdapter ingredientAdapter;

        //Variable to connect to presenter
        ViewRecipePresenter presenter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ViewRecipe);

            //Instantiate the presenter
            presenter = new ViewRecipePresenter(this, Intent.GetIntExtra(Constants.RECIPE_ID, 0));

            //Get UI components for global use.
            TextView textRecipeName = FindViewById<TextView>(Resource.Id.textRecipeName);
            TextView textEditRecipeDescription = FindViewById<TextView>(Resource.Id.textEditRecipeDescription);
            

            //Get UI components for local use.
            Button btnShare = FindViewById<Button>(Resource.Id.btnShare);
            Button btnEdit = FindViewById<Button>(Resource.Id.btnEdit);
            ListView listViewIngredients = FindViewById<ListView>(Resource.Id.listViewIngredients);

            //Setup lists.
            ingredientAdapter = new IngredientAdapter(this, presenter.CurrentRecipe.Ingredients);
            listViewIngredients.Adapter = ingredientAdapter;

            //Setup Click Events.
            btnShare.Click += (sender, e) =>
            {
                presenter.Share_Click();
            };

            btnEdit.Click += (sender, e) =>
            {
                presenter.Edit_Click();
            };

            //Set Text
            textRecipeName.Text = presenter.CurrentRecipe.Title;
            textEditRecipeDescription.Text = presenter.CurrentRecipe.Description;

            //Call the presenters OnCreate Method.
            presenter.onCreate();
        }

        public void FinishView(Result result, Intent data) { }

        public void MakeToast(string text, ToastLength length) { }

        public void Navigate(int code, Intent data)
        {
            if (code == Constants.EDIT_RECIPE)
            {
                Intent intent = new Intent(this, typeof(CreateRecipeActivity));
                intent.PutExtras(data);
                StartActivityForResult(intent, Constants.EDIT_RECIPE);
            }
        }

        public void ResetText() { }

        public void UpdateView()
        {
            ingredientAdapter.NotifyDataSetChanged();
        }

        public void SetupView()
        {
            throw new NotImplementedException();
        }
    }
}