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
    [Table("MealDayRecipe")]
    public class MealDayRecipe
    {
        [PrimaryKey, AutoIncrement]    
        public int id { get; set; }

        public int recipeId { get; set; }

        public int mealDayId { get; set; }
    }
}