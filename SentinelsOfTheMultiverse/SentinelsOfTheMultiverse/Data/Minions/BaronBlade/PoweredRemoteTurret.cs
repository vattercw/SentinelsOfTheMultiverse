using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SentinelsOfTheMultiverse.Data.Effects;

namespace SentinelsOfTheMultiverse.Data.Minions
{
    class PoweredRemoteTurret : Minion
    {

        public PoweredRemoteTurret()
        {
            maxHealth = 7;
            lifeTotal = 7;
            effectPhase = Minion.MinionType.End;
        }

        public override void executeEffect()
        {
            int numPlat = 0;
            for (int i = 0; i < GameEngine.getVillain().getEndPhaseMinions().Count; i++)
            {
                if (GameEngine.getVillain().getMinions()[i] is MobileDefensePlatform)
                {
                    numPlat++;
                }
            }

            int extraDamage = numPlat;

            for (int i = 0; i < GameEngine.getHeroes().Count; i++)
            {
                DamageEffects.DealDamage(this, GameEngine.getHeroes(), null, null, 2 + extraDamage, DamageEffects.DamageType.Projectile);
            }
        }
    }
}
