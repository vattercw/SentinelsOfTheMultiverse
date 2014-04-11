using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SentinelsOfTheMultiverse.Data.Heroes;
using SentinelsOfTheMultiverse.Data;

namespace SentinelsOfTheMultiverse.Tests
{
    [TestFixture]
    class HeroTest
    {
        string[] files = { "Dominion", "ElbowSmash", "EnduringIntercession", "GroundPound", "HakaOfBattle", "HakaOfRestoration", "HakaOfShielding",
                         "mere", "punish_the_weak", "rampage", "savage_mana", "ta_moko", "taiaha", "VitalitySurge"};

        private List<string> getDeckCardsString(IPlayer p){
            var L = new List<string>();
            var deckAndHand= p.deck.getCards();
            deckAndHand.AddRange(((Hero)p).hand);
            foreach (var card in deckAndHand)
                L.Add(card.getName());
            return L;
        }

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
            Hero heroTest = new Haka();
            
            foreach (var card in heroTest.getPlayerHand())
            {
                List<string> cardList = getDeckCardsString(heroTest);
                containsAll = containsAll && cardList.Contains(card.getName());
            }
            Assert.True(containsAll);
        }

        //[Test, RequiresSTA]
        //public void TestPlayerTurnCard()
        //{
        //    var currPlay = GameEngine.getCurrentPlayer();
        //    GameEngine.nextTurn();
        //    Assert.AreNotEqual(currPlay, GameEngine.getCurrentPlayer());
        //}

        [Test, RequiresSTA]
        public void TestPlayerLifeInit()
        {
            Hero hero = ObjectMother.TestHero();
            Assert.AreEqual(hero.lifeTotal, 34);
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
