using System.Collections.Generic;
using System.Linq;
using GrosvenorDevQuiz.BusinessObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GrosVenorDevQuizTests
{
    [TestClass]
    public class IntegrationTests
    {
        #region test as per requirements
        #region resources

        private readonly Server _server = new Server();

        /// <summary>
        /// Enhancement: could make this a resource.
        /// </summary>
        private readonly string[] _inputs = new[]
        {
            "morning, 1, 2, 3",
            "morning, 2, 1, 3",
            "morning, 1, 2, 3, 4",
            "morning, 1, 2, 3, 3, 3",
            "night, 1, 2, 3, 4",
            "night, 1, 2, 2, 4",
            "night, 1, 2, 3, 5",
            "night, 1, 1, 2, 3, 5"
        };

        private readonly string[] _outputs = new[]
        {
            "eggs, toast, coffee",
            "eggs, toast, coffee",
            "eggs, toast, coffee, error",
            "eggs, toast, coffee(x3)",
            "steak, potato, wine, cake",
            "steak, potato(x2), cake",
            "steak, potato, wine, error",
            "steak, error"
        };

        private Dictionary<int, string> GetMorningDishes()
        {
            var morningDishes = new Dictionary<int, string> { { 1, "eggs" }, { 2, "toast" }, { 3, "coffee" } };

            return morningDishes;
        }

        private Dictionary<int, string> GetEveningDishes()
        {
            var eveningDishes = new Dictionary<int, string> { { 1, "steak" }, { 2, "potato" }, { 3, "wine" }, { 4, "cake" } };

            return eveningDishes;
        }

        
        #endregion

        /// <summary>
        /// Tests the requirements given in the specifications
        /// </summary>
        [TestMethod]
        public void TestRequirements()
        {
            var i = 0;
            foreach (var returnVal in _inputs.Select(_server.TakeOrder))
            {
                Assert.AreEqual( _outputs[i], returnVal);
                i++;
            }
        }
        #endregion

        #region positive testing
        [TestMethod]
        public void ServerReturnsEmptyStringWhenOnlyOneArgumentIsPassed()
        {
            const string input = "morning";
            var returnVal = _server.TakeOrder(input);
            Assert.AreEqual("", returnVal);
        }


        [TestMethod]
        public void CanGetAnyIndividualItemInTheMorning()
        {
            var morningDishes = GetMorningDishes();
            for (var i = 1; i <= 3; i++)
            {
                var order = _server.TakeOrder(string.Format("morning, {0}", i));
                Assert.AreEqual(morningDishes[i], order);  
            }
        }
        
        [TestMethod]
        public void CanGetAnyIndividualItemAtNight()
        {
            var eveningDishes = GetEveningDishes();
            for (var i = 1; i <= 4; i++)
            {
                var order = _server.TakeOrder(string.Format("night, {0}", i));
                Assert.AreEqual(eveningDishes[i], order);
            }
        }

        [TestMethod]
        public void OrderingDesertInTheEveningReturnsError()
        {
            const string input = "morning, 4";
            var returnVal = _server.TakeOrder(input);
            Assert.AreEqual("error", returnVal);
        }
        
        [TestMethod]
        public void CanGetSteakAtnight()
        {
            const string input = "night, 1";
            var returnVal = _server.TakeOrder(input);
            Assert.AreEqual("steak", returnVal);
        }
        #endregion

        #region negative testing
        [TestMethod]
        public void HandlesWeirdCharacters()
        {
            var weirdChars = new[]
            {
                "?",
                "!",
                "@",
                "汉语",
                "ä",
            };
            var i = 0;
            foreach (var returnVal in weirdChars.Select(_server.TakeOrder))
            {
                Assert.AreEqual("", returnVal);
                i++;
            }
        }

        [TestMethod]
        public void HandlesDoubleComma()
        {
            const string input = "morning, 1, , 2";
            var returnVal = _server.TakeOrder(input);
            Assert.AreEqual("eggs, error", returnVal);
        }

        [TestMethod]
        public void HandlesWeirdSpacing()
        {
            const string input = "morning    , 1  , 2";
            var returnVal = _server.TakeOrder(input);
            Assert.AreEqual("eggs, toast", returnVal);
        }

        #endregion
    }
}
