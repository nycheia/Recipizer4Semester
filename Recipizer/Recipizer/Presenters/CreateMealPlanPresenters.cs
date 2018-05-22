using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Recipizer.Models;
using Recipizer.Activities;

namespace Recipizer.Presenters
{
    public class CreateMealPlanPresenters : IPresenter
    {

        public Dictionary<string, Recipe> recipeDictionary;
        IRecipizerView view;
        public Dictionary<string, Recipe> pickedRecipeDictionary;
        public MealPlan mp;
        public int counter = 1;

        public CreateMealPlanPresenters(IRecipizerView _view)
        {
            this.view = _view;
            recipeDictionary = new Dictionary<string, Recipe>();
            pickedRecipeDictionary = new Dictionary<string, Recipe>();
            loadRecipesFromStorage();
        }

        public void onActivityResult(int requestCode, Result resultCode, Intent data)
        {
            throw new NotImplementedException();
        }

        public void onBackPressed()
        {
            throw new NotImplementedException();
        }

        public void onCreate()
        {
            mp = new MealPlan();
        }

        public void onDestroy()
        {
            throw new NotImplementedException();
        }

        public void onPause()
        {
            throw new NotImplementedException();
        }

        public void onResume()
        {
            throw new NotImplementedException();
        }

        public void loadRecipesFromStorage()
        {
            foreach (Recipe item in Constants.Conn.Table<Recipe>().ToList())
            {
                recipeDictionary.Add(item.Title, item);
            }
        }

        public void MealPlanAmountOfDays(int days)
        {
            mp.amountOfDays = days;
            view.UpdateView();

        }

        public void ChooseRecipe_OnList(string key)
        {
            //TODO lav exception så man ikke vælger 2 af samme recipe (giver exception haHAA)
            pickedRecipeDictionary.Add(key, recipeDictionary.GetValueOrDefault(key));
            view.UpdateView();
        }

        public void OnClickAddMealDay()
        {
            if(counter != mp.amountOfDays)
            {
                MealDay md = new MealDay();
                md.day = counter;
                md.recipes = pickedRecipeDictionary.Values.ToList();
                mp.mealDays.Add(md);
                pickedRecipeDictionary.Clear();
                counter++;
                view.UpdateView();
            }
            else
            {
                if(mp.startDate != null)
                {
                    MealDay md = new MealDay();
                    md.day = counter;
                    md.recipes = pickedRecipeDictionary.Values.ToList();
                    mp.mealDays.Add(md);
                    Constants.Conn.Insert(mp);
                    foreach  (MealDay item in mp.mealDays)
                    {
                        item.mealPlanId = mp.id;
                        Constants.Conn.Insert(item);
                        foreach (Recipe i in item.recipes)
                        {
                            MealDayRecipe mdr = new MealDayRecipe();
                            mdr.recipeId = i.id;
                            mdr.mealDayId = item.id;
                            Constants.Conn.Insert(mdr);
                        }
                    }   
                }
            }
            view.UpdateView();
        }

        public void SetStartDate(DateTime day)
        {
            if (day != null)
            {
                mp.startDate = day;
            }
        }

        public void SetName(string name)
        {
            mp.name = name;
        }
    }
}