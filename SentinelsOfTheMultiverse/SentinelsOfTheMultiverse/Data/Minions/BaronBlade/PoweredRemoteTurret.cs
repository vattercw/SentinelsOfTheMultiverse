using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse.Data.Minions
{
    class PoweredRemoteTurret : Minion
    {

        public PoweredRemoteTurret()
        {
            maxHealth = 10;
            health = 10;
            effectPhase = "end";
        }

        public override void executeEffect()
        {
            throw new NotImplementedException();
        }
    }
}
