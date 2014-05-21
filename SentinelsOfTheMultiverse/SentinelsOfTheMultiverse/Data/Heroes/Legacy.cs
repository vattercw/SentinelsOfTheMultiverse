﻿using SentinelsOfTheMultiverse.Data.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SentinelsOfTheMultiverse.Data.Heroes
{
    public class Legacy : Hero
    {
        public Legacy()
        {
            lifeTotal = 32;
            maxHealth = 32;
        }


        public override void Power()
        {
            //until next turn heros do one more damage
            foreach (Hero hero in GameEngine.getHeroes())
            {
                //hero.damageAmplificationFromPlayer++;
            }
        }

        public void Fortitude(Card card) {
            card.cardType = Card.CardType.Ongoing;
            card.CardDestroyed += (sender, args) => DamageEffects.damageDealtHandlers.Remove(Fortitude_Damage_Handler);
            DamageEffects.damageDealtHandlers.Add(Fortitude_Damage_Handler);
        }

        int Fortitude_Damage_Handler(object sender, object receiver, ref int damageAmount, DamageEffects.DamageType damageType) {
            if (receiver.Equals(this))
                return -1;
            return 0;
        }


        public void DangerSense(Card card)
        {
            card.cardType = Card.CardType.Ongoing;
            DamageEffects.damageDealtHandlers.Add(DangerSense_DamageHandler);
            card.CardDestroyed += (sender, args) => DamageEffects.damageDealtHandlers.Remove(DangerSense_DamageHandler);
        }

        private int DangerSense_DamageHandler(Targetable sender, Targetable receiver, ref int damageAmount, DamageEffects.DamageType damageType) {
            if (typeof(GameEnvironment).IsAssignableFrom(sender.GetType()) || GameEngine.getEnvironment().getMinions().Contains(sender)) {
                damageAmount = 0;
            }
            return 0;
        }


        public void FlyingSmash(Card card)
        {
            card.cardType = Card.CardType.OneShot;

            var target = GameBoard.cardClickedArray;
            if (target.Count > 3)
            {
                MessageBox.Show("Please select 3 or fewer targets. \n No select target default's to the Villain.");
                Hero currentPlayer = (Hero)GameEngine.getCurrentPlayer();
                currentPlayer.hand.Add(card);
                currentPlayer.graveyard.Remove(card);
                GameEngine.playerPlayedCard = false;
            }
            else if (target.Count >= 1 && target.Count <= 3)
            {
                var villainMinions = GameEngine.getVillain().getMinions();
                var environMinions = GameEngine.getEnvironment().getMinions();

                Boolean minBool = false;

                List<Minion> minionsToAttack = new List<Minion>();
            
                foreach (Minion min in villainMinions)
                {
                    for (int k = 0; k < target.Count; k++)
                    {
                        if (min.minionName.Equals(((Card)target[k]).getName()))
                        {
                            minionsToAttack.Add(min);
                            minBool = true;
                        }
                    }
                }

                foreach (Minion min in environMinions)
                {
                    for (int k = 0; k < target.Count; k++)
                    {
                        if (min.minionName.Equals(((Card)target[k]).getName()))
                        {
                            minionsToAttack.Add(min);
                            minBool = true;
                            break;
                        }
                    }
                }

                if (minBool)
                {
                    var targets = new List<Targetable>();
                    targets.AddRange(minionsToAttack);
                    DamageEffects.DealDamage(this, targets, 3, DamageEffects.DamageType.Melee);
                    if (targets.Count < 3)
                    {
                        DamageEffects.DealDamage(this, new List<Targetable>() { GameEngine.getVillain() }, 3, DamageEffects.DamageType.Melee);
                    }
                    card.SendToGraveyard(this, cardsOnField);
                }
                else MessageBox.Show("Please select an appropriate card.");
            }
            else
            {
                DamageEffects.DealDamage(this, new List<Targetable>() { GameEngine.getVillain() }, 3, DamageEffects.DamageType.Melee);
                card.SendToGraveyard(this, cardsOnField);
            }
        }

        public void BackfistStrike(Card card){
            card.cardType = Card.CardType.OneShot;

            var target = GameBoard.cardClickedArray;
            if (target.Count > 1)
            {
                MessageBox.Show("Select only one target to perform Back-Fist Strike. \n No select target default's to the Villain.");
                Hero currentPlayer = (Hero)GameEngine.getCurrentPlayer();
                currentPlayer.hand.Add(card);
                currentPlayer.graveyard.Remove(card);
                GameEngine.playerPlayedCard = false;
            }
            else if (target.Count == 1)
            {
                var villainMinions = GameEngine.getVillain().getMinions();
                var environMinions = GameEngine.getEnvironment().getMinions();

                Boolean minBool = false;

                List<Minion> minionsToAttack = new List<Minion>();

                foreach (Minion min in villainMinions)
                {
                    if (min.minionName.Equals(((Card)target[0]).getName()))
                    {
                        minionsToAttack.Add(min);
                        minBool = true;
                    }
                }

                foreach (Minion min in environMinions)
                {
                    if (min.minionName.Equals(((Card)target[0]).getName()))
                    {
                        minionsToAttack.Add(min);
                        minBool = true;
                    }
                }

                if (minBool)
                {
                    List<Targetable> targets = new List<Targetable>();
                    targets.AddRange(minionsToAttack);
                    DamageEffects.DealDamage(this, targets , 4, DamageEffects.DamageType.Melee);
                    card.SendToGraveyard(this, cardsOnField);
                }
                else MessageBox.Show("Please select an appropriate card.");
            }
            else
            {
                DamageEffects.DealDamage(this, new List<Targetable>(){ GameEngine.getVillain()}, 4, DamageEffects.DamageType.Melee);
                card.SendToGraveyard(this, cardsOnField);
            }
        }

        public void BolsterAllies(Card card)
        {
            card.cardType = Card.CardType.OneShot;

            foreach (Hero player in GameEngine.getHeroes())
            {
                player.drawPhase(1);
            }
            card.SendToGraveyard(this, cardsOnField);
        }

        public void MotivationalCharge(Card card)
        {
            card.cardPower = new Card.Power(MotivationalChargePower);
        }

        void MotivationalChargePower(Card card, object[] args)
        {
            var target = GameBoard.cardClickedArray;
            if (target.Count > 1)
            {
                MessageBox.Show("Select only one target to deal damage to. \n No select target default's to the Villain.");
                Hero currentPlayer = (Hero)GameEngine.getCurrentPlayer();
                cardsOnField.Remove(card);
                currentPlayer.hand.Add(card);
                GameEngine.playerPlayedCard = false;
            }
            else if (target.Count == 1)
            {
                var villainMinions = GameEngine.getVillain().getMinions();
                var environMinions = GameEngine.getEnvironment().getMinions();
                
                foreach (Minion min in villainMinions)
                {
                    if (min.minionName.Equals(((Card)target[0]).getName()))
                    {
                        DamageEffects.DealDamage(this, new List<Targetable>(){min}, 2, DamageEffects.DamageType.Melee);
                        break;
                    }
                }

                foreach (Minion min in environMinions)
                {
                    if (min.minionName.Equals(((Card)target[0]).getName()))
                    {
                        DamageEffects.DealDamage(this, new List<Targetable>() { min }, 2, DamageEffects.DamageType.Melee);
                        break;
                    }
                }
            }
            else
            {
                DamageEffects.DealDamage(this, new List<Targetable>() {GameEngine.getVillain() }, 2, DamageEffects.DamageType.Melee);
            }

            foreach (Hero hero in GameEngine.getHeroes())
            {
                HealEffects.healHero(hero, 1);
            }
        }

        public void InspiringPresence(Card card)
        {
            foreach (Hero hero in GameEngine.getHeroes())
            {
                HealEffects.healHero(hero, 1);
                //change attack
                card.cardType = Card.CardType.Ongoing;
                DamageEffects.damageDealtHandlers.Add(InspiringHandler);
            }
        }
       
        public int InspiringHandler(Targetable sender, Targetable receiver, ref int damageAmount, DamageEffects.DamageType damageType)
        {
            if(typeof(Hero).IsAssignableFrom(sender.GetType()))
                return 1;
            return 0;
        }
        
        public void LeadFromTheFront(Card card)
        {
            DamageEffects.damageDealtHandlers.Add(LeadFromTheFront_Damage_Handler);
            card.CardDestroyed += LeadFromTheFront_Destroyed_Handler;

        }

        private void LeadFromTheFront_Destroyed_Handler(Card m, EventArgs e)
        {
            DamageEffects.damageDealtHandlers.Remove(LeadFromTheFront_Damage_Handler);
        }

        public int LeadFromTheFront_Damage_Handler(Targetable sender, Targetable receiver, ref int damageAmount, DamageEffects.DamageType damageType)
        {
            bool receiverIsNotLegacy = !receiver.GetType().Equals(typeof(Legacy));
            if (receiverIsNotLegacy)
            {
                if (typeof(Villain).IsAssignableFrom(sender.GetType()) || GameEngine.getVillain().getMinions().Contains(sender))
                {
                    DialogResult dialogResult = MessageBox.Show("Do you want to have Legacy take the damage?", "Lead From The Front Effect", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        DamageEffects.DealDamage(sender, new List<Targetable>() { this }, damageAmount, damageType);
                        return (-1) * damageAmount;//balances out the amount that it deals to player that isn't haka
                    }
                }
            }
            return 0;
        }

        public void SuperhumanDurability(Card card)
        {
            DamageEffects.damageDealtHandlers.Add(SuperhumanDurability_DamageHandler);
            card.cardType = Card.CardType.Ongoing;
            card.CardDestroyed += SuperhumanDurability_Destroyed_Handler;
        }

        private void SuperhumanDurability_Destroyed_Handler(Card m, EventArgs e)
        {
            DamageEffects.damageDealtHandlers.Remove(SuperhumanDurability_DamageHandler);
        }

        private void card_CardDestroyed(Card m, EventArgs e) {
            m.SendToGraveyard(this, cardsOnField);
        }

        private int SuperhumanDurability_DamageHandler(Targetable sender, Targetable receiver, ref int damageAmount, DamageEffects.DamageType damageType) {
            if(receiver.Equals(this)){
                if (damageAmount >= 5) {
                    return -3;
                }
            }
            return 0;
        }

        public void NextEvolution(Card card)
        {
            //TODO implement this method
        }

        public void SurgeOfStrength(Card card)
        {
            DamageEffects.damageDealtHandlers.Add(SurgeOfStrength_Damage_Handler);
            card.cardType = Card.CardType.Ongoing;
            card.CardDestroyed += SurgeOfStrength_Destroyed_Handler;
        }

        private void SurgeOfStrength_Destroyed_Handler(Card m, EventArgs e)
        {
            DamageEffects.damageDealtHandlers.Remove(SurgeOfStrength_Damage_Handler);
        }

        private int SurgeOfStrength_Damage_Handler(Targetable sender, Targetable receiver, ref int damageAmount, DamageEffects.DamageType damageType)
        {
            if (sender.Equals(this))
                return 1;
            return 0;
        }

        public void Thokk(Card card)
        {
            card.cardType = Card.CardType.OneShot;

            var target = GameBoard.cardClickedArray;
            if (target.Count > 1)
            {
                MessageBox.Show("Select only one target to perform Thokk \n No select target default's to the Villain.");
                Hero currentPlayer = (Hero)GameEngine.getCurrentPlayer();
                currentPlayer.hand.Add(card);
                currentPlayer.graveyard.Remove(card);
                GameEngine.playerPlayedCard = false;
            }
            else if (target.Count == 1)
            {
                var villainMinions = GameEngine.getVillain().getMinions();
                var environMinions = GameEngine.getEnvironment().getMinions();

                Boolean minBool = false;

                List<Minion> minionsToAttack = new List<Minion>();

                foreach (Minion min in villainMinions)
                {
                    if (min.minionName.Equals(((Card)target[0]).getName()))
                    {
                        minionsToAttack.Add(min);
                        minBool = true;
                    }
                }

                foreach (Minion min in environMinions)
                {
                    if (min.minionName.Equals(((Card)target[0]).getName()))
                    {
                        minionsToAttack.Add(min);
                        minBool = true;
                    }
                }

                if (minBool)
                {
                    var targets = new List<Targetable>();
                    targets.AddRange(minionsToAttack);
                    DamageEffects.DealDamage(this, targets, 4, DamageEffects.DamageType.Melee);
                    card.SendToGraveyard(this, cardsOnField);
                }
                else MessageBox.Show("Please select an appropriate card.");
            }
            else
            {
                DamageEffects.DealDamage(this, new List<Targetable>() { GameEngine.getVillain() }, 4, DamageEffects.DamageType.Melee);
                card.SendToGraveyard(this, cardsOnField);
            }
            drawPhase(1);
        }

        public void TheLegacyRing(Card card)
        {
            //TODO fix how this works
            this.numPowers = 2;
            card.CardDestroyed += TheLegacyRing_Destroyed_Handler;
        }

        private void TheLegacyRing_Destroyed_Handler(Card m, EventArgs e)
        {
            this.numPowers = 1;
        }

        public void TakeDown(Card card)
        {
            //TODO implement this method
        }

        public override void DeathPower1()
        {
            //TODO implement this method
            throw new NotImplementedException();
        }

        public override void DeathPower2()
        {
            //TODO implement this method
            throw new NotImplementedException();
        }

        public override void DeathPower3()
        {
            //TODO implement this method
            throw new NotImplementedException();
        }   
    }
}
