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
using Java.Lang;
using Recipizer.Models;

namespace Recipizer.Adapters
{
    public class IngredientAdapter : ArrayAdapter
    {
        Activity context;
        List<Ingredient> ingredients;

        public IngredientAdapter(Activity context, List<Ingredient> ingredients) 
            : base(context, 0, ingredients)
        {
            this.context = context;
            this.ingredients = ingredients;
        }
        /*public override int Count
        {
            get { return ingredients. }
        }*/

        /*public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }*/

        /*public override long GetItemId(int position)
        {
            return position;
        }*/

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            if(view == null)
            {
                view = this.context.LayoutInflater.Inflate(Resource.Layout.item_ingredient, parent, false);
            }
            Ingredient ingredient = GetItem(position);

            TextView ingName = view.FindViewById<TextView>(Resource.Id.ingAdapterTextViewIngName);
            TextView ingAmount = view.FindViewById<TextView>(Resource.Id.ingAdapterTextViewIngAmount);


            ingName.Text = ingredient.name;
            ingAmount.Text = ingredient.amount + " " + ingredient.measuringUnit;

            if (ingredient.measuringUnit == Ingredient.Unit.non)
            {
                ingAmount.Text = ingredient.amount + " ";
            }
            return view;
        }
    }
}