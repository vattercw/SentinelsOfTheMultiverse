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
