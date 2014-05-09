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
            maxHealth = 40;
        }

        public override void Power()
        {
            return;
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
            if (lowestHP.Equals(highestHP))
            {
                DamageEffects.DealDamageToHero(highestHP, damage + fireDamage, DamageEffects.DamageType.Fire);
            }
            else
            {
                DamageEffects.DealDamageToHero(lowestHP, damage, DamageEffects.DamageType.Melee);
                DamageEffects.DealDamageToHero(highestHP, damage + fireDamage, DamageEffects.DamageType.Fire);
            }

        }

        public void ConsiderThePriceOfVictory(Card card)
        {

        }

        //Ongoing Cards
        public void LivingForceField(Card card)
        {
            card.cardType = Card.CardType.Ongoing;

            GameEngine.getVillain().damageAmplificationToPlayer--;
        }

        public void BacklashField(Card card)
        {
            card.cardType = Card.CardType.Ongoing;
            DamageEffects.inPlayBacklash = true;
        }

        //Devices and Minions

        public void PoweredRemoteTurret(Card card)
        {
            PoweredRemoteTurret turret = new PoweredRemoteTurret();
            GameEngine.getVillain().addMinion(turret);
            Console.Write(turret.health);
        }

        public void MobileDefencePlatform(Card card)
        {
            MobileDefensePlatform platform = new MobileDefensePlatform();
            GameEngine.getVillain().addMinion(platform);
            Console.Write("Mobile Defense Platform: " + platform.health);
        }

        public void ElementalRedistributor(Card card)
        {
            ElementalRedistributor redist = new ElementalRedistributor();
            GameEngine.getVillain().addMinion(redist);
            Console.Write("Elemental Redistributor: "+redist.health);

        }

        public void BladeBattalion(Card card)
        {
            BladeBattalion bbat = new BladeBattalion();
            GameEngine.getVillain().addMinion(new BladeBattalion());
            Console.WriteLine("Blade Battalion: " + bbat.health);
            
        }
    }
}
