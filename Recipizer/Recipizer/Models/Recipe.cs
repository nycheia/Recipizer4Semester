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
using Java.Util;

namespace Recipizer.Models
{
    public class Recipe
    {
        public List<Ingredient> ingredients;
        public String title { get; set; }
        public String description { get; set; }
        public String author { get; set; }
        public Date dateCreated { get; set; }
        public String note { get; set; }

        public Recipe()
        {
            
        }
    }
}