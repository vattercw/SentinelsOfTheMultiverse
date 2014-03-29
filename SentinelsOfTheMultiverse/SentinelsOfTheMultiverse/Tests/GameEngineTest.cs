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
            GameEngine testEngine = new GameEngine();

            bool pass = (testEngine.startScreen != null) && (testEngine.players != null)
                     && (testEngine.heroes != null) && (testEngine.villain != null) &&
                        (testEngine.environment != null);
            Assert.True(pass);
        }
    }
}
