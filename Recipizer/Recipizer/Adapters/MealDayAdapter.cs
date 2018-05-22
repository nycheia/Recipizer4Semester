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

        private Dictionary<string, List<Recipe>> recipes;

        private List<string> mealDaysList;

        public MealDayAdapter(Context context, Dictionary<string, List<Recipe>> recipes, List<string> mealDays)
        {
            this.context = context;
            this.recipes = recipes;
            this.mealDaysList = mealDays;
        }

        public override int GroupCount
        {
            get { return mealDaysList.Count(); }
        }

        public override bool HasStableIds => false;

        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            return this.recipes[mealDaysList[groupPosition]][childPosition].ToString();
        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            return childPosition;
        }

        public override int GetChildrenCount(int groupPosition)
        {
            return recipes.Count();
        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            var view = convertView;

            if (view == null)
            {
                var inflater = context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
                view = inflater.Inflate(Resource.Layout.item_RecipeChild, null);
            }

            view.FindViewById<TextView>(Resource.Id.recAdapterTextViewRecTitle).Text = this.recipes[this.mealDaysList[groupPosition]][childPosition].ToString();
            return view;
        }

        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            return mealDaysList[groupPosition];
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

            view.FindViewById<TextView>(Resource.Id.recAdapterTextViewRecTitle).Text = mealDaysList[groupPosition];
            return view;
        }

        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return true;
        }
    }
}