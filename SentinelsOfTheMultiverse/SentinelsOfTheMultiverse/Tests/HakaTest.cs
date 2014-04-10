using NUnit.Framework;
using SentinelsOfTheMultiverse.Data;
using SentinelsOfTheMultiverse.Data.Heroes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse.Tests
{
    [TestFixture]
    class HakaTest
    {
        [Test(),RequiresSTA]
        public void TestRampage(){
            //add mocks for GameEngine.getHeroes() and GameEngine.getVillain()

            Haka haka = (Haka)ObjectMother.TestHero();
            Villain baronBlade = ObjectMother.TestVillain();
            //Minion min1 = ObjectMother.TestMinion();

            Card rampageCard = new Card("C:\\Users\\rujirasl.000\\Documents\\GitHub\\SentinelsOfTheMultiverse\\SentinelsOfTheMultiverse\\SentinelsOfTheMultiverse\\Images\\Hero\\Haka\\Rampage.png", "Rampage");
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
