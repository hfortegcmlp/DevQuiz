namespace GrosvenorDevQuiz.BusinessObjects
{

    /// <summary>
    /// Simple interface for server to take an order.
    /// </summary>
    public interface IServer
    {
        /// <summary>
        /// Takes an order as a string in the format "timeOfDay, dishType, dishType, ... dishType" and returns
        /// the dishes to be made
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        string TakeOrder(string order);
    }
}