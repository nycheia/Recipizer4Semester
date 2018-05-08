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
using Recipizer.Models;

namespace Recipizer.Activities
{
    [Activity(Label = "ViewRecipeActivity")]
    public class ViewRecipeActivity : Activity, IRecipizerView
    {

        //UI components

        //Adapters

        //Variable to connect to presenter
        ViewRecipePresenter presenter;

        //Recipe instance
        Recipe CurrentRecipe;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ViewRecipe);

            //Instantiate the presenter
            presenter = new ViewRecipePresenter(this);

            //Get the Recipe object
            //TODO get from DB
            //Temp.tempContainer.instance.RecipieContainer[1];

            //Get UI components for global use.

            //Get UI components for local use.

            //Setup lists.


            //Setup Click Events.

            //Call the presenters OnCreate Method.
            presenter.onCreate();
        }

        public void FinishView(Result result) { }

        public void MakeToast(string text, ToastLength length)
        {
            Toast.MakeText(this, text, length).Show();
        }

        public void Navigate(int code) { }

        public void ResetText() { }

        public void UpdateView() { }
    }
}