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
    [Table("MealPlan")]
    public class MealPlan
    {
        //public List<Days> days;
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public DateTime startDate { get; set; }
        public int amountOfDays { get; set; }
        public string note { get; set; }
        public bool active { get; set; }
        public string name { get; set; }
        public List<MealDay> mealDays;
        public MealPlan()
        {
            mealDays = new List<MealDay>();
        }

    }
}