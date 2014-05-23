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

        [SetUp(), RequiresSTA]
        public void Setup()
        {
            Start st = new Start();
            st.beginGame();
        }

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
            InsulaPrimus testEnvo = new InsulaPrimus();
            Card testRex = new Card("Images\\Environment\\InsulaPrimus\\2-EnragedTRex.png");
            Assert.NotNull(testRex);
            testEnvo.EnragedTRex(testRex);
            testEnvo.addMinion(new EnragedTRex());

            Assert.AreEqual(GameEngine.getEnvironment().getMinions().ToString(), testEnvo.getMinions().ToString());

        }


        [Test, RequiresSTA]
        public void TestPrimordial()
        {
            InsulaPrimus env = new InsulaPrimus();
            Card testPrime = new Card("Images\\Environment\\InsulaPrimus\\2-PrimordialPlantLife.png");
            env.PrimordialPlantLife(testPrime);

            env.PrimordialPlantLife_Continuation(1, GameEngine.getHeroes()[0], testPrime);
            env.PrimordialPlantLife_Continuation(0, GameEngine.getHeroes()[1], testPrime);
            Assert.AreEqual(GameEngine.getHeroes()[0].lifeTotal, GameEngine.getHeroes()[0].maxHealth - 2);
            Assert.AreEqual(GameEngine.getHeroes()[1].lifeTotal, GameEngine.getHeroes()[1].maxHealth - 4);
        }

        [TearDown()]
        public void TearDown()
        {
            GameEngine.TearDownGameEngine();

        }
    }
}
