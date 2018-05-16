﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Recipizer.Presenters;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Recipizer.Activities
{
    [Activity(Label = "CreateMealPlan")]
    public class CreateMealPlanActivity : Activity, IRecipizerView
    {
        ListView recipeList;
        ListView pickedRecipeList;
        TextView amountOfDaysTextView;
        TextView startDateEditText;
        Button   addMealDayBtn;

        ArrayAdapter<string> RecipeAdapter;

        ArrayAdapter<string> pickedRecipeAdapter;

        CreateMealPlanPresenters presenter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CreateMealPlan);
            
            //
            presenter = new CreateMealPlanPresenters(this);
            //
            recipeList              = FindViewById<ListView>(Resource.Id.CreateMealPlanRecipeListView);
            pickedRecipeList        = FindViewById<ListView>(Resource.Id.PickedRecipesListView);
            amountOfDaysTextView    = FindViewById<TextView>(Resource.Id.dayAmountTextView);
            startDateEditText       = FindViewById<TextView>(Resource.Id.StartDateText);
            addMealDayBtn           = FindViewById<Button>(Resource.Id.AddMealDayBtn);
            
            //TODO lav adapters til lister
            RecipeAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleExpandableListItem1, presenter.RecipeList);
            recipeList.Adapter = RecipeAdapter;

            pickedRecipeAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleExpandableListItem1, presenter.pickedRecipes);
            pickedRecipeList.Adapter = pickedRecipeAdapter;


            //TODO lav button press metoder til to knapper, husk at tjekke alt er fyldt ud, top dollar brutha (Y)
            recipeList.ItemClick += (sender, e) => {
                presenter.ChooseRecipe_OnList(e.Position);
            };

            startDateEditText.Click += PickDate_OnClick;
            
            
        }
        public void FinishView(Result result, Intent intent)
        {
            throw new NotImplementedException();
        }

        public void MakeToast(string text, ToastLength length)
        {
            throw new NotImplementedException();
        }

        public void Navigate(int code, Intent data)
        {
            throw new NotImplementedException();
        }

        public void ResetText()
        {
            throw new NotImplementedException();
        }

        public void UpdateView()
        {
            RecipeAdapter.Clear();
            RecipeAdapter.AddAll(presenter.RecipeList);
            RecipeAdapter.NotifyDataSetChanged();
            pickedRecipeAdapter.Clear();
            pickedRecipeAdapter.AddAll(presenter.pickedRecipes);
            pickedRecipeAdapter.NotifyDataSetChanged();
        }

        public void PickDate_OnClick(object sender, EventArgs eventArgs)
        {

            MealPlanDateAmountFragment dialog = new MealPlanDateAmountFragment(this, "Number of days in mealplan:");
            dialog.Show(FragmentManager, "hej");

            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                startDateEditText.Text = time.ToLongDateString();
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);

            amountOfDaysTextView.Text = "" + MealPlanDateAmountFragment.current;
        }
    }
}