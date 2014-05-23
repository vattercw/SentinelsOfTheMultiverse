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
            DiscardedAction act = DeviousDisruptionDiscardAction;
            return new object[] { act, GameEngine.ForcedEffect.DeviousDisruption, GameEngine.getPlayers() };
        }
        


        public void DeviousDisruptionDiscardAction(int discardedCards)
        {
            int cardDamage = 0;

            foreach (Hero hero in GameEngine.getHeroes())
            {
                cardDamage = hero.cardsOnField.Count + cardDamage;
            }

            cardDamage = cardDamage + 3;
            List<Targetable> targets= new List<Targetable>();
            targets.AddRange(GameEngine.getHeroes());
            DamageEffects.DealDamage(this, targets, cardDamage, DamageEffects.DamageType.Lightning);

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
            foreach (Hero h in GameEngine.getHeroes()) {
                DamageEffects.DealDamage(this, h, 2, DamageEffects.DamageType.Sonic); 
            }
            DiscardedAction act = ConsiderThePriceOfVictoryDiscardAction;
            return new object[]{act, GameEngine.ForcedEffect.ConsiderThePriceOfVictory, GameEngine.getPlayers()};
        }
        public delegate void DiscardedAction(int discardedCards);

        public void ConsiderThePriceOfVictoryDiscardAction(int discardedCards)
        {
            var cardsToRemove = deck.cards.GetRange(deck.cards.Count - discardedCards -1, discardedCards);
            Card[] dummyCardsToRemove = new Card[cardsToRemove.Count];
            cardsToRemove.CopyTo(dummyCardsToRemove);
            
            foreach(Card c in dummyCardsToRemove){
                c.SendToGraveyard(this, deck.cards);
            }
        }

        public void LivingForceField(Card card) {
            card.cardType = Card.CardType.Ongoing;
            card.CardDestroyed += (sender, args) => DamageEffects.damageDealtHandlers.Remove(LivingForceField_Damage_Handler);
            DamageEffects.damageDealtHandlers.Add(LivingForceField_Damage_Handler);
        }

        int LivingForceField_Damage_Handler(object sender, object receiver, ref int damageAmount, DamageEffects.DamageType damageType) {
            if (receiver.Equals(this))
                return -1;
            return 0;
        }

        public void BacklashField(Card card)
        {
            card.cardType = Card.CardType.Ongoing;
        }

        //Devices and Minions
        public void PoweredRemoteTurret(Card card) {
            PoweredRemoteTurret turret = new PoweredRemoteTurret();
            card.Minion = turret;
            card.CardDestroyed += PoweredRemoteTurret_Destroyed;
            EndPhase += turret.executeEffect;
            turret.MinionDied += () => card.SendToGraveyard(this, cardsOnField);
            addMinion(turret);
        }

        void PoweredRemoteTurret_Destroyed(Card c, EventArgs e) {
            removeMinion(c.Minion);
            EndPhase -= c.Minion.executeEffect;
        }

        public void MobileDefensePlatform(Card card)
        {
            MobileDefensePlatform platform = new MobileDefensePlatform();
            addMinion(platform);
            card.Minion = platform;
            DamageEffects.damageDealtHandlers.Add(MobileDefensePlatform_Effect);
            card.CardDestroyed += MobileDefensePlatform_CardDestroyed;
            platform.MinionDied += () => card.SendToGraveyard(this, cardsOnField); 
        }

        private void MobileDefensePlatform_CardDestroyed(Card m, EventArgs e) {
            removeMinion(m.Minion);
            DamageEffects.damageDealtHandlers.Remove(MobileDefensePlatform_Effect);
        }

        int MobileDefensePlatform_Effect(object sender, object receiver, ref int damageAmount, DamageEffects.DamageType damageType) {
            if (receiver.Equals(this))
                damageAmount = 0;
            return 0;
        }

        public void ElementalRedistributor(Card card)
        {
            ElementalRedistributor redist = new ElementalRedistributor();
            card.Minion = redist;
            DamageEffects.damageDealtHandlers.Add(ElementalRedistributor_DamageHandler);
            card.CardDestroyed += (sender, args)=> DamageEffects.damageDealtHandlers.Remove(ElementalRedistributor_DamageHandler);
            redist.MinionDied += () => card.SendToGraveyard(this, cardsOnField);
            addMinion(redist);
        }

        private int ElementalRedistributor_DamageHandler(Targetable sender, Targetable receiver, ref int damageAmount, DamageEffects.DamageType damageType) {
            if (receiver.Equals(this)) {
                if (damageType.Equals(DamageEffects.DamageType.Fire) || damageType.Equals(DamageEffects.DamageType.Cold) || damageType.Equals(DamageEffects.DamageType.Lightning)) {
                    Hero lowest= Utility.GetHeroLowestHP();
                    DamageEffects.DealDamage(sender, new List<Targetable>() { lowest }, damageAmount, damageType);
                    return (-1) * damageAmount; //negate the damage
                }
            }
            return 0;
        }

        public void BladeBattalion(Card card)
        {
            BladeBattalion bbat = new BladeBattalion();
            card.Minion = bbat;
            card.CardDestroyed += BladeBattalionDestroyed;
            EndPhase += bbat.executeEffect;
            bbat.MinionDied += () => card.SendToGraveyard(this, cardsOnField); 
            addMinion(bbat);
        }

        void BladeBattalionDestroyed(Card c, EventArgs e) {
            removeMinion(c.Minion);
            EndPhase -= c.Minion.executeEffect;
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
