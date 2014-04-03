using NUnit.Framework;
using SentinelsOfTheMultiverse.Data.Environments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse.Tests
{
    [TestFixture]
    class EnvironmentTest
    {

        

        [Test, RequiresSTA]
        public void testPlayerTurn()
        {
            InsulaPrimus env = new InsulaPrimus();
            var numBeforeEnd = env.deck.getCards().Count;
            env.playerTurn();
            Assert.AreNotEqual(numBeforeEnd, env.deck.getCards().Count);
        }

        [Test, RequiresSTA]
        public void testInitialization()
        {
            InsulaPrimus env = new InsulaPrimus();
            Assert.NotNull(env.inPlay);
            Assert.NotNull(env.deck);
        }
    }
}
