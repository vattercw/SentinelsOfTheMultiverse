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
            int damage = lifeTotal;

            Hero highestHP = GameEngine.getHeroes()[0];

            for (int i = 0; i < GameEngine.getHeroes().Count; i++)
            {
                if (highestHP.lifeTotal < GameEngine.getHeroes()[i].lifeTotal)
                {
                    highestHP = GameEngine.getHeroes()[i];
                }
            }
            DamageEffects.DealDamage(this, new List<Targetable>(){highestHP}, damage, DamageEffects.DamageType.Melee);
        }
    }
}
