using NUnit.Framework;
using SentinelsOfTheMultiverse.Data;
using SentinelsOfTheMultiverse.Data.Heroes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SentinelsOfTheMultiverse.Data.Villains;
using SentinelsOfTheMultiverse.Data.Effects;

namespace SentinelsOfTheMultiverse.Tests
{
    [TestFixture]
    class HakaTest
    {
        [Test(),RequiresSTA]
        public void TestRampage(){
            //add mocks for GameEngine.getHeroes() and GameEngine.getVillain()
            Start game = new Start();
            game.beginGame();
            Haka haka = (Haka)ObjectMother.TestHero();
            Minion min1 = ObjectMother.TestMinion();
            GameEngine.getVillain().addMinion(min1);

            Card rampageCard = new Card("Images\\Hero\\Haka\\Rampage.png");
            haka.Rampage(rampageCard);
            Assert.AreEqual(GameEngine.getVillain().lifeTotal, 35);
        }

        [Test(), RequiresSTA]
        public void TestElbowSmash()
        {
            Start game = new Start();
            game.beginGame();
            Haka haka = (Haka)ObjectMother.TestHero();
            BaronBlade baron = (BaronBlade)ObjectMother.TestVillain();
            GameEnvironment env = (GameEnvironment)ObjectMother.TestEnvironment();

            Minion min1 = ObjectMother.TestMinion();
            GameEngine.getVillain().addMinion(min1);

            Minion min2 = ObjectMother.TestMinion();
            GameEngine.getEnvironment().addMinion(min2);

            Card elbowSmash = new Card("Images\\Hero\\Haka\\ElbowSmash.png");

        }

        [Test(), RequiresSTA]
        public void TestTaMoko()
        {
            Start game = new Start();
            game.beginGame();
            Haka haka = (Haka) ObjectMother.TestHero();
            int startingLifeTotal = haka.lifeTotal;

            Card tamokoCard = new Card("Images\\Hero\\Haka\\TaMoko.png");
            haka.TaMoko(tamokoCard);

            DamageEffects.DealDamageToHero(haka, 2, DamageEffects.DamageType.Melee);
            Assert.AreEqual(startingLifeTotal-1, haka.lifeTotal);

            tamokoCard.SendToGraveyard(haka, haka.cardsOnField);

            DamageEffects.DealDamageToHero(haka, 2, DamageEffects.DamageType.Melee);
            Assert.AreEqual(startingLifeTotal - 3, haka.lifeTotal);
        }

        [Test(), RequiresSTA]
        public void TestHakaOfBattle()
        {
            Start game = new Start();
            game.beginGame();
            Haka haka = (Haka)ObjectMother.TestHero();
            Card hakaOfBattleCard = new Card("Images\\Hero\\Haka\\HakaOfBattle.png");
            int startLifeTotal = GameEngine.getVillain().lifeTotal;
            int startHandCount = haka.hand.Count;

            haka.DiscardCard(haka.hand[0]);

            haka.HakaOfBattle(hakaOfBattleCard);
            int damageAmount= 2 +haka.damageAmplificationFromPlayer;
            DamageEffects.DealDamageToVillain(GameEngine.getVillain(), damageAmount, DamageEffects.DamageType.Melee);
            Assert.AreEqual(GameEngine.getVillain().lifeTotal, startLifeTotal-3);

            //needs to reset haka's damage amp after first damage has been used
            int damageAmount2 = 2 + haka.damageAmplificationFromPlayer;
            DamageEffects.DealDamageToVillain(GameEngine.getVillain(), damageAmount, DamageEffects.DamageType.Melee);
            Assert.AreEqual(GameEngine.getVillain().lifeTotal, startLifeTotal - 5);

            //also need to test that it doesn't use the bonus damage until he is dealing it for the first time
        }
    }
}
