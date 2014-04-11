using NUnit.Framework;
using SentinelsOfTheMultiverse.Data.Environments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SentinelsOfTheMultiverse.Data;

namespace SentinelsOfTheMultiverse.Tests
{
    [TestFixture]
    class EnvironmentTest
    {

        

        [Test, RequiresSTA]
        public void testPlayerTurn()
        {
            GameEnvironment env = ObjectMother.TestEnvironment();
            var numBeforeEnd = env.deck.cards.Count;
            env.playerTurn();
            Assert.AreNotEqual(numBeforeEnd, env.deck.cards.Count);
        }

        [Test, RequiresSTA]
        public void testInitialization()
        {
            GameEnvironment env = ObjectMother.TestEnvironment();
            Assert.NotNull(env.cardsOnField);
            Assert.NotNull(env.deck);
        }
    }
}
