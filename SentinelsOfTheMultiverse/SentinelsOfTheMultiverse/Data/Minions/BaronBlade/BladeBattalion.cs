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
            health = 5;
            effectPhase = Minion.MinionType.End;
        }

        public override void executeEffect()
        {
            int damage = health;

            Hero highestHP = GameEngine.getHeroes()[0];

            for (int i = 0; i < GameEngine.getHeroes().Count; i++)
            {
                if (highestHP.lifePoints < GameEngine.getHeroes()[i].lifePoints)
                {
                    highestHP = GameEngine.getHeroes()[i];
                }
            }
            DamageEffects.DealDamageToHero(highestHP, damage, DamageEffects.DamageType.Melee);
        }
    }
}
