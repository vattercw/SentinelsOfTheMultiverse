using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SentinelsOfTheMultiverse.Data.Heroes;

namespace SentinelsOfTheMultiverse.Tests
{
    [TestFixture]
    class HeroTest
    {
        string[] files = { "dominion", "elbow_smash", "enduring_intercession", "ground_pound", "haka_back",
                         "haka_death", "haka_hero", "haka_of_battle", "haka_of_restoration", "haka_of_shielding",
                         "mere", "punish_the_weak", "rampage", "savage_mana", "ta_moko", "taiaha", "vitality_surge"};

        [Test, RequiresSTA]
        public void TestHeroInitalization()
        {
            Hero heroTest = new Haka();
            Assert.IsNotNull(heroTest);
            Assert.AreEqual("Haka", heroTest.getCharacterName());
        }

        [Test, RequiresSTA]
        public void TestHeroHandInitilization()
        {
            Hero heroTest = new Haka();
            Assert.IsNotNull(heroTest.hand);
        }

        [Test, RequiresSTA]
        public void TestHeroHandPopulate()
        {
            Hero heroTest = new Haka();
            Assert.AreEqual(heroTest.hand.Count, 4);
        }

        [Test, RequiresSTA]
        public void TestValidityOfHand()
        {
            bool containsAll = true;
            Hero heroTest = new Haka();
            foreach (var card in heroTest.getPlayerHand())
            {
                containsAll = containsAll && files.Contains(card.getName());
            }
            Assert.True(containsAll);
        }

        [Test, RequiresSTA] //TODO: test a diff way
        public void TestPlayerTurn()
        {
            GameEngine g = new GameEngine();
            var currPlay = g.getCurrentPlayer();
            g.nextTurn();
            Assert.AreNotEqual(currPlay, g.getCurrentPlayer());
        }
    }
}
