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
using SQLite;

namespace Recipizer.Models
{
    [Table("Recipe")]
    public class Recipe
    {
        public List<Ingredient> Ingredients;
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(2000)]
        public string Description { get; set; }
        //public String Author { get; set; }
        [NotNull]
        public DateTime DateCreated { get; set; }
        [MaxLength(2000)]
        public string Note { get; set; }

        public Recipe()
        {
            
        }

        public void SetIngredients()
        {
            Ingredients = (from i in Constants.Conn.Table<Ingredient>() where i.RecipeId == id select i).ToList();
        }

        public Recipe(List<Ingredient> _Ingredients, string _Title, string _Description, DateTime _DateCreated)
        {
            Ingredients = _Ingredients;
            Title = _Title;
            Description = _Description;
            DateCreated = _DateCreated;
        }
    }
}