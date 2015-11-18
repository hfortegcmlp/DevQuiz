using System;
using GrosvenorDevQuiz.BusinessObjects;

namespace GrosvenorDevQuiz
{
    /// <summary>
    /// TODO: Create entities
    /// TODO:Enable parser to 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            IServer server = new Server();
            Console.WriteLine("Add your order or q for quit");
            var order = Console.ReadLine();
            while (!order.ToLower().Equals("q"))
            {
                var food = server.TakeOrder(order);
                Console.WriteLine(food);

                Console.WriteLine("");
                Console.WriteLine("Add your order or q for quit");
                order = Console.ReadLine();
            }
        }
    }
}