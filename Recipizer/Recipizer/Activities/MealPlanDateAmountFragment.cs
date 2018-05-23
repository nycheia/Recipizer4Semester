using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Recipizer.Activities
{
    public class MealPlanDateAmountFragment : DialogFragment
    {
        private readonly Context context;
        private readonly string title;
        public static int current;
        public delegate void clickedOK(int days);
        public clickedOK clicked;

        public MealPlanDateAmountFragment(Context context, string title)
        {
            this.context = context;
            this.title = title;

        }
        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            var inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
            var view = inflater.Inflate(Resource.Layout.MealPlanDateAmount, null);
            var amountOfDays = view.FindViewById<EditText>(Resource.Id.DateAmountText);
            amountOfDays.Text = "1";


            var dialog = new AlertDialog.Builder(context);
            dialog.SetTitle(title);
            dialog.SetView(view);
            dialog.SetPositiveButton("ok", (s, a) => 
            {
                if (!int.TryParse(amountOfDays.Text, out current))
                {
                    current = 1;
                };
                clicked(current);
            });
            return dialog.Create();

        } 
    }
}