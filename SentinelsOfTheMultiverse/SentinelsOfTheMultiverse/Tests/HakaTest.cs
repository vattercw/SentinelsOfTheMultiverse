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
using System.Reflection;
using SentinelsOfTheMultiverse.Data.Environments;

namespace SentinelsOfTheMultiverse.Tests
{
    [TestFixture]
    class HakaTest
    {
        [Test(), RequiresSTA]
        public void TestRampage()
        {
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
            Haka haka = (Haka)ObjectMother.TestHero();
            int startingLifeTotal = haka.lifeTotal;

            Card tamokoCard = new Card("Images\\Hero\\Haka\\TaMoko.png");
            haka.TaMoko(tamokoCard);

            Assert.True(false);
            //DamageEffects.DealDamgae(haka, 2, DamageEffects.DamageType.Melee);
            //Assert.AreEqual(startingLifeTotal - 1, haka.lifeTotal);

            tamokoCard.SendToGraveyard(haka, haka.cardsOnField);

            
            //DamageEffects.DealDamageToHero(haka, 2, DamageEffects.DamageType.Melee);
            //Assert.AreEqual(startingLifeTotal - 3, haka.lifeTotal);
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

            Assert.True(false);
            //int damageAmount= 2 + haka.damageAmplificationFromPlayer;
            //DamageEffects.DealDamageToVillain(GameEngine.getVillain(), damageAmount, DamageEffects.DamageType.Melee);
            //Assert.AreEqual(GameEngine.getVillain().lifeTotal, startLifeTotal - 3);

            ////needs to reset haka's damage amp after first damage has been used
            //int damageAmount2 = 2 + haka.damageAmplificationFromPlayer;
            //DamageEffects.DealDamageToVillain(GameEngine.getVillain(), damageAmount, DamageEffects.DamageType.Melee);
            //Assert.AreEqual(GameEngine.getVillain().lifeTotal, startLifeTotal - 5);

            //also need to test that it doesn't use the bonus damage until he is dealing it for the first time

        }
        [Test(), RequiresSTA]
        public void TestGroundPound() {
            GameEngine.TearDownGameEngine();
            Start game = new Start();

            game.beginGame();

            Haka haka = (Haka)GameEngine.getHeroes().Find(x => x.GetType() == typeof(Haka));
            Card groundPoundCard = new Card("Images\\Hero\\Haka\\2-GroundPound.png");

            haka.GroundPound_Continuation(0, groundPoundCard);

            int damageAmount = 20;
            //tests damage from nonhero
            DamageEffects.DealDamage(GameEngine.getEnvironment(), new List<Targetable>() { haka }, damageAmount, DamageEffects.DamageType.Melee);
            Assert.AreEqual(haka.maxHealth, haka.lifeTotal);

            //tests damage from hero
            DamageEffects.DealDamage(haka, new List<Targetable>() { haka }, damageAmount, DamageEffects.DamageType.Melee);
            Assert.AreEqual(haka.maxHealth-damageAmount, haka.lifeTotal);
        }

        [Test(), RequiresSTA]
        public void TestGroundPoundWithExistingDamageHandler() {
            GameEngine.TearDownGameEngine();
            Start game = new Start();

            game.beginGame();

            Haka haka = (Haka)GameEngine.getHeroes().Find(x => x.GetType() == typeof(Haka));
            Card groundPoundCard = new Card("Images\\Hero\\Haka\\2-GroundPound.png");
            var  insulaPrimus =(InsulaPrimus)GameEngine.getEnvironment();
            
            DamageEffects.damageDealtHandlers.Add(insulaPrimus.ObsidianFieldHandler);

            haka.GroundPound_Continuation(0, groundPoundCard);

            int damageAmount = 20;
            DamageEffects.DealDamage(insulaPrimus, new List<Targetable>() { haka }, damageAmount, DamageEffects.DamageType.Melee);
            Assert.AreEqual(haka.maxHealth, haka.lifeTotal);

        }

        [Test(), RequiresSTA]
        public void TestDominion()
        {
            GameEngine.TearDownGameEngine();
            Start game = new Start();
            
            game.beginGame();

            Haka haka= (Haka)GameEngine.getHeroes().Find(x => x.GetType() == typeof(Haka));
            Card envCard = new Card("Images\\Environment\\InsulaPrimus\\2-PterodactylThief.png");
            envCard.cardType = Card.CardType.Environment;
            GameEngine.getEnvironment().cardsOnField.Add(envCard);

            Card dominion = new Card("Images\\Hero\\Haka\\3-Dominion.png");
            haka.Dominion(dominion);

            GameEngine.getEnvironment().cardsOnField.Find(x=> x.cardType== Card.CardType.Environment).SendToGraveyard(GameEngine.getEnvironment(), GameEngine.getEnvironment().cardsOnField);
        }

        [Test(), RequiresSTA]
        public void TestEnduringIntercession() {
            GameEngine.TearDownGameEngine();
            Start game = new Start();
            game.beginGame();

            Haka haka = (Haka)GameEngine.getHeroes().Find(x => x.GetType() == typeof(Haka));
            Legacy legacy= (Legacy)GameEngine.getHeroes().Find(x => x.GetType() == typeof(Legacy));
            InsulaPrimus env = (InsulaPrimus)GameEngine.getEnvironment();
            Card enduringIntercessionCard = new Card("Images\\Hero\\Haka\\3-EnduringIntercession.png");

            haka.EnduringIntercession(enduringIntercessionCard);

            DamageEffects.DealDamage(env, new List<Targetable>(){legacy, haka},4, DamageEffects.DamageType.Melee); 
            Assert.AreEqual(legacy.maxHealth, legacy.lifeTotal);
            Assert.AreEqual(haka.maxHealth - 8, haka.lifeTotal); //haka takes his own and legacy's damage


            //test that damage wasn't absorbed for damage not from environment
            var hakaTotal = haka.lifeTotal;
            DamageEffects.DealDamage(legacy, new List<Targetable>() { legacy, haka }, 4, DamageEffects.DamageType.Melee);
            Assert.AreEqual(legacy.maxHealth -4, legacy.lifeTotal); //legacy did take 4 damage
            Assert.AreEqual(hakaTotal - 4, haka.lifeTotal); //haka takes ONLY his own damage
        }

        [Test(), RequiresSTA]
        public void TestMere()
        {
            GameEngine.TearDownGameEngine();
            Haka testHaka= new Haka();
            BaronBlade testVil = new BaronBlade();
            InsulaPrimus env = new InsulaPrimus();
            List<Hero> myHeroes = new List<Hero>() { testHaka };
            typeof(GameEngine).GetField("heroes", BindingFlags.Static| BindingFlags.NonPublic).SetValue(null, myHeroes);
            typeof(GameEngine).GetField("villain", BindingFlags.Static | BindingFlags.NonPublic).SetValue(null, testVil);
            typeof(GameEngine).GetField("environment", BindingFlags.Static | BindingFlags.NonPublic).SetValue(null, env);
            Card mere= new Card("Images\\Hero\\Haka\\3-Mere.png");

            //deals damage to villain because cardClickedArray is empty
            GameBoard.cardClickedArray = new List<Card>();
            testHaka.MerePower(mere, null);

            Assert.AreEqual(5, testHaka.hand.Count);
            Assert.AreNotEqual(GameEngine.getVillain().maxHealth, GameEngine.getVillain().lifeTotal);
        }


        [Test, RequiresSTA]
        public void TestPower()
        {
            Start game = new Start();
            game.beginGame();
            Haka haka = new Haka();

            haka.Power();

            Assert.AreEqual(38, GameEngine.getVillain().lifeTotal);
            
        }
    }
}
