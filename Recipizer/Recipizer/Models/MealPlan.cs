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
    public class MealPlan
    {
        //public List<Days> days;
        [NotNull]
        public DateTime startDate { get; set; }
        [NotNull]
        public DateTime endDate { get; set; }
        public String note { get; set; }
        public Boolean active { get; set; }
        [NotNull, MaxLength(200)]
        public String name { get; set; }

        public MealPlan()
        {

        }
    }
}