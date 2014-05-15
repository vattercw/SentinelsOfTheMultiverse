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
            lifeTotal = 15;
            effectPhase = Minion.MinionType.OnAttack;
        }


        public override void executeEffect()
        {
            throw new NotImplementedException();
        }
    }
}
