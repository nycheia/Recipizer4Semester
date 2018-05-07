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
    public class MealPlan
    {
        //public List<Days> days;
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string note { get; set; }
        public bool active { get; set; }
        public string name { get; set; }

        public MealPlan()
        {

        }
    }
}