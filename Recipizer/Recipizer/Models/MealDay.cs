﻿using System;
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
    public class MealDay
    {
        public List<string> meals { get; set; }
        public List<Recipe> recipes { get; set; }
        public string note { get; set; }
        public int dayID { get; set; }
    }
}