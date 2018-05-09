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
    public class IngredientButtonAdapter : BaseAdapter<Ingredient>
    {
        Activity context;
        List<Ingredient> ingredients;

        public IngredientButtonAdapter(Activity context, List<Ingredient> ingredients) 
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
                view = this.context.LayoutInflater.Inflate(Resource.Layout.item_ingredient_button, parent, false);
            }
            //Ingredient ingredient = GetItem(position);

            /*TextView ingName = view.FindViewById<TextView>(Resource.Id.ingAdapterTextViewIngName);
            TextView ingAmount = view.FindViewById<TextView>(Resource.Id.ingAdapterTextViewIngAmount);*/

            view.FindViewById<TextView>(Resource.Id.ingAdapterTextViewIngName).Text = ingredients[position].name;
            view.FindViewById<TextView>(Resource.Id.ingAdapterTextViewIngAmount).Text 
                = ingredients[position].amount + " " + ingredients[position].measuringUnit;

            void removeListItemsClickEvent(object sender, EventArgs e)
            {
                
                //ingredients.RemoveAt(position);
                ingredients.Remove(ingredients[position]);
                NotifyDataSetChanged();
            };
            
            Button btnIngredientAdapterDelete = view.FindViewById<Button>(Resource.Id.btnIngredientAdapterDelete);
            //btnIngredientAdapterDelete.Click -= removeListItemsClickEvent;
            //It prevents multiple events
            if (!btnIngredientAdapterDelete.HasOnClickListeners)
            {
                btnIngredientAdapterDelete.Click += removeListItemsClickEvent;
            }

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