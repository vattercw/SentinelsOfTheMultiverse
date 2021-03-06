﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse.Data.Effects
{
    public static class DamageEffects
    {
        public static List<DamageHandler> damageDealtHandlers = new List<DamageEffects.DamageHandler>();
        public delegate int DamageHandler(Targetable sender, Targetable receiver, ref int damageAmount, DamageType damageType);

        public enum DamageType { Projectile, Fire, Ice, Melee, Toxic, Lightning, All };


        public static bool inPlayBacklash { get; set; }

        public static void DealDamage(Targetable sender, List<Targetable> receivers, int damageAmount, DamageType damageType)
        {
            foreach (Targetable receiver in receivers)
            {
                int damageModifiers = 0;
                if (damageDealtHandlers.Count != 0)
                {
                    DamageHandler[] dummyDamageHandlers = new DamageHandler[damageDealtHandlers.Count];
                    damageDealtHandlers.CopyTo(dummyDamageHandlers);
                    foreach (DamageHandler dmgHand in dummyDamageHandlers)
                    {
                        damageModifiers += dmgHand(sender, receiver, ref damageAmount, damageType);
                        
                        if (damageAmount == 0){ //meaning the user is immune
                            damageModifiers = 0; //could add another pointer param for invincible in dmgHand 
                            break;
                        }
                    }
                }
                if (damageModifiers + damageAmount > receiver.maxHealth)
                    receiver.lifeTotal = receiver.maxHealth;
                else
                    receiver.lifeTotal -= damageModifiers + damageAmount;
            }
            
            
            /*
            if (heroes != null)
            {
                foreach (Hero hero in heroes)
                {
                    hero.lifeTotal -= damageModifiers + damageAmount;
                    Console.WriteLine(hero.lifeTotal);
                }
            }
            if (villain != null)
            {
                villain.lifeTotal -= damageModifiers + damageAmount;
                Console.WriteLine(villain.lifeTotal);
            }
            if (minions != null)
            {
                foreach (Minion minion in minions)
                {
                    minion.lifeTotal -= damageModifiers + damageAmount;
                    Console.WriteLine(minion.lifeTotal);
                }
            }
            */
        }
/*
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
            if (hero.lifeTotal <= 0)
            {
                //make him dead
            }
        }
        public static void DealDamageToVillain(Villain vil, int damageAmount, DamageType damageType)
        {
            if (vil != null)
            {
                int totalVillainDamage = vil.damageAmplificationToPlayer + damageAmount;
                vil.lifeTotal -= totalVillainDamage;
                if (vil.lifeTotal <= 0)
                {
                    GameBoard.WinCondition();
                }
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

        */
    }
}
