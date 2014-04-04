using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SentinelsOfTheMultiverse.Data;

namespace SentinelsOfTheMultiverse.Tests
{
    [TestFixture]
    class MinionTest
    {
        [Test, RequiresSTA]
        public void TestMinionInitalization()
        {
            Minion minionTest = new Minion("nameSpace", 100);
            Assert.AreEqual("nameSpace", minionTest.getMinionName());
            Assert.AreEqual(100, minionTest.getCurrentHealth());
        }
    }
}
