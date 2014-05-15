using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse.Data.Minions
{
    class ElementalRedistributor : Minion
    {

        public ElementalRedistributor()
        {
            maxHealth = 10;
            lifeTotal = 10;
            effectPhase = Minion.MinionType.OnAttack;
        }
        public override void executeEffect()
        {
            throw new NotImplementedException();
        }
    }
}
