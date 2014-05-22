using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SentinelsOfTheMultiverse.Data;
using SentinelsOfTheMultiverse.Data.Effects;
using SentinelsOfTheMultiverse.Data.Heroes;
using SentinelsOfTheMultiverse.Data.Minions;
using SentinelsOfTheMultiverse.Data.Villains;

namespace SentinelsOfTheMultiverse.Tests
{
    [TestFixture]
    class HealTest
    {
        [Test, RequiresSTA]
        public void TestHeal()
        {
            Legacy legacy = new Legacy();
            Haka haka = new Haka();
            List<Hero> heroes = new List<Hero>();
            legacy.lifeTotal = legacy.maxHealth - 5;
            haka.lifeTotal = haka.maxHealth - 5;
            BaronBlade blade = new BaronBlade();
            blade.lifeTotal = blade.maxHealth - 5;
            MobileDefensePlatform plat = new MobileDefensePlatform();
            plat.lifeTotal = plat.maxHealth - 5;
            List<Minion> minion = new List<Minion>();
            minion.Add(plat);
            heroes.Add(legacy);
            heroes.Add(haka);
            HealEffects.Heal(heroes, blade, minion, 5);

            Assert.AreEqual(heroes[0].lifeTotal, heroes[0].maxHealth);
            Assert.AreEqual(heroes[1].lifeTotal, heroes[1].maxHealth);
            

            Assert.AreEqual(blade.lifeTotal, blade.maxHealth);

            foreach (Minion min in minion)
            {
                Assert.AreEqual(min.lifeTotal, min.maxHealth);
            }
            
        }
    }
}
