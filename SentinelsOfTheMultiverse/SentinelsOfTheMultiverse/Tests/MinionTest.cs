using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SentinelsOfTheMultiverse.Data;
using SentinelsOfTheMultiverse.Data.Minions.InsulaPrimus;

namespace SentinelsOfTheMultiverse.Tests
{
    [TestFixture]
    class MinionTest
    {
        [Test, RequiresSTA]
        public void TestMinionInitalization()
        {
            Minion minionTest = new EnragedTRex();
            Assert.AreEqual("EnragedTRex", minionTest.getMinionName());
            Assert.AreEqual(15, minionTest.getCurrentHealth());
        }
    }
}
