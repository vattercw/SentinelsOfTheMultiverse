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
        [Test(), RequiresSTA]
        public void TestHastenDoom()
        {
            Start game = new Start();
            game.beginGame();
            BaronBlade test = new BaronBlade();
            Card hastenCard = new Card("Images\\Villain\\BaronBlade\\HastenDoom.png");
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
            Start game = new Start();
            game.beginGame();

            BaronBlade test = new BaronBlade();
            Card nanoCard = new Card("Images\\Villain\\BaronBlade\\FleshRepairNanites.png");
            test.FleshRepairNanites(nanoCard);

            Assert.AreEqual(GameEngine.getVillain().lifeTotal, 40);
        }

        [Test(), RequiresSTA]
        public void TestSlashAndBurn()
        {
            Start game = new Start();
            game.beginGame();

            BaronBlade test = new BaronBlade();
            Card slashCard = new Card("Images\\Villain\\BaronBlade\\SlashAndBurn.png");
            test.SlashAndBurn(slashCard);

            for (int i = 0; i < GameEngine.getHeroes().Count; i++)
            {
                Assert.AreEqual(GameEngine.getHeroes()[i].lifeTotal, 30);
            }
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
            Start game = new Start();
            game.beginGame();
            BaronBlade test = new BaronBlade();
            Card turretCard = new Card("Images\\Villain\\BaronBlade\\PoweredRemoteTurret.png");
            test.PoweredRemoteTurret(turretCard);

            Assert.AreEqual(GameEngine.getVillain().getMinions()[0].ToString(), (new PoweredRemoteTurret()).ToString());
        }

        [Test(), RequiresSTA]
        public void TestPlatform()
        {
            Start game = new Start();
            game.beginGame();
            BaronBlade test = new BaronBlade();
            Card platCard = new Card("Images\\Villain\\BaronBlade\\MobileDefencePlatform.png");
            test.MobileDefensePlatform(platCard);

            Assert.AreEqual(GameEngine.getVillain().getMinions()[0].ToString(), (new MobileDefensePlatform()).ToString());
        }

        [Test(), RequiresSTA]
        public void TestRedistributor()
        {
            Start game = new Start();
            game.beginGame();
            BaronBlade test = new BaronBlade();
            Card redistCard = new Card("Images\\Villain\\BaronBlade\\ElementalRedistributor.png");
            test.ElementalRedistributor(redistCard);

            Assert.AreEqual(GameEngine.getVillain().getMinions()[0].ToString(), (new ElementalRedistributor()).ToString());
        }

        [Test(), RequiresSTA]
        public void TestBattalion()
        {
            Start game = new Start();
            game.beginGame();
            BaronBlade test = new BaronBlade();
            Card battleCard = new Card("Images\\Villain\\BaronBlade\\BladeBattalion.png");
            test.BladeBattalion(battleCard);

            Assert.AreEqual(GameEngine.getVillain().getMinions()[0].ToString(), (new BladeBattalion()).ToString());
        }
    }
}
