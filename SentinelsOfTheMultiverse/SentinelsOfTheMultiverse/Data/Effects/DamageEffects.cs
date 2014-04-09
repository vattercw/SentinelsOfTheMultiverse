using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse.Data.Effects
{
    public static class DamageEffects
    {
        public enum DamageType { Projectile, Fire, Ice, Melee };
        private static int _damageAmplification;
        static int DamageAmplification
        {
            get
            {
                return _damageAmplification;
            }
            set
            {
                _damageAmplification = value;
            }
        }

        public static void DealDamage(List<Hero> heroes, Villain villain, List<Minion> minions, int damageAmount, DamageType damageType)
        {
            foreach (Hero hero in heroes)
            {
                int totalDamage = hero.getDamageAmplification() + damageAmount;
                DealDamageToHero(hero, totalDamage, damageType);
            }

            int totalVillainDamage = villain.getDamageAmplification() + damageAmount;
            DealDamageToVillain(totalVillainDamage, damageType);

            foreach (Minion minion in minions)
            {
                int totalMinionDamage = minion.getDamageAmplification() + damageAmount; //TODO: + getGlobalDamageAmplification();
                DealDamageToMinion(minion, totalMinionDamage, damageType);
            }
        }

        private static void DealDamageToHero(Hero hero, int damageAmount, DamageType damageType)
        {
            for (int i = 0; i < hero.getImmunities().Count; i++)
            {
                if (hero.getImmunities()[i].Equals(damageType))
                {
                    //probably give some type of "he's immune!" or some shit
                    return;
                }
            }
            hero.lifePoints -= damageAmount;
        }

        private static void DealDamageToVillain(int damageAmount, DamageType damageType)
        {
            
        }

        public static void DealDamageToMinion(Minion targetMinion, int damageAmount, DamageType damageType)
        {

        }
    }
}
