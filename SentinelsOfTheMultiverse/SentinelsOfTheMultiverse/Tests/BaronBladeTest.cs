using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SentinelsOfTheMultiverse.Data;
using SentinelsOfTheMultiverse.Data.Minions;
using SentinelsOfTheMultiverse.Data.Villains;

namespace SentinelsOfTheMultiverse.Tests
{
    class BaronBladeTest
    {

        [SetUp(), RequiresSTA]
        public void Setup()
        {
            Start st = new Start();
            st.beginGame();
        }

        [Test(), RequiresSTA]
        public void TestHastenDoom()
        {
            BaronBlade test = new BaronBlade();
            Card hastenCard = new Card("Images\\Villain\\BaronBlade\\4-HastenDoom.png");
            test.HastenDoom(hastenCard);

            Assert.AreEqual(GameEngine.getHeroes()[0].lifeTotal, 32);
            Assert.AreEqual(GameEngine.getHeroes()[1].lifeTotal, 30);

        }

        [Test(), RequiresSTA]
        public void TestDeviousDisruption()
        {
            //test
        }

        [Test(), RequiresSTA]
        public void TestFleshRepairNanites()
        {

            BaronBlade test = new BaronBlade();
            Card nanoCard = new Card("Images\\Villain\\BaronBlade\\1-FleshRepairNanites.png");
            test.FleshRepairNanites(nanoCard);

            Assert.AreEqual(GameEngine.getVillain().lifeTotal, 40);
        }

        [Test(), RequiresSTA]
        public void TestSlashAndBurn()
        {

            BaronBlade test = new BaronBlade();
            Card slashCard = new Card("Images\\Villain\\BaronBlade\\2-SlashAndBurn.png");
            test.SlashAndBurn(slashCard);

            Assert.AreEqual(GameEngine.getHeroes()[0].lifeTotal, GameEngine.getHeroes()[0].maxHealth-4);
            Assert.AreEqual(GameEngine.getHeroes()[1].lifeTotal, GameEngine.getHeroes()[1].maxHealth - 2);
        }

        [Test(), RequiresSTA]
        public void TestConsiderThePrice()
        {

        }

        [Test(), RequiresSTA]
        public void TestLivingForceField()
        {
           
        }

        [Test(), RequiresSTA]
        public void TestTurret()
        {
            BaronBlade baron = (BaronBlade)GameEngine.getVillain();
            Card turretCard = new Card("Images\\Villain\\BaronBlade\\2-PoweredRemoteTurret.png");
            baron.PoweredRemoteTurret(turretCard);
            List<Minion> minions = GameEngine.getVillain().getMinions();
            Assert.IsTrue(minions.Contains(turretCard.Minion));
        }

        [Test(), RequiresSTA]
        public void TestPlatform()
        {
            BaronBlade baron = (BaronBlade)GameEngine.getVillain();
            Card platCard = new Card("Images\\Villain\\BaronBlade\\3-MobileDefencePlatform.png");
            baron.MobileDefensePlatform(platCard);
            List<Minion> minions = GameEngine.getVillain().getMinions();
            Assert.IsTrue(minions.Contains(platCard.Minion));
        }

        [Test(), RequiresSTA]
        public void TestRedistributor()
        {
            BaronBlade baron = (BaronBlade)GameEngine.getVillain();
            Card redistCard = new Card("Images\\Villain\\BaronBlade\\2-ElementalRedistributor.png");
            baron.ElementalRedistributor(redistCard);
            List<Minion> minions = GameEngine.getVillain().getMinions();
            Assert.IsTrue(minions.Contains(redistCard.Minion));
        }

        [Test(), RequiresSTA]
        public void TestBattalion()
        {
            BaronBlade baron = (BaronBlade)GameEngine.getVillain();
            Card battleCard = new Card("Images\\Villain\\BaronBlade\\4-BladeBattalion.png");
            baron.BladeBattalion(battleCard);
            List<Minion> minions = GameEngine.getVillain().getMinions();
            Assert.IsTrue(minions.Contains(battleCard.Minion));
        }

        [TearDown()]
        public void TearDown()
        {
            GameEngine.TearDownGameEngine();

        }
    }
}
