using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace Recipizer.Activities
{
    [Activity(Label = "Recipizer", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

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

