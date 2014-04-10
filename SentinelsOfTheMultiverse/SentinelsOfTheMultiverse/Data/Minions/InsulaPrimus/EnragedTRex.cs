using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse.Data.Minions.InsulaPrimus
{
    class EnragedTRex : Minion
    {
        public EnragedTRex()
        {
            maxHealth = 15;
            health = 15;
            effectPhase = "attacked";
            GameEngine.getVillain().addMinion(this, effectPhase);
        }


        public override void executeEffect()
        {
            throw new NotImplementedException();
        }
    }
}
