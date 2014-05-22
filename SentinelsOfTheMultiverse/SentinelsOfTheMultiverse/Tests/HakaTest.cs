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
        [SetUp(), RequiresSTA]
        public void Setup()
        {
            Start st = new Start();
            st.beginGame();
        }

        [Test(), RequiresSTA]
        public void TestRampage()
        {
            //add mocks for GameEngine.getHeroes() and GameEngine.getVillain()
            Haka haka = (Haka)GameEngine.getHeroes().Find(x => x.GetType() == typeof(Haka));

            Card rampageCard = new Card("Images\\Hero\\Haka\\3-Rampage.png");
            haka.Rampage(rampageCard);
            Assert.AreEqual(35, GameEngine.getVillain().lifeTotal);
            Assert.AreEqual(32, haka.lifeTotal);
        }

        [Test(), RequiresSTA]
        public void TestElbowSmash()
        {
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
            Haka haka = (Haka)ObjectMother.TestHero();
            BaronBlade baron = (BaronBlade)ObjectMother.TestVillain();
            int startingLifeTotal = haka.lifeTotal;

            Card tamokoCard = new Card("Images\\Hero\\Haka\\TaMoko.png");
            haka.TaMoko(tamokoCard);

            DamageEffects.DealDamage(baron, new List<Targetable>() { haka }, 4, DamageEffects.DamageType.Melee);

            Assert.AreEqual(31, haka.lifeTotal);
        }

        [Test(), RequiresSTA]
        public void TestHakaOfBattle()
        {
            Haka haka = (Haka)ObjectMother.TestHero();
            Card hakaOfBattleCard = new Card("Images\\Heroes\\Haka\\3-HakaOfBattle.png");
            Card elbowCard = new Card("Images\\Heroes\\Haka\\3-ElbowSmash.png");

            int startLifeTotal = GameEngine.getVillain().lifeTotal;
            int startHandCount = haka.hand.Count;

            haka.DiscardCard(haka.hand[0]);
            haka.DiscardCard(haka.hand[1]);

            haka.HakaOfBattle(hakaOfBattleCard);
            //Pretend to have discard 2 cards
            haka.HakaOfBattle_Continuation(2, hakaOfBattleCard);

            haka.ElbowSmash(elbowCard);

            Assert.AreEqual(startHandCount, haka.hand.Count);
            Assert.AreEqual(35, GameEngine.getVillain().lifeTotal);
        }

        [Test(), RequiresSTA]
        public void TestHakaOfRestoration()
        {
            Haka haka = (Haka)ObjectMother.TestHero();
            Card hakaOfRestorationCard = new Card("Images\\Heroes\\Haka\\3-HakaOfRestoration.png");

            haka.lifeTotal -= 14;
            int startHandCount = haka.hand.Count;

            haka.DiscardCard(haka.hand[0]);
            haka.DiscardCard(haka.hand[1]);

            haka.HakaOfRestoration(hakaOfRestorationCard);
            //Pretend to have discard 2 cards
            haka.HakaOfRestoration_Continuation(2, hakaOfRestorationCard);

            Assert.AreEqual(startHandCount, haka.hand.Count);
            Assert.AreEqual(22, haka.lifeTotal);
        }

        [Test(), RequiresSTA]
        public void TestHakaOfShielding()
        {
            Haka haka = (Haka)ObjectMother.TestHero();
            Card hakaOfShieldingCard = new Card("Images\\Heroes\\Haka\\3-HakaOfShielding.png");

            int startHandCount = haka.hand.Count;

            haka.DiscardCard(haka.hand[0]);
            haka.DiscardCard(haka.hand[1]);

            haka.HakaOfShielding(hakaOfShieldingCard);
            //Pretend to have discard 2 cards
            haka.HakaOfShielding_Continuation(2, hakaOfShieldingCard);

            DamageEffects.DealDamage(haka, new List<Targetable>() { haka }, 5, DamageEffects.DamageType.Melee);

            Assert.AreEqual(startHandCount, haka.hand.Count);
            Assert.AreEqual(33, haka.lifeTotal);
        }

        [Test(), RequiresSTA]
        public void TestGroundPound() {
            TearDown();
            Setup();
            Haka haka = (Haka)GameEngine.getHeroes().Find(x => x.GetType() == typeof(Haka));
            Legacy legacy = (Legacy)GameEngine.getHeroes().Find(x => x.GetType() == typeof(Legacy));
            Card groundPoundCard = new Card("Images\\Hero\\Haka\\2-GroundPound.png");

            haka.GroundPound_Continuation(0, groundPoundCard);

            int damageAmount = 20;
            //tests damage from nonhero
            DamageEffects.DealDamage(GameEngine.getEnvironment(), new List<Targetable>() { haka }, damageAmount, DamageEffects.DamageType.Melee);
            Assert.AreEqual(haka.maxHealth, haka.lifeTotal);

            //tests damage from hero
            DamageEffects.DealDamage(legacy, new List<Targetable>() { haka }, damageAmount, DamageEffects.DamageType.Melee);
            Assert.AreEqual(haka.maxHealth-damageAmount, haka.lifeTotal);
        }

        [Test(), RequiresSTA]
        public void TestGroundPoundWithExistingDamageHandler() {

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
            Haka haka= (Haka)GameEngine.getHeroes().Find(x => x.GetType() == typeof(Haka));
            Card envCard = new Card("Images\\Environment\\InsulaPrimus\\2-PterodactylThief.png");
            envCard.cardType = Card.CardType.Environment;
            GameEngine.getEnvironment().cardsOnField.Add(envCard);

            Card dominion = new Card("Images\\Hero\\Haka\\3-Dominion.png");
            haka.Dominion(dominion);

            var startHandSize = haka.hand.Count;
            //destroy an environment card
            GameEngine.getEnvironment().cardsOnField.Find(x=> x.cardType== Card.CardType.Environment).SendToGraveyard(GameEngine.getEnvironment(), GameEngine.getEnvironment().cardsOnField);
            
            //assuming yes was pressed
            Assert.AreEqual(startHandSize + 1, haka.hand.Count);
        }

        [Test(), RequiresSTA]
        public void TestEnduringIntercession() {

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
            Haka haka = new Haka();

            haka.Power();

            Assert.AreEqual(38, GameEngine.getVillain().lifeTotal);   
        }

        [TearDown()]
        public void TearDown()
        {
            GameEngine.TearDownGameEngine();
            
        }
    }
}
