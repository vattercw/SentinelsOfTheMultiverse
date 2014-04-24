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
            Start testGame = new Start();
            testGame.beginGame();
            Legacy testLegacy = new Legacy();

            Card testFortitude = new Card("Images\\Hero\\Legacy\\Fortitude.png", "Fortitude");
            testLegacy.Fortitude(testFortitude);
            Assert.AreEqual(testLegacy.damageAmplificationToPlayer, -1);
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
            
        }

        [Test, RequiresSTA]
        public void TestTakeDown()
        {

        }

    }
}
