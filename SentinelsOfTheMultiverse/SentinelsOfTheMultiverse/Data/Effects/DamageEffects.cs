using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse.Data.Effects
{
    public static class DamageEffects
    {
        //event AttackEventHandler Attack;
        //internal delegate void AttackEventHandler(object sender, EventArgs e);

        public enum DamageType { Projectile, Fire, Ice, Melee, Toxic };

        public static bool inPlayBacklash { get; set; }

        private static int _globalDamageAmplification;
        public static int GlobalDamageAmplification
        {
            get
            {
                return _globalDamageAmplification;
            }
            set
            {
                _globalDamageAmplification = value;
            }
        }

        public static void DealDamage(List<Hero> heroes, Villain villain, List<Minion> minions, int damageAmount, DamageType damageType)
        {
            
            if (heroes != null)
            {
                foreach (Hero hero in heroes)
                {
                    DealDamageToHero(hero, damageAmount, damageType);
                    Console.WriteLine(hero.lifeTotal);
                }
            }
            if (villain != null)
            {
                DealDamageToVillain(villain, damageAmount, damageType);
                Console.WriteLine(villain.lifeTotal);
            }
            if (minions != null)
            {
                foreach (Minion minion in minions)
                {
                    DealDamageToMinion(minion, damageAmount, damageType);
                    Console.WriteLine(minion.health);
                }
            }
        }

        public static void DealDamageToHero(Hero hero, int damageAmount, DamageType damageType)
        {
            for (int i = 0; i < hero.getImmunities().Count; i++)
            {
                if (hero.getImmunities()[i].Equals(damageType))
                {
                    //probably give some type of "he's immune!" or some shit
                    return;
                }
            }
            int totalDamage = hero.damageAmplificationToPlayer + damageAmount;
            hero.lifeTotal -= totalDamage;
        }

        public static void DealDamageToVillain(Villain vil, int damageAmount, DamageType damageType)
        {
            if (vil != null)
            {
                int totalVillainDamage = vil.damageAmplificationToPlayer + damageAmount;
                vil.lifeTotal -= totalVillainDamage;
            }
        }

        public static void DealDamageToMinion(Minion targetMinion, int damageAmount, DamageType damageType)
        {
            int totalMinionDamage = targetMinion.getDamageAmplification() + damageAmount; //TODO: + getGlobalDamageAmplification();
            targetMinion.health -= totalMinionDamage;
            Console.WriteLine(GameEngine.getVillain().getMinions().ToString());
            if (targetMinion.health <= 0)
            {
                //make sure that it is adding/removing minions correctly
                Console.WriteLine(targetMinion.getMinionName() + " is dead.");
                if (GameEngine.getVillain().getMinions().Contains(targetMinion))
                {
                    GameEngine.getVillain().removeMinion(targetMinion);
                    Console.WriteLine(GameEngine.getVillain().getMinions().ToString());
                } else if (GameEngine.getEnvironment().getMinions().Contains(targetMinion))
                {
                    GameEngine.getEnvironment().removeMinion(targetMinion);
                    Console.WriteLine(GameEngine.getEnvironment().getMinions().ToString());
                }
            }
        }

        
    }
}
