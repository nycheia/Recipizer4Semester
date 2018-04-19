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
    public class Ingredient
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string name { get; set; }
        public string amount { get; set; }
        public Unit measuringUnit { get; set; }
        public enum Unit { non, g, kg, l, dl, ml, tsps, tbsp, cup };

        public Ingredient(string name, string amount, Unit measuringUnit)
        {
            this.name = name;
            this.amount = amount;
            this.measuringUnit = measuringUnit;
        }

        public static Unit StringToUnit(string s)
        {
            Unit res;

            switch (s.ToLower())
            {
                case "g":
                    res = Unit.g;
                    break;

                case "kg":
                    res = Unit.kg;
                    break;

                case "l":
                    res = Unit.l;
                    break;

                case "dl":
                    res = Unit.dl;
                    break;

                case "ml":
                    res = Unit.ml;
                    break;

                case "tsps":
                    res = Unit.tsps;
                    break;

                case "tbsp":
                    res = Unit.tbsp;
                    break;

                case "cup":
                    res = Unit.cup;
                    break;

                default:
                    res = Unit.non;
                    break;
            }

            return res;

        }
    }
}