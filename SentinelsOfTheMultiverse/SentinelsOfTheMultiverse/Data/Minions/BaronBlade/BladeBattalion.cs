using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SentinelsOfTheMultiverse.Data.Effects;

namespace SentinelsOfTheMultiverse.Data.Minions
{
    class BladeBattalion : Minion
    {
        public BladeBattalion()
        {
            maxHealth = 5;
            lifeTotal = 5;
            effectPhase = Minion.MinionType.End;
        }

        public override void executeEffect()
        {
            List<Targetable> targets = new List<Targetable>() { Utility.GetHeroHighestHP() };
            DamageEffects.DealDamage(this, targets, lifeTotal, DamageEffects.DamageType.Melee);
        }
    }
}
