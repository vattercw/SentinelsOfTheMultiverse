using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SentinelsOfTheMultiverse.Data;
using SentinelsOfTheMultiverse.Data.Heroes;
using SentinelsOfTheMultiverse.Data.Villains;

namespace SentinelsOfTheMultiverse.Tests
{
    class ObjectMother
    {
        public static Hero TestHero()
        {
            return new Haka();
        }

        public static Villain TestVillian()
        {
            return new BaronBlade();
        }
    }
}
