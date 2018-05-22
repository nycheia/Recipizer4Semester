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
    [Activity(Label = "ViewMealDay")]
    public class ViewMealDayActivity : Activity, IRecipizerView
    {
        ListView listRecipes;
        ViewMealDayPresenter presenter;
        ArrayAdapter<string> recipeAdapter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ViewMealDay);
            // Create your application here
            presenter = new ViewMealDayPresenter(this);

            listRecipes = FindViewById<ListView>(Resource.Id.RecipesListMealDay);
            recipeAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleExpandableListItem1, presenter.recipes.Keys.ToList());
            listRecipes.Adapter = recipeAdapter;

            presenter.PopulateRecipeList(Intent.GetIntExtra("id", 0));

            listRecipes.ItemClick += (sender, e) =>
            {
                presenter.Recipe_onClick(recipeAdapter.GetItem(e.Position));
            };
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
            Intent intent = new Intent(this, typeof(ViewRecipeActivity));
            intent.PutExtras(data);
            StartActivity(intent);

        }

        public void ResetText()
        {
            throw new NotImplementedException();
        }

        public void UpdateView()
        {
            recipeAdapter.Clear();
            recipeAdapter.AddAll(presenter.recipes.Keys);
            recipeAdapter.NotifyDataSetChanged();
        }
    }
}