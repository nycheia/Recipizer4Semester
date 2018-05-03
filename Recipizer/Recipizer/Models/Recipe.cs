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
using Java.Util;

namespace Recipizer.Models
{
    public class Recipe
    {
        public List<Ingredient> ingredients;
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        [NotNull, MaxLength(300)]
        public String title { get; set; }
        [NotNull]
        public String description { get; set; }
        [NotNull]
        public String author { get; set; }
        [NotNull]
        public Date dateCreated { get; set; }
        [NotNull]
        public String note { get; set; }

        public Recipe()
        {
            
        }
    }
}