using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse.Data.Minions.InsulaPrimus
{
    class PterodactylThief : Minion
    {
        public PterodactylThief()
        {
            maxHealth = 10;
            health = 10;
            effectPhase = "attacked";
            GameEngine.getVillain().addMinion(this, effectPhase);
        }

        public override void executeEffect()
        {
            throw new NotImplementedException();
        }
    }
}
