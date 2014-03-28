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
            Assert.AreNotEqual(null, villainTest);
            //Assert.AreEqual("Baron Blade", villainTest.getCharacterName());
        }

    }
}
