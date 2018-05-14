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
using Recipizer.Activities;
using Recipizer.Models;

namespace Recipizer.Presenters
{
    public class ShoppingListPresenter : IPresenter
    {
        IRecipizerView view;

        public List<string> shoppingListList;

        public ShoppingListPresenter(IRecipizerView _view)
        {
            this.view = _view;
            shoppingListList = new List<string>();
        }

        public void AddShoppingListItem(string shopListItem)
        {
            ShoppingList shopList = new ShoppingList();
            shoppingListList.Add(shopListItem);
            view.UpdateView();
            view.ResetText();

        }

        public void ClearList()
        {
            //somethingsomethingdatabasedatabase det her er bare for at minde mig om det
            shoppingListList.Clear();
        }
        public void onActivityResult(int requestCode, Result resultCode, Intent data)
        {
            throw new NotImplementedException();
        }

        public void onBackPressed()
        {
            throw new NotImplementedException();
        }

        public void onCreate()
        {
            throw new NotImplementedException();
        }

        public void onDestroy()
        {
            throw new NotImplementedException();
        }

        public void onPause()
        {
            throw new NotImplementedException();
        }

        public void onResume()
        {
            throw new NotImplementedException();
        }
    }
}