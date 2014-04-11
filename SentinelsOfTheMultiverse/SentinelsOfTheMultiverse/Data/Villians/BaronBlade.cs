using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SentinelsOfTheMultiverse.Data.Effects;
using SentinelsOfTheMultiverse.Data.Minions;

namespace SentinelsOfTheMultiverse.Data.Villains
{
    public class BaronBlade: Villain
    {
        public BaronBlade()
        {
            lifeTotal = 40;
        }

        //One Shot Cards
        public void HastenDoom(Card card)
        {

            DamageEffects.DealDamage(GameEngine.getHeroes(), null, null, 2, DamageEffects.DamageType.Toxic);
            GameEngine.getVillain().drawPhase(1);
        }

        public void FleshRepairNanites(Card card)
        {
            GameEngine.getVillain().lifeTotal += 10;
        }

        public static void DeviousDisruption(Card card)
        {

        }

        public void SlashAndBurn(Card card)
        {
            int damage = GameEngine.getHeroes().Count;
            Hero lowestHP = GameEngine.getHeroes()[0];

            for (int i = 0; i < GameEngine.getHeroes().Count; i++)
            {
                if (lowestHP.lifeTotal > GameEngine.getHeroes()[i].lifeTotal)
                {
                    lowestHP = GameEngine.getHeroes()[i];
                }
            }

            Hero highestHP = GameEngine.getHeroes()[0];

            for (int i = 0; i < GameEngine.getHeroes().Count; i++)
            {
                if (highestHP.lifeTotal < GameEngine.getHeroes()[i].lifeTotal)
                {
                    highestHP = GameEngine.getHeroes()[i];
                }
            }
            int fireDamage = 2;

            DamageEffects.DealDamageToHero(lowestHP, damage, DamageEffects.DamageType.Melee);
            DamageEffects.DealDamageToHero(highestHP, damage + fireDamage, DamageEffects.DamageType.Fire);

        }

        public void ConsiderThePriceOfVictory(Card card)
        {

        }

        //Ongoing Cards
        public void LivingForceField(Card card)
        {

        }

        public void BacklashField(Card card)
        {

        }

        //Devices and Minions

        public void PoweredRemoteTurret(Card card)
        {
            GameEngine.getVillain().addMinion(new PoweredRemoteTurret());
        }

        public void MobileDefencePlatform(Card card)
        {
            GameEngine.getVillain().addMinion(new MobileDefensePlatform());
        }

        public void ElementalRedistributor(Card card)
        {
            GameEngine.getVillain().addMinion(new ElementalRedistributor());
        }

        public void BladeBattalion(Card card)
        {
            GameEngine.getVillain().addMinion(new BladeBattalion());
        }
    }
}
