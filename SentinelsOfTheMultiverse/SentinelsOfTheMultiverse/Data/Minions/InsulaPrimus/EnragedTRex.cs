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
            maxHealth = 10;
            health = 10;
            effectPhase = "attacked";
        }


        public static override void executeEffect()
        {
            throw new NotImplementedException();
        }
    }
}
