using System;
using System.Collections.Generic;
using GrosvenorDevQuiz.Entities;

namespace GrosvenorDevQuiz.BusinessObjects
{
    /// <summary>
    /// Initialized by timeOfDay.
    /// Stores business logic regarding what meals can be added
    /// Has internal error state that will not let you add meals after an error has been encountered.
    /// Has capability of outputting meals to be created
    /// Responsible for 
    /// </summary>
    internal class MealProcessorStaticData : IMealProcessor
    {
        private Dictionary<Enumerations.DishType, MealWithCount> _meals;
        private Enumerations.TimeOfDay _timeOfDay;
        private bool _encounteredError;

        /// <summary>
        /// resets the processor and makes it ready for a new batch of dishes
        /// </summary>
        /// <param name="timeOfDay"></param>
        public void Initialize(Enumerations.TimeOfDay timeOfDay)
        {
            _encounteredError = false;
            _timeOfDay = timeOfDay;
            _meals = GetMealsByPeriod(timeOfDay);
        }

        /// <summary>
        /// Tries to parse the dish type and adds it to the collection if successful
        /// </summary>
        /// <param name="dishType"></param>
        /// <returns>true if successfull, else false</returns>
        /// <throws>Exception if error has been encountered</throws>
        public bool AddDishType(string dishType)
        {
            if(_encounteredError)
            {
                throw new Exception("Error encountered - processing halted");
            }

            Enumerations.DishType dish;
            if (Enum.TryParse(dishType, out dish))
            {
                if (IsDishAllowed(dish))
                {
                    _meals[dish].Count++;
                    return true;
                } else
                {
                    _encounteredError = true;
                }
            }
            _encounteredError = true;
            return false;
        }

        /// <summary>
        /// Can be run any time after initialization
        /// </summary>
        /// <returns>The meals the be prepared</returns>
        public string GetMeals()
        {
            var output = new List<string>();
            foreach (var value in _meals.Values)
            {
                if (value.Count > 0)
                {
                    var tmp = value.Name;
                    if (value.Count > 1)
                    {
                        tmp = tmp + string.Format("(x{0})", value.Count);
                    }
                    output.Add(tmp);
                }
            }
            if (_encounteredError)
            {
                output.Add("error");
            }
            return string.Join(", ", output);
        }

        #region private

        /// <summary>
        /// returns true if dish exists in the meal period AND
        /// multiples are allowed or the current count is 0
        /// </summary>
        /// <param nameB="dish"></param>
        /// <returns></returns>
        private bool IsDishAllowed(Enumerations.DishType dish)
        {

            return _meals.ContainsKey(dish) && (
                _meals[dish].AllowMultiples ||
                _meals[dish].Count == 0);
        }

        /// <summary>
        /// Fetches the allowed meals for a given meal period
        /// This function is the only one that contains data which could easily be extracted out to a datastore
        /// 
        /// </summary>
        /// <param name="timeOfDay"></param>
        /// <returns></returns>
        private Dictionary<Enumerations.DishType, MealWithCount> GetMealsByPeriod(Enumerations.TimeOfDay timeOfDay)
        {
            var meals = new Dictionary<Enumerations.DishType, MealWithCount>();
            if (timeOfDay == Enumerations.TimeOfDay.Morning)
            {
                meals.Add(Enumerations.DishType.Entree, new MealWithCount { DishType = Enumerations.DishType.Entree, Name = "eggs" });
                meals.Add(Enumerations.DishType.Side, new MealWithCount { DishType = Enumerations.DishType.Side, Name = "toast" });
                meals.Add(Enumerations.DishType.Drink, new MealWithCount { DishType = Enumerations.DishType.Drink, Name = "coffee", AllowMultiples = true });
            } else
            {
                meals.Add(Enumerations.DishType.Entree, new MealWithCount { DishType = Enumerations.DishType.Entree, Name = "steak" });
                meals.Add(Enumerations.DishType.Side, new MealWithCount { DishType = Enumerations.DishType.Side, Name = "potato", AllowMultiples = true });
                meals.Add(Enumerations.DishType.Drink, new MealWithCount { DishType = Enumerations.DishType.Drink, Name = "wine" });
                meals.Add(Enumerations.DishType.Dessert, new MealWithCount { DishType = Enumerations.DishType.Dessert, Name = "cake" });
            }
            return meals;
        }

        private class MealWithCount : Meal
        {
            public MealWithCount()
            {
                Count = 0;
            }
            public int Count { get; set; }

        }
        #endregion
    }
}
