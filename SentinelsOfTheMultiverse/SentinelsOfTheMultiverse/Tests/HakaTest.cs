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

            haka.Rampage();
            Assert.AreEqual(baronBlade.lifePoints, 38);
        }

    }
}
