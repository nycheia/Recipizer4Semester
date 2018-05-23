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
            LoadShoppingListFromDatabase();
        }

        public void AddShoppingListItem(string shopListItem)
        {
            ShoppingItem shopItem = new ShoppingItem(shopListItem);
            shoppingListList.Add(shopListItem);
            view.UpdateView();
            view.ResetText();

            Constants.Conn.Insert(shopItem);
        }

        public void ClearList()
        {
            shoppingListList.Clear();
            Constants.Conn.DeleteAll<ShoppingItem>();
            view.UpdateView();
        }

        public void LoadShoppingListFromDatabase()
        {
            foreach(ShoppingItem shopItem in Constants.Conn.Table<ShoppingItem>())
            {
                shoppingListList.Add(shopItem.ShoppingListItem);
            }
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