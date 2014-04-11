using NUnit.Framework;
using SentinelsOfTheMultiverse.Data;
using SentinelsOfTheMultiverse.Data.Heroes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SentinelsOfTheMultiverse.Data.Villains;

namespace SentinelsOfTheMultiverse.Tests
{
    [TestFixture]
    class HakaTest
    {
        [Test(),RequiresSTA]
        public void TestRampage(){
            //add mocks for GameEngine.getHeroes() and GameEngine.getVillain()
            ObjectMother.TestGame();
            Haka haka = (Haka)ObjectMother.TestHero();
            Villain baronBlade = new BaronBlade();
            //Minion min1 = ObjectMother.TestMinion();

            Card rampageCard = new Card("Images\\Hero\\Haka\\Rampage.png", "Rampage");
            haka.Rampage(rampageCard);
            Assert.AreEqual(baronBlade.lifeTotal, 38);
        }

        [Test(), RequiresSTA]
        public void TestElbowSmash()
        {
            //test smash
        }

    }
}
