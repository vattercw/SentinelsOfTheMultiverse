using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse.Data.Effects
{
    public abstract class OneShotEffect: IEffect
    {
        string effectName;
        private int _damageAmplification;
        int DamageAplification { get; set; }

        public OneShotEffect()
        {
            effectName = GetType().Name;
        }

        public void DealDamage(List<Hero> heroes, Villain villain, List<Minion> minions, int damageAmount, IEffect.DamageType damageType){
            foreach (Hero hero in heroes)
            {
                int totalDamage = hero.getDamageAmplification() + damageAmount;
                DealDamageToHero(hero, totalDamage, damageType);
            }

            int totalVillainDamage = villain.getDamageAmplification() + damageAmount;
            DealDamageToVillain(totalVillainDamage, damageType);

            foreach (Minion minion in minions)
            {
                int totalVillainDamage = villain.getDamageAmplification() + damageAmount + getGlobalDamageAmplification();
                DealDamageToMinion(minion, damageAmount, damageType);
            }
        }

        public void DealDamageToHero(Hero hero, int damageAmount, IEffect.DamageType damageType)
        {
            
        }

        public void DealDamageToVillain(int damageAmount, IEffect.DamageType damageType)
        {

        }

        public void DealDamageToMinion(Minion targetMinion, int damageAmount, IEffect.DamageType damageType)
        {

        }
    }
}
