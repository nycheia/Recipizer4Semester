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

namespace Recipizer
{
    [Activity(Label = "CreateRecipeActivity")]
    public class CreateRecipeActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CreateRecipe);
            // Create your application here

            //Initialize UI components
            Spinner spinnerUnits = FindViewById<Spinner>(Resource.Id.spinnerUnits);

            //UI interaction
            spinnerUnits.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinnerUnits_ItemSelected);
            var unitAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.unit_array, Android.Resource.Layout.SimpleSpinnerItem);
            unitAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerItem);
            spinnerUnits.Adapter = unitAdapter;
        }

        private void spinnerUnits_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinnerUnits = (Spinner)sender;
            //string toast = string.Format("Measuring unit: {0}", spinnerUnits.GetItemAtPosition(e.Position));
            //Toast.MakeText(this, toast, ToastLength.Long).Show();
        }
    }
}