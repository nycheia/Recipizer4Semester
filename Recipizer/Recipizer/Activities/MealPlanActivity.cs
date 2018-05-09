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
using Recipizer.Presenters;

namespace Recipizer.Activities
{
    [Activity(Label = "MealPlanActivity")]
    public class MealPlanActivity : Activity, IRecipizerView
    {
        ListView listViewMealPlan;

        ArrayAdapter<string> mealPlanAdapter;

        MealPlanPresenter presenter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MealPlan);
            // instansiating presenter
            presenter = new MealPlanPresenter(this);
            // setting up UI components
            listViewMealPlan = FindViewById<ListView>(Resource.Id.ListMealPlan);
            //setting up adapter
            mealPlanAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleExpandableListItem1); //presenter.mealPlanList);
            listViewMealPlan.Adapter = mealPlanAdapter;
            //presenter.onCreate();
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
           // mealPlanAdapter.NotifyDataSetChanged();
        }

    }
}