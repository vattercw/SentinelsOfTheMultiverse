using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SentinelsOfTheMultiverse.Data;
using SentinelsOfTheMultiverse.Data.Environments;
using SentinelsOfTheMultiverse.Data.Heroes;
using SentinelsOfTheMultiverse.Data.Minions;
using SentinelsOfTheMultiverse.Data.Villains;

namespace SentinelsOfTheMultiverse.Tests
{
    class ObjectMother
    {
        public static Hero TestHero()
        {
            return new Haka();
        }

        public static Villain TestVillain()
        {
            return new BaronBlade();
        }

        public static GameEnvironment TestEnvironment()
        {
            return new InsulaPrimus();
        }

        public static Minion TestMinion()
        {
            return new BladeBattalion();
        }
    }
}
