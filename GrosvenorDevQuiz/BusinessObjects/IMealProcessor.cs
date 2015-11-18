using GrosvenorDevQuiz.Entities;
namespace GrosvenorDevQuiz.BusinessObjects
{
    public interface IMealProcessor
    {
        /// <summary>
        /// Tries to parse the dish type and adds it to the collection if successful
        /// </summary>
        /// <param name="dishType"></param>
        /// <returns>true if successfull, else false</returns>
        /// <throws>Exception if error has been encountered</throws>
        bool AddDishType(string dishType);

        /// <summary>
        /// Can be run any time after initialization
        /// </summary>
        /// <returns>The meals the be prepared</returns>
        string GetMeals();

        /// <summary>
        /// resets the processor and makes it ready for a new batch of dishes
        /// </summary>
        /// <param name="timeOfDay"></param>
        void Initialize(Enumerations.TimeOfDay timeOfDay);
    }
}