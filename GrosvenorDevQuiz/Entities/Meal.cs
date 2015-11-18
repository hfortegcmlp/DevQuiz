namespace GrosvenorDevQuiz.Entities
{
    /// <summary>
    /// Meal is the only entity that should be stored in a database, enabling the application to pull dishes by TimeOfDay
    /// For this to be the case, TimeOfDay would have to be added to the entity, or to a secondary table if it is decided that
    /// Meal can belong to many TimeOfDays
    /// </summary>
    class Meal
    {
        public Meal()
        {
            AllowMultiples = false;
        }

        public Enumerations.DishType DishType { get; set; }
        public string Name { get; set; }
        public bool AllowMultiples { get; set; }
    }
}
