using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SentinelsOfTheMultiverse.Data;
using SentinelsOfTheMultiverse.Data.Heroes;

namespace SentinelsOfTheMultiverse.Tests
{

    [TestFixture]
    class LegacyTest
    {
        [Test, RequiresSTA]
        public void TestFortitude()
        {
            Start game = new Start();
            game.beginGame();
            Legacy legacy = new Legacy();

            Card fortitude = new Card("Images\\Hero\\Legacy\\Fortitude.png", "Fortitude");
            legacy.Fortitude(fortitude);
            Assert.AreEqual(legacy.damageAmplificationToPlayer, -1);
        }

        [Test, RequiresSTA]
        public void TestDangerSense()
        {

        }

        [Test, RequiresSTA]
        public void TestFlyingSmash()
        {

        }

        [Test, RequiresSTA]
        public void TestBackFistStrike()
        {

        }

        [Test, RequiresSTA]
        public void TestMotivationalCharge()
        {

        }

        [Test, RequiresSTA]
        public void TestInspiringPresence()
        {

        }

        [Test, RequiresSTA]
        public void TestLeadFromFront()
        {

        }

        [Test, RequiresSTA]
        public void TestSuperhuman()
        {
            Start game = new Start();
            game.beginGame();
            Legacy legacy = new Legacy();

            Card superhuman = new Card("Images\\Hero\\Legacy\\SuperhumanDurability.png", "SuperhumanDurability");
            legacy.SuperhumanDurability(superhuman);
            Assert.AreEqual(legacy.damageAmplificationFromPlayer, 1);
        }

        [Test, RequiresSTA]
        public void TestNextEvolution()
        {

        }

        [Test, RequiresSTA]
        public void TesSurgeOfStrength()
        {

        }

        [Test, RequiresSTA]
        public void TestThokk()
        {

        }
        [Test, RequiresSTA]
        public void TestLegacyRing()
        {
            Start game = new Start();
            game.beginGame();
            Legacy legacy = new Legacy();

            Card ring = new Card("Images\\Hero\\Legacy\\TheLegacyRing.png", "TheLegacyRing");
            legacy.TheLegacyRing(ring);
            Assert.AreEqual(legacy.numPowers, 2);
        }

        [Test, RequiresSTA]
        public void TestTakeDown()
        {

        }

    }
}
