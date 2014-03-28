using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SentinelsOfTheMultiverse.Data.Heroes;

namespace SentinelsOfTheMultiverse.Tests
{
    [TestFixture]
    class HeroTest
    {
        [Test, RequiresSTA]
        public void TestHeroInitalization()
        {
            Hero heroTest = new Haka();
            Assert.AreNotEqual(null, heroTest);
            Assert.AreEqual("Haka", heroTest.getCharacterName());
        }

    }
}
