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


        public override object[] executeEffect()
        {
            var orderedTargets = GameEngine.getTargets().OrderBy(x => x.lifeTotal).ToList();
            Targetable target;
            if (orderedTargets[orderedTargets.Count - 1] == this || orderedTargets[orderedTargets.Count - 2] == this) {
                target = orderedTargets[orderedTargets.Count - 3];
            } else {
                target = orderedTargets[orderedTargets.Count - 2];
            }
            Data.Effects.DamageEffects.DealDamage(this, new List<Targetable>() { target }, 5, Effects.DamageEffects.DamageType.Melee);
            return null;
        }
    }
}
