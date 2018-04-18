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

namespace Recipizer.Models
{
    public class Ingredient
    {
        public String name { get; set; }
        public Double amount { get; set; }
        public enum Unit { g, kg, l, dl, ml, teaspoon, tablespoon, cup };

        public Ingredient()
        {

        }
    }
}