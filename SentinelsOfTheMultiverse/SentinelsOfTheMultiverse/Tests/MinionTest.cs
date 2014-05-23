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
        [SetUp(), RequiresSTA]
        public void Setup() {
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
            Card c = new Card("Images\\Villain\\BaronBlade\\3-MobileDefensePlatform.png");
            BaronBlade villain = (BaronBlade)GameEngine.getVillain();
            villain.cardsOnField.Add(c);
            villain.MobileDefensePlatform(c);

            DamageEffects.DealDamage(null, new List<Targetable>() { villain }, 10, DamageEffects.DamageType.Fire);
            Assert.AreEqual(villain.maxHealth, villain.lifeTotal);
        }

        [Test, RequiresSTA]
        public void TestElementalRedistributor() {
            Card c = new Card("Images\\Villain\\BaronBlade\\2-ElementalRedistributor.png");
            BaronBlade villain = (BaronBlade)GameEngine.getVillain();
            
            var lowestHero = Utility.GetHeroLowestHP();
            
            villain.cardsOnField.Add(c);
            villain.ElementalRedistributor(c);

            //should redirect the damage
            DamageEffects.DealDamage(null, new List<Targetable>() { villain }, 10, DamageEffects.DamageType.Fire);
            Assert.AreEqual(villain.maxHealth, villain.lifeTotal);
            Assert.AreEqual(lowestHero.maxHealth - 10, lowestHero.lifeTotal);

            //villain should take damage
            DamageEffects.DealDamage(null, new List<Targetable>() { villain }, 10, DamageEffects.DamageType.Melee);
            Assert.AreNotEqual(villain.maxHealth, villain.lifeTotal);
            Assert.AreEqual(lowestHero.maxHealth - 10, lowestHero.lifeTotal);
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

        [Test, RequiresSTA]
        public void TestMinionsCardDestroyed() {
            Card c = new Card("Images\\Villain\\BaronBlade\\4-BladeBattalion.png");
            BaronBlade villain = (BaronBlade)GameEngine.getVillain();
            villain.cardsOnField.Add(c);
            villain.BladeBattalion(c);
         
            c.SendToGraveyard(villain, villain.cardsOnField);
            Assert.AreEqual(0, villain.cardsOnField.Count);
            Assert.AreEqual(0, villain.getMinions().Count);
        }

        [Test, RequiresSTA]
        public void VelociraptorEffect()
        {
            VelociraptorPack velo = new VelociraptorPack();
            Card c = new Card("Images\\Environment\\InsulaPrimus\\3-VelociraptorPack.png");
            GameEngine.getEnvironment().cardsOnField.Add(c);
            velo.executeEffect();
            Assert.AreEqual(GameEngine.getHeroes()[1].lifeTotal, 30);
        }

        [TearDown()]
        public void TearDown() {
            GameEngine.TearDownGameEngine();

        }
    }
}
