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

        Button createBtn;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MealPlan);
            // instansiating presenter
            presenter = new MealPlanPresenter(this);
            // setting up UI components
            listViewMealPlan = FindViewById<ListView>(Resource.Id.ListMealPlan);
            createBtn = FindViewById<Button>(Resource.Id.btnAddMealList);

            //setting up adapter
            mealPlanAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleExpandableListItem1, presenter.mealPlanList);
            listViewMealPlan.Adapter = mealPlanAdapter;

            listViewMealPlan.ItemClick += (sender, e) => {
                presenter.mealPlan_OnClick(e.Position);
            };

            createBtn.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(CreateMealPlanActivity));
                StartActivity(intent);
            };



            //presenter.onCreate();
        }

        protected override void OnResume()
        {
            base.OnResume();
            presenter.onResume();
        }

        public void FinishView(Result result, Intent intent)
        {
            throw new NotImplementedException();
        }

        public void MakeToast(string text, ToastLength length)
        {
            Toast.MakeText(this, text, length).Show();
        }

        public void Navigate(int code, Intent data)
        {
            if(code == 1)
            {
                Intent intent = new Intent(this, typeof(ViewMealPlanActivity));
                intent.PutExtras(data);
                StartActivityForResult(intent, 1);
            }
        }

        public void ResetText()
        {
            throw new NotImplementedException();
        }

        public void UpdateView()
        {
            mealPlanAdapter.Clear();
            mealPlanAdapter.AddAll(presenter.mealPlanList);
            mealPlanAdapter.NotifyDataSetChanged();
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