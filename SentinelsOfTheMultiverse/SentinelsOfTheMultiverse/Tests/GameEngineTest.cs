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
    class GameEngineTest
    {
        [Test, RequiresSTA]
        public void TestInitialization()
        {
            //GameEngine testEngine = new GameEngine();
            Start test = new Start();
            test.beginGame();
            bool pass =(GameEngine.getPlayers() != null)
                     && (GameEngine.getHeroes() != null) && (GameEngine.getVillain() != null) &&
                        (GameEngine.getEnvironment() != null);
            Assert.True(pass);
        }

        [Test, RequiresSTA]
        public void TestNextTurn()
        {
            Start test = new Start();
            test.beginGame();

            Assert.AreEqual(GameEngine.getHeroes()[0], GameEngine.getCurrentPlayer());
            GameEngine.nextTurn();
            Assert.AreEqual(GameEngine.getHeroes()[1], GameEngine.getCurrentPlayer());
        }
    }
}
