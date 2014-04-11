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
            maxHealth = 10;
            health = 10;
            effectPhase = Minion.MinionType.End;
        }

        public override void executeEffect()
        {
            int numTurret = 0;
            for (int i = 0; i < GameEngine.getVillain().getEndPhaseMinions().Count; i++)
            {
                if (GameEngine.getVillain().getEndPhaseMinions()[i] is PoweredRemoteTurret)
                {
                    numTurret++;
                }
            }

            int extraDamage = numTurret - 1;

            for (int i = 0; i < GameEngine.getHeroes().Count; i++)
            {
                DamageEffects.DealDamage(GameEngine.getHeroes(), null, null, 2, DamageEffects.DamageType.Projectile);
            }
        }
    }
}
