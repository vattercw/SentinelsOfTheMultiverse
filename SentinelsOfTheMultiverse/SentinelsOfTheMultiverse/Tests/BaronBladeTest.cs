using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SentinelsOfTheMultiverse.Data;
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
            //test
        }

        [Test(), RequiresSTA]
        public void TestSlashAndBurn()
        {
            //test
        }

        [Test(), RequiresSTA]
        public void TestConsiderThePrice()
        {
            //test
        }

        [Test(), RequiresSTA]
        public void TestLivingForceField()
        {
            //test
        }

        [Test(), RequiresSTA]
        public void TestBackLashField()
        {
            //test
        }
    }
}
