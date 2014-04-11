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
        public void HastenDoom(){

            DamageEffects.DealDamage(GameEngine.getHeroes(), null, null, 2, DamageEffects.DamageType.Toxic);
            GameEngine.getVillain().drawPhase(1);
        }

        public void FleshRepairNanites()
        {
            GameEngine.getVillain().lifeTotal += 10;
        }

        public static void DeviousDisruption()
        {

        }

        public void SlashAndBurn()
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

        public void ConsiderThePriceOfVictory()
        {

        }

        //Ongoing Cards
        public void LivingForceField()
        {

        }

        public void BacklashField()
        {

        }

        //Devices and Minions

        public void PoweredRemoteTurret()
        {
            GameEngine.getVillain().addMinion(new PoweredRemoteTurret());
        }

        public void MobileDefencePlatform()
        {
            GameEngine.getVillain().addMinion(new MobileDefensePlatform());
        }

        public void ElementalRedistributor()
        {
            GameEngine.getVillain().addMinion(new ElementalRedistributor());
        }

        public void BladeBattalion()
        {
            GameEngine.getVillain().addMinion(new BladeBattalion());
        }
    }
}
