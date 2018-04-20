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
    public class IngredientAdapter : BaseAdapter<Ingredient>
    {
        Activity context;
        List<Ingredient> ingredients;

        public IngredientAdapter(Activity context, List<Ingredient> ingredients) 
            : base()
        {
            this.context = context;
            this.ingredients = ingredients;
        }
        public override int Count
        {
            get { return ingredients.Count; }
        }

        public override Ingredient this[int position]
        {
            get { return ingredients[position]; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            if(view == null)
            {
                view = this.context.LayoutInflater.Inflate(Resource.Layout.item_ingredient, parent, false);
            }
            //Ingredient ingredient = GetItem(position);

            /*TextView ingName = view.FindViewById<TextView>(Resource.Id.ingAdapterTextViewIngName);
            TextView ingAmount = view.FindViewById<TextView>(Resource.Id.ingAdapterTextViewIngAmount);*/

            view.FindViewById<TextView>(Resource.Id.ingAdapterTextViewIngName).Text = ingredients[position].name;
            view.FindViewById<TextView>(Resource.Id.ingAdapterTextViewIngAmount).Text 
                = ingredients[position].amount + " " + ingredients[position].measuringUnit;

            /*ingName.Text = ingredient.name;
            ingAmount.Text = ingredient.amount + " " + ingredient.measuringUnit;

            if (ingredient.measuringUnit == Ingredient.Unit.non)
            {
                ingAmount.Text = ingredient.amount + " ";
            }*/
            return view;
        }
    }
}