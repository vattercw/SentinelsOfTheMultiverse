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

            Card fortitude = new Card("Images\\Hero\\Legacy\\Fortitude.png", "Rampage");
            haka.Rampage(rampageCard);
            Assert.AreEqual(GameEngine.getVillain().lifeTotal, 35);
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



    }
}
