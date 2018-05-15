using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Recipizer.Models
{
    [Table("ShoppingList")]
    public class ShoppingItem
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        //public List<Ingredient> Ingredients;
        //public List<string> ShoppingListItems;
        public string ShoppingListItem { get; set; }

        public ShoppingItem()
        {

        }

        public ShoppingItem(string _ShoppingListItem)
        {
            this.ShoppingListItem = _ShoppingListItem;
        }
    }
}