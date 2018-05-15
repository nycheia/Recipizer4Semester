using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Recipizer.Presenters;

namespace Recipizer.Activities
{
    [Activity(Label = "Recipizer", MainLauncher = true)]
    public class MainActivity : Activity, IRecipizerView
    {
        private MainPresenter presenter;

        public void FinishView(Result result, Intent data)
        {
            throw new System.NotImplementedException();
        }

        public void MakeToast(string text, ToastLength length)
        {
            throw new System.NotImplementedException();
        }

        public void Navigate()
        {
            throw new System.NotImplementedException();
        }

        public void Navigate(int code, Intent data)
        {
            throw new System.NotImplementedException();
        }

        public void RequestPermission()
        {
            throw new System.NotImplementedException();
        }

        public void ResetText()
        {
            throw new System.NotImplementedException();
        }

        public void SetupView()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateView()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            presenter = new MainPresenter(this);
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            presenter.onCreate();
            //Initialize UI Components
            Button btnRecipeNav = FindViewById<Button>(Resource.Id.btnRecipeNav);
            Button btnMealPlanNav = FindViewById<Button>(Resource.Id.btnMealPlanNav);
            Button btnShopListNav = FindViewById<Button>(Resource.Id.btnShopListNav);


            //UI interaction
            btnRecipeNav.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(RecipesActivity));
                StartActivity(intent);
            };

            btnMealPlanNav.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(MealPlanActivity));
                StartActivity(intent);
            };

            btnShopListNav.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(ShoppingListActivity));
                StartActivity(intent);
            };
        }
    }
}

