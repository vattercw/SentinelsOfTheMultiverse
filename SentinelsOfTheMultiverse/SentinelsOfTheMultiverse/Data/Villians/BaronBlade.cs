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

        public object[] DeviousDisruption(Card card)
        {
            DisruptDiscardedAction act = DeviousDisruptionDiscardAction;
            return new object[] { act, GameEngine.ForcedEffect.DeviousDisruption, GameEngine.getPlayers() };
        }
        public delegate void DisruptDiscardedAction(int discardedCards, Hero target);

        void DeviousDisruptionDiscardAction(int discardedCards, Hero target)
        {
            int cardDamage = 0;

            foreach (Hero hero in GameEngine.getHeroes())
            {
                cardDamage = hero.cardsOnField.Count + cardDamage;
            }

            cardDamage = cardDamage + 3;

            DamageEffects.DealDamageToHero(target, cardDamage, DamageEffects.DamageType.Lightning);

        }

        public void SlashAndBurn(Card card)
        {
            int damage = GameEngine.getHeroes().Count;
            Hero lowestHP = Utility.GetHeroLowestHP();

            Hero highestHP = GameEngine.getHeroes()[0];

            highestHP = Utility.GetHeroHighestHP(highestHP);

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




        public object[] ConsiderThePriceOfVictory(Card card)
        {
            DiscardedAction act = ConsiderThePriceOfVictoryDiscardAction;
            return new object[]{act, GameEngine.getPlayers()};
        }
        public delegate void DiscardedAction(int discardedCards);

        void ConsiderThePriceOfVictoryDiscardAction(int discardedCards)
        {
            Console.WriteLine("number of discarded cards: " + discardedCards);
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



        /// <summary>
        /// These death powers do not exist for the villain
        /// </summary>

        public override void DeathPower1()
        {
            return;
        }

        public override void DeathPower2()
        {
            return;
        }

        public override void DeathPower3()
        {
            return;
        }
    }
}
