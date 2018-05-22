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
using Recipizer.Models;

namespace Recipizer.Adapters
{
    class ShareAdapter : BaseAdapter<Recipe>
    {

        Activity context;
        List<Recipe> recipes;

        public ShareAdapter(Activity context, List<Recipe> recipes)
            : base()
        {
            this.context = context;
            this.recipes = recipes;
        }
        public override int Count
        {
            get { return recipes.Count; }
        }

        public override Recipe this[int position]
        {
            get { return recipes[position]; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            if (view == null)
            {
                view = this.context.LayoutInflater.Inflate(Resource.Layout.item_share, parent, false);
            }

            view.FindViewById<TextView>(Resource.Id.textViewShareName).Text
                = recipes[position].Title;

            view.FindViewById<TextView>(Resource.Id.textViewShareType).Text
                = recipes[position].Type();



            return view;


        }

    }
}