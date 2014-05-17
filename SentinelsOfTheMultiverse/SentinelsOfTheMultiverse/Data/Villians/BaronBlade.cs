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
            card.cardType = Card.CardType.OneShot;
            var targets = new List<Targetable>();
            targets.AddRange(GameEngine.getHeroes());
            DamageEffects.DealDamage(this, targets, 2, DamageEffects.DamageType.Toxic);
            drawPhase(1);
            card.SendToGraveyard(this, cardsOnField);
        }

        public void FleshRepairNanites(Card card)
        {
            card.cardType = Card.CardType.OneShot;
            HealEffects.healVillain(this, 10);
            card.SendToGraveyard(this, cardsOnField);
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

            DamageEffects.DealDamage(this, new List<Targetable>(){target}, cardDamage, DamageEffects.DamageType.Lightning);

        }

        public void SlashAndBurn(Card card)
        {
            int damage = GameEngine.getHeroes().Count;
            Hero lowestHP = Utility.GetHeroLowestHP();
            Hero highestHP = Utility.GetHeroHighestHP();

            int fireDamage = 2;
            DamageEffects.DealDamage(this, new List<Targetable>(){lowestHP}, damage, DamageEffects.DamageType.Melee);
            DamageEffects.DealDamage(this, new List<Targetable>(){highestHP}, damage + fireDamage, DamageEffects.DamageType.Fire);
            
            card.SendToGraveyard(this, cardsOnField);
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

            //GameEngine.getVillain().damageAmplificationToPlayer--;
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
            addMinion(turret);
            Console.Write(turret.lifeTotal);
        }

        public void MobileDefencePlatform(Card card)
        {
            MobileDefensePlatform platform = new MobileDefensePlatform();
            addMinion(platform);
            Console.Write("Mobile Defense Platform: " + platform.lifeTotal);
            //TODO: this isn't done
        }

        public void ElementalRedistributor(Card card)
        {
            ElementalRedistributor redist = new ElementalRedistributor();
            addMinion(redist);
            Console.Write("Elemental Redistributor: "+redist.lifeTotal);
            //TODO this isnt done
        }

        public void BladeBattalion(Card card)
        {
            BladeBattalion bbat = new BladeBattalion();
            card.Minion = bbat;
            card.CardDestroyed += BladeBattalionDestroyed;
            EndPhase += bbat.executeEffect;
            addMinion(bbat);
            Console.WriteLine("Blade Battalion: " + bbat.lifeTotal);
        }

        void BladeBattalionDestroyed(Card c, EventArgs e) {
            removeMinion(c.Minion);
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
