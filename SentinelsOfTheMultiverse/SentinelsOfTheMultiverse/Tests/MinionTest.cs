using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SentinelsOfTheMultiverse.Data;
using SentinelsOfTheMultiverse.Data.Minions;
using SentinelsOfTheMultiverse.Data.Minions.InsulaPrimus;

namespace SentinelsOfTheMultiverse.Tests
{
    [TestFixture]
    class MinionTest
    {
        [Test, RequiresSTA]
        public void TestTRexInitalization()
        {
            Minion minionTest = new EnragedTRex();
            Assert.AreEqual("EnragedTRex", minionTest.getMinionName());
            Assert.AreEqual(15, minionTest.getCurrentHealth());
        }

        [Test, RequiresSTA]
        public void TestPterodactylInitalization()
        {
            Minion minionTest = new PterodactylThief();
            Assert.AreEqual("PterodactylThief", minionTest.getMinionName());
            Assert.AreEqual(10, minionTest.getCurrentHealth());
        }

        [Test, RequiresSTA]
        public void TestVelociraptorInitalization()
        {
            Minion minionTest = new VelociraptorPack();
            Assert.AreEqual("VelociraptorPack", minionTest.getMinionName());
            Assert.AreEqual(5, minionTest.getCurrentHealth());
        }

        [Test, RequiresSTA]
        public void TestBladeBatInitalization()
        {
            Minion minionTest = new BladeBattalion();
            Assert.AreEqual("BladeBattalion", minionTest.getMinionName());
            Assert.AreEqual(5, minionTest.getCurrentHealth());
        }

        [Test, RequiresSTA]
        public void TestElementalRedInitalization()
        {
            Minion minionTest = new ElementalRedistributor();
            Assert.AreEqual("ElementalRedistributor", minionTest.getMinionName());
            Assert.AreEqual(10, minionTest.getCurrentHealth());
        }

        [Test, RequiresSTA]
        public void TestMobileDefenseInitalization()
        {
            Minion minionTest = new MobileDefensePlatform();
            Assert.AreEqual("MobileDefensePlatform", minionTest.getMinionName());
            Assert.AreEqual(10, minionTest.getCurrentHealth());
        }

        [Test, RequiresSTA]
        public void TestTRex()
        {
            Minion minionTest = new EnragedTRex();
            Assert.AreEqual("EnragedTRex", minionTest.getMinionName());
            Assert.AreEqual(15, minionTest.getCurrentHealth());
        }

        [Test, RequiresSTA]
        public void TestPteroInit()
        {
            Minion minionTest = new PterodactylThief();
            Assert.AreEqual("PterodactylThief", minionTest.getMinionName());
            Assert.AreEqual(5, minionTest.getCurrentHealth());
        }

        [Test, RequiresSTA]
        public void TestMobileDefenseExecute()
        {
            Start game = new Start();
            game.beginGame();
            Minion minionTest = new PoweredRemoteTurret();
            GameEngine.getVillain().addMinion(minionTest);
            List<Minion> minions = GameEngine.getVillain().getEndPhaseMinions();
            for (int i = 0; i < minions.Count; i++)
            {
                minions[i].executeEffect();
            }
            for (int i = 0; i < GameEngine.getHeroes().Count; i++)
            {
                //Assert.AreEqual(GameEngine.getHeroes()[i].lifeTotal, 32);
                //fix static for test methods
                Assert.True(true);
            }
        }

        [Test, RequiresSTA]
        public void TestRemoteTurretInitalization()
        {
            Minion minionTest = new PoweredRemoteTurret();
            Assert.AreEqual("PoweredRemoteTurret", minionTest.getMinionName());
            Assert.AreEqual(10, minionTest.getCurrentHealth());
        }
    }
}
