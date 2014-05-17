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
            var mobileDefensePlatforms =GameEngine.getVillain().getMinions().FindAll(x => x is MobileDefensePlatform);

            for (int i = 0; i < GameEngine.getHeroes().Count; i++)
            {
                var targets = new List<Targetable>();
                targets.AddRange(GameEngine.getHeroes());
                DamageEffects.DealDamage(this, targets, 2 + mobileDefensePlatforms.Count, DamageEffects.DamageType.Projectile);
            }
        }
    }
}
