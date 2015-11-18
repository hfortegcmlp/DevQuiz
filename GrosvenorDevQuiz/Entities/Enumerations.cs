using System;
using System.ComponentModel;
using System.Reflection;

namespace GrosvenorDevQuiz.Entities
{
    public class Enumerations
    {
        /// <summary>
        /// The available dish types
        /// </summary>
        public enum DishType
        {
            Entree = 1,
            Side = 2,
            Drink = 3,
            Dessert = 4
        }

        /// <summary>
        /// Times of day the restaurant is open
        /// </summary>
        public enum TimeOfDay
        {
            [Description("morning")]
            Morning,
            [Description("night")]
            Night
        }

        /// <summary>
        /// Fetches the description attribute of an enum
        /// </summary>
        /// <param name="value">Enum to get description from</param>
        /// <returns>value of Description attribute if exists, else null</returns>
        public static string GetDescription(Enum value)
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            if (name != null)
            {
                var field = type.GetField(name);
                if (field != null)
                {
                    var attr = Attribute.GetCustomAttribute(
                        field,
                        typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return null;
        }
    }
}
