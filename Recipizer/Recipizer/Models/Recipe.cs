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
    public class Recipe
    {
        public List<Ingredient> Ingredients;
        public string Title { get; set; }
        public string Description { get; set; }
        //public String Author { get; set; }
        public DateTime DateCreated { get; set; }
        public string Note { get; set; }

        public Recipe(List<Ingredient> _Ingredients, string _Title, string _Description, DateTime _DateCreated)
        {
            Ingredients = _Ingredients;
            Title = _Title;
            Description = _Description;
            DateCreated = _DateCreated;
        }
    }
}