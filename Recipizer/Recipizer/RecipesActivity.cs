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

namespace Recipizer
{
    [Activity(Label = "RecipesActivity")]
    public class RecipesActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Recipes);
            // Create your application here

            //Initialize UI components
            Button btnNewRecipe = FindViewById<Button>(Resource.Id.btnNewRecipe);

            //UI interaction
            btnNewRecipe.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(CreateRecipeActivity));
                StartActivity(intent);
            };
        }
    }
}