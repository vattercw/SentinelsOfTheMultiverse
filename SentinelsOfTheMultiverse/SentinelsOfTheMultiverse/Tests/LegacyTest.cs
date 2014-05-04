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
            Start testGame = new Start();
            testGame.beginGame();
            Legacy testLegacy = new Legacy();

            Card testDanger = new Card("Images\\Hero\\Legacy\\DangerSense.png", "DangerSense");
            testLegacy.DangerSense(testDanger);
            Assert.True(testLegacy.immuneToEnvironment);
        }

        [Test, RequiresSTA]
        public void TestFlyingSmash()
        {

        }

        //TODO fix test to get prompted selected array AND counteract static gamestate much code still not touched with this test
        [Test, RequiresSTA]
        public void TestBackFistStrike()
        {
            Start testGame = new Start();
            testGame.beginGame();
            Legacy testLegacy = new Legacy();

            Card testStrike = new Card("Images\\Hero\\Legacy\\BackFistStrike.png", "BackFistStrike");
            testLegacy.BackFistStrike(testStrike);
            Assert.AreEqual(GameEngine.getVillain().lifeTotal, 35); //change to 36 when done individually, 35 with all are executed, change when static problem fixed...
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
