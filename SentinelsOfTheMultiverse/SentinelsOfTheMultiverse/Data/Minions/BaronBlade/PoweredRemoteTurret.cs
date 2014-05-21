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

        public override object[] executeEffect()
        {
            var mobileDefensePlatforms =GameEngine.getVillain().getMinions().FindAll(x => x is PoweredRemoteTurret);

            var targets = new List<Targetable>();
            targets.AddRange(GameEngine.getHeroes());
            DamageEffects.DealDamage(this, targets, 2 + mobileDefensePlatforms.Count, DamageEffects.DamageType.Projectile);
            return null;
        }
    }
}
