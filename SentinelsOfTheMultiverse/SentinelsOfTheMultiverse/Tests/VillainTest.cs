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
            Villain villainTest = ObjectMother.TestVillain();
            Assert.NotNull(villainTest);
            Assert.AreEqual("BaronBlade", villainTest.getCharacterName());
        }

        [Test, RequiresSTA]
        public void TestVillainDrawCard()
        {
            Villain villainTest = ObjectMother.TestVillain();
            var begin = villainTest.cardsOnField.Count;
            villainTest.drawPhase(1);
            var end = villainTest.cardsOnField.Count;
            Assert.AreNotEqual(begin, end);
        }

        [Test, RequiresSTA]
        public void TestVillainLifeInit()
        {
            Villain villain = ObjectMother.TestVillain();
            Assert.AreEqual(villain.lifeTotal, 40);
        }

        [Test, RequiresSTA]
        public void TestGetMinionEmpty()
        {
            Villain test = ObjectMother.TestVillain();
            Assert.AreEqual(test.getMinions(), new List<Minion>());
        }

        [Test, RequiresSTA]
        public void TestGetMinion()
        {
            Villain test = ObjectMother.TestVillain();
            test.addMinion(ObjectMother.TestMinion());
            List<Minion> testList = new List<Minion>();
            testList.Add(ObjectMother.TestMinion());
            List<Minion> actual = test.getMinions();
            Assert.AreEqual(actual.ToString(), testList.ToString());
        }
    }
}
