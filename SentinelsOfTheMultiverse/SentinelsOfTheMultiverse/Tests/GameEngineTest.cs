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

            bool pass =(GameEngine.getPlayers() != null)
                     && (GameEngine.getHeroes() != null) && (GameEngine.getVillain() != null) &&
                        (GameEngine.getEnvironment() != null);
            Assert.True(pass);
        }
    }
}
