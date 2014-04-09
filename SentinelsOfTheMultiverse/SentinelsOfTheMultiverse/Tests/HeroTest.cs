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
            Hero heroTest = ObjectMother.TestHero();
            Assert.AreEqual("Haka", heroTest.getCharacterName());
        }

        [Test, RequiresSTA]
        public void TestHeroHandInitilization()
        {
            Hero heroTest = ObjectMother.TestHero();
            Assert.IsNotNull(heroTest.hand);
        }

        [Test, RequiresSTA]
        public void TestHeroHandPopulate()
        {
            Hero heroTest = ObjectMother.TestHero();
            Assert.AreEqual(heroTest.hand.Count, 4);
        }

        [Test, RequiresSTA]
        public void TestValidityOfHand()
        {
            bool containsAll = true;
            Hero heroTest = ObjectMother.TestHero();
            foreach (var card in heroTest.getPlayerHand())
            {
                containsAll = containsAll && files.Contains(card.getName());
            }
            Assert.True(containsAll);
        }

        [Test, RequiresSTA] //TODO: test a diff way
        public void TestPlayerTurnCard()
        {
            var currPlay = GameEngine.getCurrentPlayer();
            GameEngine.nextTurn();
            Assert.AreNotEqual(currPlay, GameEngine.getCurrentPlayer());
        }

        [Test, RequiresSTA]
        public void TestPlayerLifeInit()
        {
            Hero hero = ObjectMother.TestHero();
            Assert.AreEqual(hero.lifePoints, 34);
        }

        [Test, RequiresSTA]
        public void TestPlayerImmunization()
        {
            Hero hero = ObjectMother.TestHero();
            Assert.AreEqual(hero.getImmunities(), new List<String>());
            hero.addImmunity("Projectile");
            List<String> testImmunities = new List<String>();
            testImmunities.Add("Projectile");
            Assert.AreEqual(hero.getImmunities(), testImmunities);
        }

    }
}
