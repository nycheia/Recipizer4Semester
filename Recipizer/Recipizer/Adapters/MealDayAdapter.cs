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
    public class MealDayAdapter : BaseExpandableListAdapter
    {
        private readonly Context context;

        private List<Recipe> recipesList;

        private List<MealDay> mealDaysList;

        public MealDayAdapter(Context context, List<Recipe> list)
        {
            this.context = context;
            this.recipesList = list;
        }

        public override int GroupCount => throw new NotImplementedException();

        public override bool HasStableIds => throw new NotImplementedException();

        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            return null;
        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            return childPosition;
        }

        public override int GetChildrenCount(int groupPosition)
        {
            // TODO noget med count fra child list.
            return groupPosition;
        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            var view = convertView;

            if (view == null)
            {
                var inflater = context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
                view = inflater.Inflate(Resource.Layout.item_RecipeChild, null);
            }

            view.FindViewById<TextView>(Resource.Id.recAdapterTextViewRecTitle).Text = recipesList[childPosition].Title;
            return view;
        }

        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            return null;
        }

        public override long GetGroupId(int groupPosition)
        {
            return groupPosition;
        }

        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            var view = convertView;

            if (view == null)
            {
                var inflater = context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
                view = inflater.Inflate(Resource.Layout.item_MealDayGroup, null);
            }

            view.FindViewById<TextView>(Resource.Id.recAdapterTextViewRecTitle).Text = mealDaysList[groupPosition].dayID.ToString();
            return view;
        }

        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return true;
        }
    }
}