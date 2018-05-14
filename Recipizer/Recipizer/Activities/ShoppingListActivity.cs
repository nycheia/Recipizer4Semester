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
    [Activity(Label = "ShoppingListActivity")]
    public class ShoppingListActivity : Activity, IRecipizerView
    {
        //UI components
        EditText editTextAddToShoppingList;
        ListView listViewShoppingList;

        //Adapters
        ArrayAdapter<string> shoppingListAdapter;

        //Presenter
        ShoppingListPresenter presenter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ShoppingList);

            //Instatiating the presenter
            presenter = new ShoppingListPresenter(this);

            //Retrieving UI components
            editTextAddToShoppingList = FindViewById<EditText>(Resource.Id.editText_AddToShoppingList);
            Button btnAddShopIngredient = FindViewById<Button>(Resource.Id.btnAddShopIngredient);
            Button btnClearShopList = FindViewById<Button>(Resource.Id.btnClearShopList);
            listViewShoppingList = FindViewById<ListView>(Resource.Id.listViewShoppingList);

            //Adapter
            shoppingListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, presenter.shoppingListList);
            listViewShoppingList.Adapter = shoppingListAdapter;

            //Button Events
            btnAddShopIngredient.Click += (object sender, EventArgs e) =>
            {
                presenter.AddShoppingListItem(editTextAddToShoppingList.Text);
            };

            btnClearShopList.Click += (object sender, EventArgs e) =>
            {
                //ClearListView
                //presenter.ClearList
            };

        }

        public void ClearListView()
        {
            shoppingListAdapter.Clear();
            
            //clear list in database too
            //UpdateView();
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
            
        }

        public void ResetText()
        {
            editTextAddToShoppingList.Text = "";
        }

        public void SetupView()
        {
            throw new NotImplementedException();
        }

        public void UpdateView()
        {
            shoppingListAdapter.NotifyDataSetChanged();
        }

    }
}