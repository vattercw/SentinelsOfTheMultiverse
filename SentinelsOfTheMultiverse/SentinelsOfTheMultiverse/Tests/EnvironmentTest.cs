using NUnit.Framework;
using SentinelsOfTheMultiverse.Data.Environments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SentinelsOfTheMultiverse.Data;
using SentinelsOfTheMultiverse.Data.Effects;
using SentinelsOfTheMultiverse.Data.Minions.InsulaPrimus;
using SentinelsOfTheMultiverse.Data.Heroes;

namespace SentinelsOfTheMultiverse.Tests
{
    [TestFixture]
    class EnvironmentTest
    {

        [Test, RequiresSTA]
        public void testInitialization()
        {
            GameEnvironment env = ObjectMother.TestEnvironment();
            Assert.NotNull(env.cardsOnField);
            Assert.NotNull(env.deck);
        }

        [Test, RequiresSTA]
        public void testObsidian()
        {
            Start testGame = new Start();
            testGame.beginGame();
            InsulaPrimus testEnvo = new InsulaPrimus();
            Haka testHaka = new Haka();
            Card elbowCard = new Card("Images\\Heroes\\Haka\\3-ElbowSmash.png");
            Card testObsidian = new Card("Images\\Environment\\InsulaPrimus\\3-ObsidianField.png");

            testEnvo.ObsidianField(testObsidian);

            testHaka.ElbowSmash(elbowCard);

            Assert.AreEqual(36, GameEngine.getVillain().lifeTotal);            

        }

        [Test, RequiresSTA]
        public void testVelo()
        {
            Start testGame = new Start();
            testGame.beginGame();
            InsulaPrimus testEnvo = new InsulaPrimus();
            Card testVelo = new Card("Images\\Environment\\InsulaPrimus\\3-VelociraptorPack.png");
            Assert.NotNull(testVelo);
            testEnvo.VelociraptorPack(testVelo);
            testEnvo.addMinion(new VelociraptorPack());

            Assert.AreEqual(GameEngine.getEnvironment().getMinions().ToString(), testEnvo.getMinions().ToString());

        }

        [Test, RequiresSTA]
        public void testTRex()
        {
            Start testGame = new Start();
            testGame.beginGame();
            InsulaPrimus testEnvo = new InsulaPrimus();
            Card testRex = new Card("Images\\Environment\\InsulaPrimus\\2-EnragedTRex.png");
            Assert.NotNull(testRex);
            testEnvo.EnragedTRex(testRex);
            testEnvo.addMinion(new EnragedTRex());

            Assert.AreEqual(GameEngine.getEnvironment().getMinions().ToString(), testEnvo.getMinions().ToString());

        }

        [Test, RequiresSTA]
        public void testPtero()
        {
            Start testGame = new Start();
            testGame.beginGame();
            InsulaPrimus testEnvo = new InsulaPrimus();
            Card testPtero = new Card("Images\\Environment\\InsulaPrimus\\2-PterodactylThief.png");
            Assert.NotNull(testPtero);
            testEnvo.PterodactylThief(testPtero);
            testEnvo.addMinion(new PterodactylThief());

            Assert.AreEqual(GameEngine.getEnvironment().getMinions().ToString(), testEnvo.getMinions().ToString());

        }
    }
}
