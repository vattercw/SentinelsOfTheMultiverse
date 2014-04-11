using NUnit.Framework;
using SentinelsOfTheMultiverse.Data;
using SentinelsOfTheMultiverse.Data.Heroes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SentinelsOfTheMultiverse.Data.Villains;
using SentinelsOfTheMultiverse.Data.Effects;

namespace SentinelsOfTheMultiverse.Tests
{
    [TestFixture]
    class HakaTest
    {
        [Test(),RequiresSTA]
        public void TestRampage(){
            //add mocks for GameEngine.getHeroes() and GameEngine.getVillain()
            Start game = new Start();
            game.beginGame();
            Haka haka = (Haka)ObjectMother.TestHero();
            Minion min1 = ObjectMother.TestMinion();
            GameEngine.getVillain().addMinion(min1);

            Card rampageCard = new Card("Images\\Hero\\Haka\\Rampage.png", "Rampage");
            haka.Rampage(rampageCard);
            Assert.AreEqual(GameEngine.getVillain().lifeTotal, 38);
        }

        [Test(), RequiresSTA]
        public void TestElbowSmash()
        {
            //test smash
        }

        [Test(), RequiresSTA]
        public void TestTaMoko()
        {
            Start game = new Start();
            game.beginGame();
            Haka haka = (Haka) ObjectMother.TestHero();
            int startingLifeTotal = haka.lifeTotal;

            Card tamokoCard = new Card("Images\\Hero\\Haka\\TaMoko.png", "TaMoko");
            haka.TaMoko(tamokoCard);
            DamageEffects.DealDamageToHero(haka, 2, DamageEffects.DamageType.Melee);
            
            Assert.AreEqual(startingLifeTotal-1, haka.lifeTotal);

            CardDrawingEffects.DestroyCard(tamokoCard, haka);
        }

    }
}
