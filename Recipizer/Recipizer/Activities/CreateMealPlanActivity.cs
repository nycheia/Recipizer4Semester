using System;
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
            RecipeAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleExpandableListItem1, presenter.recipeDictionary.Keys.ToList());
            recipeList.Adapter = RecipeAdapter;

            pickedRecipeAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleExpandableListItem1, presenter.pickedRecipeDictionary.Keys.ToList());
            pickedRecipeList.Adapter = pickedRecipeAdapter;


            //TODO lav button press metoder til to knapper, husk at tjekke alt er fyldt ud, top dollar brutha (Y)
            recipeList.ItemClick += (sender, e) => {
                string dictionaryKey = RecipeAdapter.GetItem(e.Position);
                presenter.ChooseRecipe_OnList(dictionaryKey);
            };

            startDateEditText.Click += PickDate_OnClick;

            addMealDayBtn.Click += (sender, e) => {
                presenter.OnClickAddMealDay();
            };

            presenter.onCreate();
            
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
            RecipeAdapter.AddAll(presenter.recipeDictionary.Keys);
            RecipeAdapter.NotifyDataSetChanged();
            pickedRecipeAdapter.Clear();
            pickedRecipeAdapter.AddAll(presenter.pickedRecipeDictionary.Keys);
            pickedRecipeAdapter.NotifyDataSetChanged();
            amountOfDaysTextView.Text = presenter.counter + "/" + presenter.mp.amountOfDays;

        }

        public void PickDate_OnClick(object sender, EventArgs eventArgs)
        {

            MealPlanDateAmountFragment dialog = new MealPlanDateAmountFragment(this, "Number of days in mealplan:");
            dialog.Show(FragmentManager, "hej");
            dialog.clicked = SetNumberOfDays;
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                startDateEditText.Text = time.ToLongDateString();
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);

            //amountOfDaysTextView.Text = "" + MealPlanDateAmountFragment.current;
        }

        public void SetNumberOfDays(int days)
        {
            //amountOfDaysTextView.Text = days.ToString();
            presenter.MealPlanAmountOfDays(days);
        }
    }
}