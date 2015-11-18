using System;
using GrosvenorDevQuiz.Entities;

namespace GrosvenorDevQuiz.BusinessObjects
{
    /// <summary>
    /// Server takes an order and returns dishes to be made
    /// </summary>
    public class Server : IServer
    {
        private readonly IMealProcessor _mealProcessor;
        public Server()
        {
            //Extracted as interface for future extensibility, The Static Data implementation could be switched for one with a real datastore without 
            //affecting the rest of the application
            _mealProcessor = new MealProcessorStaticData();
        }

        /// <summary>
        /// Takes an order as a string in the format "timeOfDay, dish, dish, ... dish" and returns
        /// the dishes to be made
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public string TakeOrder(string order)
        {
            //splits string and removes whitespaces
            var parsedInput = ParseAndTrim(order);
            var timeOfDay = GetTimeOfDay(parsedInput[0]);
            if (timeOfDay == null)
            {
                return "";
            }

            //cast as non nullable is safe here, I like to keep the conversion close to the null check
            return GetDishes((Enumerations.TimeOfDay) timeOfDay, parsedInput);
        }

        /// <summary>
        /// Splits string by ',' and trims the individual items
        /// </summary>
        /// <param name="toParse"></param>
        /// <returns></returns>
        private string[] ParseAndTrim(string toParse)
        {
            var returnVal = toParse.Split(',');
            for (int i = 0; i < returnVal.Length; i++)
            {
                returnVal[i] = returnVal[i].Trim();
            }
            return returnVal;
        }

        /// <summary>
        /// Given a string, checks if it is a valid time of day
        /// </summary>
        /// <param name="timeOfDayToParse">sting to match against Enumeration.TimeOfDay description attribute</param>
        /// <returns>Enumeration.TimeOfDay if there is a match, else null</returns>
        private static Enumerations.TimeOfDay? GetTimeOfDay(string timeOfDayToParse)
        {
            timeOfDayToParse = timeOfDayToParse.Trim();
            foreach (Enumerations.TimeOfDay timeOfDay in Enum.GetValues(typeof(Enumerations.TimeOfDay)))
            {
                if(Enumerations.GetDescription(timeOfDay).Equals(timeOfDayToParse)) 
                {
                    return timeOfDay;
                }
            }
            return null;
        }

        /// <summary>
        /// Fetches the dishes to be prepared
        /// </summary>
        /// <param name="timeOfDay"></param>
        /// <param name="timeSlots">Array of numbers</param>
        /// <returns></returns>
        private string GetDishes(Enumerations.TimeOfDay timeOfDay, string[] timeSlots)
        {
            //Extracted as interface to provide the capabiltiy to use IOC to inject the MealProcessorStaticData
            _mealProcessor.Initialize(timeOfDay);

            //TODO: start at index 0
            for (var i = 1; i < timeSlots.Length; i++)
            {
                if (!_mealProcessor.AddDishType(timeSlots[i]))
                {
                    break;
                }
            }
            return _mealProcessor.GetMeals();
        }
    }
}
