using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse.Data.Minions.InsulaPrimus
{
    class VelociraptorPack : Minion
    {
        public VelociraptorPack()
        {
            maxHealth = 5;
            health = 5;
            effectPhase = "end";
            //GameEngine.getVillain().addMinion(this, effectPhase);
        }

        public override void executeEffect()
        {
            throw new NotImplementedException();
        }
    }
}
