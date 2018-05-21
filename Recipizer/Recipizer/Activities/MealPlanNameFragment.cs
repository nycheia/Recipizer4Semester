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
    public class MealPlanNameFragment : DialogFragment
    {
        private readonly Context context;
        private readonly string title;
        public delegate void clickedOK(string name);
        public clickedOK clicked;

        public MealPlanNameFragment(Context context, string title)
        {
            this.context = context;
            this.title = title;

        }
        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            var inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
            var view = inflater.Inflate(Resource.Layout.MealPlanName, null);
            var name = view.FindViewById<EditText>(Resource.Id.nameText);


            var dialog = new AlertDialog.Builder(context);
            dialog.SetTitle(title);
            dialog.SetView(view);
            dialog.SetPositiveButton("ok", (s, a) => 
            {
                clicked(name.Text);
            });
            return dialog.Create();

        } 
    }
}