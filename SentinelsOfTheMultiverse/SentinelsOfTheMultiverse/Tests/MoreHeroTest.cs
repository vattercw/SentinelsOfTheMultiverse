using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SentinelsOfTheMultiverse.Data;

namespace SentinelsOfTheMultiverse.Tests
{
    [TestFixture]
    class MoreHeroTest
    {
        [Test, RequiresSTA]
        public void TestInitialization()
        {
            Start test = new Start();
            test.beginGame();
            int originalHand = GameEngine.getHeroes()[0].hand.Count;
            GameEngine.getHeroes()[0].playerTurn(true, true);
            int afterHand = GameEngine.getHeroes()[0].hand.Count;
            Assert.AreEqual(originalHand + 1, afterHand);

            originalHand = GameEngine.getHeroes()[1].hand.Count;
            GameEngine.getHeroes()[1].playerTurn(false, false);
            afterHand = GameEngine.getHeroes()[1].hand.Count;
            Assert.AreEqual(originalHand + 2, afterHand);


        }
    }
}
