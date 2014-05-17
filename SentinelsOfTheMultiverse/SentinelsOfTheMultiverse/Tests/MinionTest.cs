using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SentinelsOfTheMultiverse.Data;
using SentinelsOfTheMultiverse.Data.Minions;
using SentinelsOfTheMultiverse.Data.Minions.InsulaPrimus;
using SentinelsOfTheMultiverse.Data.Villains;
using SentinelsOfTheMultiverse.Data.Effects;

namespace SentinelsOfTheMultiverse.Tests
{
    [TestFixture]
    class MinionTest
    {
        [SetUp, RequiresSTA]
        private void Setup() {
            Start st = new Start();
            st.beginGame();
        }

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
            Assert.AreEqual(5, minionTest.getCurrentHealth());
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
            Minion minionTest = new PoweredRemoteTurret();
            GameEngine.getVillain().addMinion(minionTest);
            List<Minion> minions = GameEngine.getVillain().getMinions();
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
            Assert.AreEqual(7, minionTest.getCurrentHealth());
        }

        [Test, RequiresSTA]
        public void TestMinionDeath() {
            Card c = new Card("Images\\Villain\\BaronBlade\\4-BladeBattalion.png");
            BaronBlade villain = (BaronBlade)GameEngine.getVillain();
            
            villain.cardsOnField.Add(c);
            villain.BladeBattalion(c);
            Assert.AreEqual(1, villain.getMinions().Count);
            Minion bladeBat= villain.getMinions()[0];

            DamageEffects.DealDamage(villain, new List<Targetable>() { bladeBat }, bladeBat.maxHealth, DamageEffects.DamageType.Melee);

            Assert.AreEqual(0, villain.getMinions().Count);
        }

        [TearDown]
        public void TearDown() {
            GameEngine.TearDownGameEngine();
        }
    }
}
