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
            lifeTotal = 5;
            effectPhase = Minion.MinionType.End;
            //GameEngine.getVillain().addMinion(this, effectPhase);
        }

        public override object[] executeEffect()
        {
            var orderedTargets = GameEngine.getTargets().OrderBy(x => x.lifeTotal).ToList();
            int weakest = 0;
            while (orderedTargets[weakest] == this || typeof(GameEnvironment).IsAssignableFrom(this.GetType()))
            {

                weakest++;
            }
            int damage = 2;
            List<Card> velociraptorNum = GameEngine.getEnvironment().cardsOnField.FindAll(x=>x.getName().Equals(minionName));
            damage = damage * velociraptorNum.Count;
            Data.Effects.DamageEffects.DealDamage(this, new List<Targetable>() { orderedTargets[weakest] }, damage, Effects.DamageEffects.DamageType.Melee);
            return null;
        }
    }
}
