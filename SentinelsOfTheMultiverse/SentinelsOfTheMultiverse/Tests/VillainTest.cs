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
    [TestFixture]
    class VillainTest
    {
        [Test, RequiresSTA]
        public void TestVillainInitalization()
        {
            Villain villainTest = new BaronBlade();
            Assert.NotNull(villainTest);
            Assert.AreEqual("BaronBlade", villainTest.getCharacterName());
        }

        [Test, RequiresSTA]
        public void TestVillainDrawCard()
        {
            Villain villainTest = new BaronBlade();
            var begin = villainTest.cardsOnField.Count;
            villainTest.drawPhase(1);
            var end = villainTest.cardsOnField.Count;
            Assert.AreNotEqual(begin, end);
        }

    }
}
