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
            health = 10;
            effectPhase = "ongoing";
            GameEngine.getVillain().addMinion(this, effectPhase);
        }

        public override void executeEffect()
        {
            GameEngine.getVillain().addImmunity("all");
        }
    }
}
