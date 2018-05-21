using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Recipizer.Adapters;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Recipizer.Activities;
//using Recipizer.Models;
using Recipizer.Presenters;

namespace Recipizer.Activities
{
    [Activity(Label = "ViewMealPlanActivity")]
    public class ViewMealPlanActivity : Activity, IRecipizerView
    {
        ExpandableListView mealDayExpList;

        MealDayAdapter mealDayAdapter;

        ViewMealPlanPresenter presenter;

        List<string> mealDayList;
        Dictionary<string, List<string>> recipes;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ViewMealPlan);

            //MealPlan mealPlan = Constants.Conn.Get<MealPlan>(Intent.GetIntExtra("mealPlanId", 0));

            presenter = new ViewMealPlanPresenter();

            presenter.populateMealDaysFromStorage(Intent.GetIntExtra("mealPlanId", 1));

            ExpandableListView mealPlanListView = FindViewById<ExpandableListView>(Resource.Id.mealPlanExpandableListView);
            mealDayAdapter = new MealDayAdapter(this, recipes, mealDayList);

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
            throw new NotImplementedException();
        }


    }
}