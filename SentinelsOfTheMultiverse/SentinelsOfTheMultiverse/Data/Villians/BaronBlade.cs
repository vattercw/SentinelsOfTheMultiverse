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
        public static void HastenDoom(){

            DamageEffects.DealDamage(GameEngine.getHeroes(), null, null, 2, DamageEffects.DamageType.Toxic);
            GameEngine.getVillain().drawPhase(1);
        }

        public static void FleshRepairNanites()
        {
            GameEngine.getVillain().lifePoints += 10;
        }

        public static void DeviousDisruption()
        {

        }

        public static void SlashAndBurn()
        {
            int damage = GameEngine.getHeroes().Count;
            Hero lowestHP = GameEngine.getHeroes()[0];

            for (int i = 0; i < GameEngine.getHeroes().Count; i++)
            {
                if (lowestHP.lifePoints > GameEngine.getHeroes()[i].lifePoints)
                {
                    lowestHP = GameEngine.getHeroes()[i];
                }
            }

            Hero highestHP = GameEngine.getHeroes()[0];

            for (int i = 0; i < GameEngine.getHeroes().Count; i++)
            {
                if (highestHP.lifePoints < GameEngine.getHeroes()[i].lifePoints)
                {
                    highestHP = GameEngine.getHeroes()[i];
                }
            }
            int fireDamage = 2;

            DamageEffects.DealDamageToHero(lowestHP, damage, DamageEffects.DamageType.Melee);
            DamageEffects.DealDamageToHero(highestHP, damage + fireDamage, DamageEffects.DamageType.Fire);

        }

        public static void ConsiderThePriceOfVictory()
        {

        }

        //Ongoing Cards
        public static void LivingForceField()
        {

        }

        public static void BacklashField()
        {

        }

        //Devices and Minions

        public static void PoweredRemoteTurret()
        {
            GameEngine.getVillain().addMinion(new PoweredRemoteTurret(), Minion.MinionType.End);
        }

        public static void MobileDefencePlatform()
        {
            GameEngine.getVillain().addMinion(new MobileDefensePlatform(), Minion.MinionType.Ongoing);
        }

        public static void ElementalRedistributor()
        {
            GameEngine.getVillain().addMinion(new ElementalRedistributor(), Minion.MinionType.OnAttack);
        }

        public static void BladeBattalion()
        {
            GameEngine.getVillain().addMinion(new BladeBattalion(), Minion.MinionType.End);
        }
    }
}
