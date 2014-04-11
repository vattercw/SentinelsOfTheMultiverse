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
            ObjectMother.TestGame();
            BaronBlade test = new BaronBlade();
            Card hastenCard = new Card("Images\\Villain\\BaronBlade\\HastenDoom.png", "HastenDoom");
            test.HastenDoom(hastenCard);

            for(int i=0; i < GameEngine.getHeroes().Count; i++){
            Assert.AreEqual(GameEngine.getHeroes()[i].lifeTotal, 32);
            }

        }

        [Test(), RequiresSTA]
        public void TestDeviousDisruption()
        {
            //test
        }

        [Test(), RequiresSTA]
        public void TestFleshRepairNanites()
        {
            ObjectMother.TestGame();
            BaronBlade test = new BaronBlade();
            Card nanoCard = new Card("Images\\Villain\\BaronBlade\\FleshRepairNanites.png", "FleshRepairNanites");
            test.FleshRepairNanites(nanoCard);

            Assert.AreEqual(GameEngine.getVillain().lifeTotal, 50);
        }

        [Test(), RequiresSTA]
        public void TestSlashAndBurn()
        {
            ObjectMother.TestGame();
            BaronBlade test = new BaronBlade();
            Card slashCard = new Card("Images\\Villain\\BaronBlade\\SlashAndBurn.png", "SlashAndBurn");
            test.SlashAndBurn(slashCard);

            for (int i = 0; i < GameEngine.getHeroes().Count; i++)
            {
                Assert.AreEqual(GameEngine.getHeroes()[i].lifeTotal, 31);
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
            ObjectMother.TestGame();
            BaronBlade test = new BaronBlade();
            Card turretCard = new Card("Images\\Villain\\BaronBlade\\PoweredRemoteTurret.png", "PoweredRemoteTurret");
            test.PoweredRemoteTurret(turretCard);

            Assert.AreEqual(GameEngine.getVillain().getMinions()[0].ToString(), (new PoweredRemoteTurret()).ToString());
        }

        [Test(), RequiresSTA]
        public void TestPlatform()
        {
            ObjectMother.TestGame();
            BaronBlade test = new BaronBlade();
            Card platCard = new Card("Images\\Villain\\BaronBlade\\MobileDefencePlatform.png", "MobileDefencePlatform");
            test.MobileDefencePlatform(platCard);

            Assert.AreEqual(GameEngine.getVillain().getMinions()[0].ToString(), (new MobileDefensePlatform()).ToString());
        }

        [Test(), RequiresSTA]
        public void TestRedistributor()
        {
            ObjectMother.TestGame();
            BaronBlade test = new BaronBlade();
            Card redistCard = new Card("Images\\Villain\\BaronBlade\\ElementalRedistributor.png", "ElementalRedistributor");
            test.ElementalRedistributor(redistCard);

            Assert.AreEqual(GameEngine.getVillain().getMinions()[0].ToString(), (new ElementalRedistributor()).ToString());
        }

        [Test(), RequiresSTA]
        public void TestBattalion()
        {
            ObjectMother.TestGame();
            BaronBlade test = new BaronBlade();
            Card battleCard = new Card("Images\\Villain\\BaronBlade\\BladeBattalion.png", "BladeBattalion");
            test.BladeBattalion(battleCard);

            Assert.AreEqual(GameEngine.getVillain().getMinions()[0].ToString(), (new BladeBattalion()).ToString());
        }
    }
}
