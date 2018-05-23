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
    [Activity(Label = "ViewMealPlanActivity", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class ViewMealPlanActivity : Activity, IRecipizerView
    {
        ListView mealDaysList;

        ArrayAdapter<string> mealDayAdapter;

        ViewMealPlanPresenter presenter;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ViewMealPlan);

            //MealPlan mealPlan = Constants.Conn.Get<MealPlan>(Intent.GetIntExtra("mealPlanId", 0));

            presenter = new ViewMealPlanPresenter(this);

            //ExpandableListView mealPlanListView = FindViewById<ExpandableListView>(Resource.Id.mealPlanExpandableListView);
            //mealDayAdapter = new MealDayAdapter(this, presenter.recipes, presenter.mealDays);
            //mealPlanListView.Adapter = mealDayAdapter;

            

            mealDaysList = FindViewById<ListView>(Resource.Id.mealDayListView);
            mealDayAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleExpandableListItem1, presenter.mealDays.Keys.ToList());
            mealDaysList.Adapter = mealDayAdapter;

            mealDaysList.ItemClick += (sender, e) =>
            {
                presenter.MealDay_onClick(mealDayAdapter.GetItem(e.Position));
            };


            presenter.PopulateMealDaysFromStorage(Intent.GetIntExtra("mealPlanId", 1));

            

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
            Intent intent = new Intent(this, typeof(ViewMealDayActivity));
            intent.PutExtras(data);
            StartActivityForResult(intent, 2);
        }

        public void ResetText()
        {
            throw new NotImplementedException();
        }

        public void UpdateView()
        {
            mealDayAdapter.Clear();
            mealDayAdapter.AddAll(presenter.mealDays.Keys.ToList());
            mealDayAdapter.NotifyDataSetChanged();
        }

        public void SetupView()
        {
            throw new NotImplementedException();
        }

        public void RequestPermission()
        {
            throw new NotImplementedException();
        }

        public void MakeDialog(int code)
        {
            throw new NotImplementedException();
        }
    }
}