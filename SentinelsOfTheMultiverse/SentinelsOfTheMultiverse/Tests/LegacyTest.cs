using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SentinelsOfTheMultiverse.Data;
using SentinelsOfTheMultiverse.Data.Heroes;
using SentinelsOfTheMultiverse.Data.Minions;
using SentinelsOfTheMultiverse.Data.Minions.InsulaPrimus;

namespace SentinelsOfTheMultiverse.Tests
{

    [TestFixture]
    class LegacyTest
    {

        [SetUp(), RequiresSTA]
        public void Setup()
        {
            Start st = new Start();
            st.beginGame();
        }

        [Test, RequiresSTA]
        public void TestFortitude()
        {
           
            Legacy testLegacy = new Legacy();

            Card testFortitude = new Card("Images\\Hero\\Legacy\\Fortitude.png");
            testLegacy.Fortitude(testFortitude);
            //Assert.AreEqual(testLegacy.damageAmplificationToPlayer, -1);
            Assert.True(false);
        }

        [Test, RequiresSTA]
        public void TestDangerSense()
        {
            Legacy testLegacy = new Legacy();

            Card testDanger = new Card("Images\\Hero\\Legacy\\DangerSense.png");
            testLegacy.DangerSense(testDanger);
            //TODO fix this test
            Assert.Fail();
        }

        [Test, RequiresSTA]
        public void TestFlyingSmash()
        {


            Legacy testLegacy = new Legacy();
            Card testFlying = new Card("Images\\Hero\\Legacy\\FlyingSmash.png");
            testLegacy.FlyingSmash(testFlying);
            Card redist = new Card("Images\\Villain\\BaronBlade\\2-ElementalRedistributor.png");
            Card velo = new Card("Images\\Environment\\InsulaPrimus\\3-VelociraptorPack.png");
            Card battalion = new Card("Images\\Environment\\InsulaPrimus\\4-BattleBattalion.png");
            Card battalion2 = new Card("Images\\Environment\\InsulaPrimus\\4-BattleBattalion.png");
            List<Card> cards = new List<Card>();
            cards.Add(redist);
            cards.Add(velo);
            GameEngine.getEnvironment().cardsOnField.Add(velo);
            GameEngine.getEnvironment().addMinion(new VelociraptorPack());
            GameEngine.getVillain().addMinion(new ElementalRedistributor());
            GameEngine.getVillain().cardsOnField.Add(redist);
            GameBoard.cardClickedArray = cards;
            testLegacy.FlyingSmash(testFlying);

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
            
            testLegacy.FlyingSmash(testFlying);

            Card redist2 = new Card("Images\\Villain\\BaronBlade\\ElementalRedistributor.png");
            List<Card> cards2 = new List<Card>();
            cards2.Add(redist2); 
            GameEngine.getVillain().cardsOnField.Add(redist2);
            GameBoard.cardClickedArray = cards2;
            testLegacy.FlyingSmash(testFlying);

            testLegacy.FlyingSmash(testFlying);

            Card obsidian = new Card("Images\\Environment\\InsulaPrimus\\3-ObsidianField.png");
            GameEngine.getEnvironment().cardsOnField.Add(obsidian);
            List<Card> cards4 = new List<Card>();
            cards4.Add(obsidian);
            GameBoard.cardClickedArray = cards4;

            testLegacy.FlyingSmash(testFlying);

        }

        //TODO fix test to get prompted selected array AND counteract static gamestate much code still not touched with this test
        [Test, RequiresSTA]
        public void TestBackFistStrike()
        {

            Card testStrike = new Card("Images\\Hero\\Legacy\\BackFistStrike.png");

            Legacy testLegacy = new Legacy();

            testLegacy.BackfistStrike(testStrike);

            Card redist = new Card("Images\\Villain\\BaronBlade\\2-ElementalRedistributor.png");
            Card velo = new Card("Images\\Environment\\InsulaPrimus\\3-VelociraptorPack.png");
            Card battalion = new Card("Images\\Environment\\InsulaPrimus\\4-BattleBattalion.png");
            Card battalion2 = new Card("Images\\Environment\\InsulaPrimus\\4-BattleBattalion.png");
            List<Card> cards = new List<Card>();
            cards.Add(redist);
            cards.Add(velo);
            GameEngine.getEnvironment().cardsOnField.Add(velo);
            GameEngine.getEnvironment().addMinion(new VelociraptorPack());
            GameEngine.getVillain().addMinion(new ElementalRedistributor());
            GameEngine.getVillain().cardsOnField.Add(redist);
            GameBoard.cardClickedArray = cards;

            testLegacy.BackfistStrike(testStrike);


            List<Card> cards3 = new List<Card>();
            cards3.Add(redist);
            GameEngine.getVillain().addMinion(new ElementalRedistributor());
            GameEngine.getVillain().cardsOnField.Add(redist);
            GameBoard.cardClickedArray = cards3;

            testLegacy.BackfistStrike(testStrike);

            GameEngine.getEnvironment().cardsOnField.Add(velo);
            GameEngine.getEnvironment().addMinion(new VelociraptorPack());
            List<Card> cards2 = new List<Card>();
            cards2.Add(velo);
            GameBoard.cardClickedArray = cards2;

            testLegacy.BackfistStrike(testStrike);

            Card obsidian = new Card("Images\\Environment\\InsulaPrimus\\3-ObsidianField.png");
            GameEngine.getEnvironment().cardsOnField.Add(obsidian);
            List<Card> cards4 = new List<Card>();
            cards4.Add(obsidian);
            GameBoard.cardClickedArray = cards4;

            testLegacy.BackfistStrike(testStrike);

            
            
        }

        [Test, RequiresSTA]
        public void TestMotivationalCharge()
        {
            Card testMot = new Card("Images\\Hero\\Legacy\\3-MotivationalCharge.png");

            Legacy testLegacy = new Legacy();

            testLegacy.MotivationalChargePower(testMot, null);

            Card redist = new Card("Images\\Villain\\BaronBlade\\2-ElementalRedistributor.png");
            Card velo = new Card("Images\\Environment\\InsulaPrimus\\3-VelociraptorPack.png");
            Card battalion = new Card("Images\\Environment\\InsulaPrimus\\4-BattleBattalion.png");
            Card battalion2 = new Card("Images\\Environment\\InsulaPrimus\\4-BattleBattalion.png");
            List<Card> cards = new List<Card>();
            cards.Add(redist);
            cards.Add(velo);
            GameEngine.getEnvironment().cardsOnField.Add(velo);
            GameEngine.getEnvironment().addMinion(new VelociraptorPack());
            GameEngine.getVillain().addMinion(new ElementalRedistributor());
            GameEngine.getVillain().cardsOnField.Add(redist);
            GameBoard.cardClickedArray = cards;

            testLegacy.MotivationalChargePower(testMot, null);


            List<Card> cards3 = new List<Card>();
            cards3.Add(redist);
            GameEngine.getVillain().addMinion(new ElementalRedistributor());
            GameEngine.getVillain().cardsOnField.Add(redist);
            GameBoard.cardClickedArray = cards3;

            testLegacy.MotivationalChargePower(testMot, null);

            GameEngine.getEnvironment().cardsOnField.Add(velo);
            GameEngine.getEnvironment().addMinion(new VelociraptorPack());
            List<Card> cards2 = new List<Card>();
            cards2.Add(velo);
            GameBoard.cardClickedArray = cards2;

            testLegacy.MotivationalChargePower(testMot, null);

            Card obsidian = new Card("Images\\Environment\\InsulaPrimus\\3-ObsidianField.png");
            GameEngine.getEnvironment().cardsOnField.Add(obsidian);
            List<Card> cards4 = new List<Card>();
            cards4.Add(obsidian);
            GameBoard.cardClickedArray = cards4;

            testLegacy.MotivationalChargePower(testMot, null);
        }

        [Test, RequiresSTA]
        public void TestInspiringPresence()
        {

        }

        [Test, RequiresSTA]
        public void TestLeadFromFront()
        {

        }

        [Test, RequiresSTA]
        public void TestBolsterAllies()
        {
           
            Legacy legacy = new Legacy();

            List<int> firstList = new List<int>();
            foreach (Hero hero in GameEngine.getHeroes())
            {
                firstList.Add(hero.hand.Count());
            }

            Card bolster = new Card("Images\\Hero\\Legacy\\BolsterAllies.png");
            legacy.BolsterAllies(bolster);

            List<int> secondList = new List<int>();
            foreach (Hero hero in GameEngine.getHeroes())
            {
                secondList.Add(hero.hand.Count());
            }

            for(int i = 0; i < secondList.Count(); i++)
            {
                Assert.AreEqual(secondList[i], firstList[i] + 1);
            }
        }

        [Test, RequiresSTA]
        public void TestSuperhuman()
        {
            
            Legacy legacy = new Legacy();

            
            Card superhuman = new Card("Images\\Hero\\Legacy\\SuperhumanDurability.png");
            legacy.SuperhumanDurability(superhuman);
            //Assert.AreEqual(legacy.damageAmplificationFromPlayer, 1);
            Assert.True(false);
        }

        [Test, RequiresSTA]
        public void TestNextEvolution()
        {

        }

        [Test, RequiresSTA]
        public void TesSurgeOfStrength()
        {
        }

        [Test, RequiresSTA]
        public void TestThokk()
        {
            Card testThokk = new Card("Images\\Hero\\Legacy\\3-Thokk.png");

            Legacy testLegacy = new Legacy();

            testLegacy.Thokk(testThokk);

            Card redist = new Card("Images\\Villain\\BaronBlade\\2-ElementalRedistributor.png");
            Card velo = new Card("Images\\Environment\\InsulaPrimus\\3-VelociraptorPack.png");
            Card battalion = new Card("Images\\Environment\\InsulaPrimus\\4-BattleBattalion.png");
            Card battalion2 = new Card("Images\\Environment\\InsulaPrimus\\4-BattleBattalion.png");
            List<Card> cards = new List<Card>();
            cards.Add(redist);
            cards.Add(velo);
            GameEngine.getEnvironment().cardsOnField.Add(velo);
            GameEngine.getEnvironment().addMinion(new VelociraptorPack());
            GameEngine.getVillain().addMinion(new ElementalRedistributor());
            GameEngine.getVillain().cardsOnField.Add(redist);
            GameBoard.cardClickedArray = cards;

            testLegacy.Thokk(testThokk);


            List<Card> cards3 = new List<Card>();
            cards3.Add(redist);
            GameEngine.getVillain().addMinion(new ElementalRedistributor());
            GameEngine.getVillain().cardsOnField.Add(redist);
            GameBoard.cardClickedArray = cards3;

            testLegacy.Thokk(testThokk);

            GameEngine.getEnvironment().cardsOnField.Add(velo);
            GameEngine.getEnvironment().addMinion(new VelociraptorPack());
            List<Card> cards2 = new List<Card>();
            cards2.Add(velo);
            GameBoard.cardClickedArray = cards2;

            testLegacy.Thokk(testThokk);

            Card obsidian = new Card("Images\\Environment\\InsulaPrimus\\3-ObsidianField.png");
            GameEngine.getEnvironment().cardsOnField.Add(obsidian);
            List<Card> cards4 = new List<Card>();
            cards4.Add(obsidian);
            GameBoard.cardClickedArray = cards4;

            testLegacy.Thokk(testThokk);
        }
        [Test, RequiresSTA]
        public void TestLegacyRing()
        {
           
            Legacy legacy = new Legacy();

            Card ring = new Card("Images\\Hero\\Legacy\\TheLegacyRing.png");
            legacy.TheLegacyRing(ring);
            Assert.AreEqual(legacy.numPowers, 2);

            legacy.TheLegacyRing_Destroyed_Handler(ring, null);

            Assert.AreEqual(legacy.numPowers, 1);
            

        }

        [Test, RequiresSTA]
        public void TestTakeDown()
        {

        }

        [Test, RequiresSTA]
        public void TestPower()
        {
            
            Legacy legacy = new Legacy();

            legacy.Power();

            foreach (Hero hero in GameEngine.getHeroes())
            {
                //Assert.AreEqual(1, hero.damageAmplificationFromPlayer);
                Assert.True(false);
            }

            Assert.NotNull(GameEngine.getHeroes());
        }

        [TearDown()]
        public void TearDown()
        {
            GameEngine.TearDownGameEngine();

        }

    }
}
