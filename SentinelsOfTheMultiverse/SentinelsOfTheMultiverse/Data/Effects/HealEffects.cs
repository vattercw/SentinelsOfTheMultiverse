using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse.Data.Effects
{
    public static class healEffects
    {

        public static void Heal(List<Hero> heroes, Villain villain, List<Minion> minions, int healAmount)
        {
            
            if (heroes != null)
            {
                foreach (Hero hero in heroes)
                {
                    healHero(hero, healAmount);
                    Console.WriteLine(hero.lifeTotal);
                }
            }
            if (villain != null)
            {
                healVillain(villain, healAmount);
                Console.WriteLine(villain.lifeTotal);
            }
            if (minions != null)
            {
                foreach (Minion minion in minions)
                {
                    healMinion(minion, healAmount);
                    Console.WriteLine(minion.health);
                }
            }
        }

        public static void healHero(Hero hero, int healAmount)
        {
            if (hero != null)
            {
                if (hero.lifeTotal >= hero.maxHealth)
                {
                    //make sure target doesn't go over maxHealth
                    hero.lifeTotal = hero.maxHealth;
                }
            }
        }

        public static void healVillain(Villain vil, int healAmount)
        {
            if (vil != null)
            {
                if (vil.lifeTotal >= vil.maxHealth)
                {
                    //make sure target doesn't go over maxHealth
                    vil.lifeTotal = vil.maxHealth;
                }
            }
        }

        public static void healMinion(Minion targetMinion, int healAmount)
        {
            targetMinion.health += healAmount;
            Console.WriteLine(GameEngine.getVillain().getMinions().ToString());
            if (targetMinion.health >= targetMinion.maxHealth)
            {
                //make sure target doesn't go over maxHealth
                targetMinion.health = targetMinion.maxHealth;
            }
        }
    }
}
