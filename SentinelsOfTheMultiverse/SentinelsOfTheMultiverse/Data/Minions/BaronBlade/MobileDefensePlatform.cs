using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse.Data.Minions
{
    class MobileDefensePlatform : Minion
    {

        public MobileDefensePlatform()
        {
            maxHealth = 10;
            lifeTotal = 10;
            effectPhase = Minion.MinionType.Ongoing;
        }

        public override void executeEffect()
        {
        }
    }
}
