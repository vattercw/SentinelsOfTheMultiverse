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
using SentinelsOfTheMultiverse.Data.Minions.InsulaPrimus;
using SentinelsOfTheMultiverse.Data.Minions;

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
            GameEngine.getVillain().getMinions()[0].lifeTotal = 0;
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
            Haka testHaka = new Haka();
            Card testElbow = new Card("Images\\Hero\\Haka\\3-ElbowSmash.png");
            testHaka.ElbowSmash(testElbow);
            Card redist = new Card("Images\\Villain\\BaronBlade\\2-ElementalRedistributor.png");
            Card velo = new Card("Images\\Environment\\InsulaPrimus\\3-VelociraptorPack.png");
            Card battalion = new Card("Images\\Environment\\InsulaPrimus\\4-BattleBattalion.png");
            Card battalion2 = new Card("Images\\Environment\\InsulaPrimus\\4-BattleBattalion.png");
            List<Card> cards = new List<Card>();
            cards.Add(velo);
            GameEngine.getEnvironment().cardsOnField.Add(velo);
            GameEngine.getEnvironment().addMinion(new VelociraptorPack());
            GameBoard.cardClickedArray = cards;
            testHaka.ElbowSmash(testElbow);

            List<Card> cards3 = new List<Card>();
            cards3.Add(redist);
            cards3.Add(velo);
            cards3.Add(battalion);
            cards3.Add(battalion2);

            GameEngine.getEnvironment().cardsOnField.Add(velo);
            GameEngine.getEnvironment().addMinion(new VelociraptorPack());
            GameEngine.getVillain().addMinion(new ElementalRedistributor());
            GameEngine.getVillain().addMinion(new BladeBattalion());
            GameEngine.getVillain().addMinion(new BladeBattalion());
            GameEngine.getVillain().cardsOnField.Add(redist);
            GameEngine.getVillain().cardsOnField.Add(battalion2);
            GameEngine.getVillain().cardsOnField.Add(battalion);
            GameBoard.cardClickedArray = cards3;

            testHaka.ElbowSmash(testElbow);

            GameBoard.cardClickedArray.Clear();
            testHaka.ElbowSmash(testElbow);

            Card obsidian = new Card("Images\\Environment\\InsulaPrimus\\3-ObsidianField.png");
            GameEngine.getEnvironment().cardsOnField.Add(obsidian);
            List<Card> cards4 = new List<Card>();
            cards4.Add(obsidian);
            GameBoard.cardClickedArray = cards4;

            testHaka.ElbowSmash(testElbow);
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
            //GameEngine.getVillain().getMinions()[0].lifeTotal = 0;
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
            Haka testHaka = new Haka();
            Card testMere = new Card("Images\\Hero\\Haka\\3-Mere.png");
            testHaka.Mere(testMere);
            testHaka.MerePower(testMere, null);
            Card redist = new Card("Images\\Villain\\BaronBlade\\2-ElementalRedistributor.png");
            Card velo = new Card("Images\\Environment\\InsulaPrimus\\3-VelociraptorPack.png");
            Card battalion = new Card("Images\\Environment\\InsulaPrimus\\4-BattleBattalion.png");
            Card battalion2 = new Card("Images\\Environment\\InsulaPrimus\\4-BattleBattalion.png");
            List<Card> cards = new List<Card>();
            cards.Add(velo);
            GameEngine.getEnvironment().cardsOnField.Add(velo);
            GameEngine.getEnvironment().addMinion(new VelociraptorPack());
            GameBoard.cardClickedArray = cards;
            testHaka.MerePower(testMere, null);

            List<Card> cards2 = new List<Card>();
            cards2.Add(redist);
            GameEngine.getVillain().cardsOnField.Add(redist);
            GameEngine.getVillain().addMinion(new ElementalRedistributor());
            GameBoard.cardClickedArray = cards2;
            testHaka.MerePower(testMere, null);

            List<Card> cards3 = new List<Card>();
            cards3.Add(redist);
            cards3.Add(velo);
            cards3.Add(battalion);
            cards3.Add(battalion2);

            GameEngine.getEnvironment().cardsOnField.Add(velo);
            GameEngine.getEnvironment().addMinion(new VelociraptorPack());
            GameEngine.getVillain().addMinion(new ElementalRedistributor());
            GameEngine.getVillain().addMinion(new BladeBattalion());
            GameEngine.getVillain().addMinion(new BladeBattalion());
            GameEngine.getVillain().cardsOnField.Add(redist);
            GameEngine.getVillain().cardsOnField.Add(battalion2);
            GameEngine.getVillain().cardsOnField.Add(battalion);
            GameBoard.cardClickedArray = cards3;

            testHaka.MerePower(testMere, null);

            Card obsidian = new Card("Images\\Environment\\InsulaPrimus\\3-ObsidianField.png");
            GameEngine.getEnvironment().cardsOnField.Add(obsidian);
            List<Card> cards4 = new List<Card>();
            cards4.Add(obsidian);
            GameBoard.cardClickedArray = cards4;

            testHaka.MerePower(testMere, null);
        }


        [Test, RequiresSTA]
        public void TestPower()
        {
            Haka testHaka = new Haka();
            testHaka.Power();
            Card redist = new Card("Images\\Villain\\BaronBlade\\2-ElementalRedistributor.png");
            Card velo = new Card("Images\\Environment\\InsulaPrimus\\3-VelociraptorPack.png");
            Card battalion = new Card("Images\\Environment\\InsulaPrimus\\4-BattleBattalion.png");
            Card battalion2 = new Card("Images\\Environment\\InsulaPrimus\\4-BattleBattalion.png");
            List<Card> cards = new List<Card>();
            cards.Add(velo);
            GameEngine.getEnvironment().cardsOnField.Add(velo);
            GameEngine.getEnvironment().addMinion(new VelociraptorPack());
            GameBoard.cardClickedArray = cards;
            testHaka.Power();

            List<Card> cards2 = new List<Card>();
            cards2.Add(redist);
            GameEngine.getVillain().cardsOnField.Add(redist);
            GameEngine.getVillain().addMinion(new ElementalRedistributor());
            GameBoard.cardClickedArray = cards2;
            testHaka.Power();

            List<Card> cards3 = new List<Card>();
            cards3.Add(redist);
            cards3.Add(velo);
            cards3.Add(battalion);
            cards3.Add(battalion2);

            GameEngine.getEnvironment().cardsOnField.Add(velo);
            GameEngine.getEnvironment().addMinion(new VelociraptorPack());
            GameEngine.getVillain().addMinion(new ElementalRedistributor());
            GameEngine.getVillain().addMinion(new BladeBattalion());
            GameEngine.getVillain().addMinion(new BladeBattalion());
            GameEngine.getVillain().cardsOnField.Add(redist);
            GameEngine.getVillain().cardsOnField.Add(battalion2);
            GameEngine.getVillain().cardsOnField.Add(battalion);
            GameBoard.cardClickedArray = cards3;

            testHaka.Power();

            Card obsidian = new Card("Images\\Environment\\InsulaPrimus\\3-ObsidianField.png");
            GameEngine.getEnvironment().cardsOnField.Add(obsidian);
            List<Card> cards4 = new List<Card>();
            cards4.Add(obsidian);
            GameBoard.cardClickedArray = cards4;

            testHaka.Power();  
        }

        [TearDown()]
        public void TearDown()
        {
            GameEngine.TearDownGameEngine();
            
        }
    }
}
