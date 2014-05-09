﻿using SentinelsOfTheMultiverse.Data.Effects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SentinelsOfTheMultiverse.Data.Heroes
{
    public class Haka: Hero
    {
        public Haka()
        {
            lifeTotal = 34;
            maxHealth = 34;
        }


        public void Rampage(Card card)
        {
            card.cardType = Card.CardType.OneShot;
            Villain villain = GameEngine.getVillain();
            GameEnvironment environ = GameEngine.getEnvironment();
            DamageEffects.DealDamage(null, villain, villain.getMinions(), 5, DamageEffects.DamageType.Melee);
            DamageEffects.DealDamage(GameEngine.getHeroes(), null,null, 2, DamageEffects.DamageType.Melee);
            //hit thews dern environment minionz tew
            DamageEffects.DealDamage(null, null, environ.getMinions(), 5, DamageEffects.DamageType.Melee);
            card.SendToGraveyard(this, cardsOnField);
        }

        public void ElbowSmash(Card card)
        {
            card.cardType = Card.CardType.OneShot;

            var target = GameBoard.cardClickedArray;
            if (target.Count > 1)
            {
                MessageBox.Show("Select only one target to perform Elbow Smash. \n No select target default's to the Villain.");
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

                List<Minion> minionAttack = null;
                
                foreach (Minion min in villainMinions)
                {
                    if (min.minionName == target[0].Name)
                    {
                        minionAttack.Add(min);
                        minBool = true;
                    }
                }

                foreach (Minion min in environMinions)
                {
                    if (min.minionName == target[0].Name)
                    {
                        minionAttack.Add(min);
                        minBool = true;
                    }
                }

                if (minBool)
                {
                    DamageEffects.DealDamage(null, null, minionAttack, 3, DamageEffects.DamageType.Melee);
                    card.SendToGraveyard(this, cardsOnField);
                }
                else MessageBox.Show("Please select an appropriate card.");
            }
            else
            {
                DamageEffects.DealDamage(null, GameEngine.getVillain(), null, 3, DamageEffects.DamageType.Melee);
                card.SendToGraveyard(this, cardsOnField);
            }
        }

        public void Dominion(Card card)
        {
            
        }

        public void EnduringIntercession(Card card)
        {
            
        }

        public void GroundPound(Card card)
        {
            
        }

        public void HakaOfBattle(Card card)
        {
            card.cardType = Card.CardType.OneShot;
            CardDrawingEffects.DrawCards(2);
            //Don't forget to include something that doesn't allow them to go to the next turn until they discard.
            //also, reset the discard count
            if (GameBoard.discardedCardsThisTurn.Count < 1)
            {
                MessageBox.Show("You must discard are atleast one card to use this One-Shot.", "Discard Problem");
            }
            else
            {
                //damageAmplificationFromPlayer += GameBoard.discardedCardsThisTurn.Count;
                //card.SendToGraveyard(this, cardsOnField);
            }

            //add event handler for attack from haka
        }

        
        public void TaMoko(Card card)
        {
            card.cardType = Card.CardType.Ongoing;
            cardsOnField.Add(card);
            card.effects.Add(TaMokoEffect);
            TaMokoEffect();//may or may not need this here
            ongoingEffects.Add(TaMokoEffect);
        }

        void TaMokoEffect()
        {
            damageAmplificationToPlayer -= 1;
        }

        public void HakaOfRestoration(Card card)
        {
            card.cardType = Card.CardType.OneShot;
            CardDrawingEffects.DrawCards(2);

            Hero currentPlayer = (Hero)GameEngine.getCurrentPlayer();
            //Don't forget to include something that doesn't allow them to go to the next turn until they discard.

            if (GameBoard.discardedCardsThisTurn.Count == 0)
            {
                MessageBox.Show("You must discard are atleast one card to use this One-Shot.", "Discard Problem");
                currentPlayer.hand.Add(card);
                currentPlayer.graveyard.Remove(card);
                GameEngine.playerPlayedCard = false;
            }
            else
            {
                damageAmplificationFromPlayer += GameBoard.discardedCardsThisTurn.Count;
                card.SendToGraveyard(this, cardsOnField);
            }
        }

        override public void Power()
        {
            var target = GameBoard.cardClickedArray;
            if (target.Count > 1)
            {
                MessageBox.Show("Select only one target. \n No select target default's to the Villain.");
            }
            else if (target.Count == 1)
            {
                var villainMinions = GameEngine.getVillain().getMinions();
                var environMinions = GameEngine.getEnvironment().getMinions();

                Boolean minBool = false;

                List<Minion> minionAttack = null;

                foreach (Minion min in villainMinions)
                {
                    if (min.minionName == target[0].Name)
                    {
                        minionAttack.Add(min);
                        minBool = true;
                    }
                }

                foreach (Minion min in environMinions)
                {
                    if (min.minionName == target[0].Name)
                    {
                        minionAttack.Add(min);
                        minBool = true;
                    }
                }

                if (minBool)
                {
                    DamageEffects.DealDamage(null, null, minionAttack, 2, DamageEffects.DamageType.Melee);
                
                }
                else MessageBox.Show("Please select an appropriate card.");
            }
            else
            {
                DamageEffects.DealDamage(null, GameEngine.getVillain(), null, 2, DamageEffects.DamageType.Melee);
              
            }
        }

        public void HakaOfShielding(Card card)
        {
            int damageReduction = 3;
        }

        public void Mere(Card card)
        {
            //card.power = alskdjf;
        }

 
        public void PunishTheWeak(Card card)
        {
            
        }

        public void SavageMana(Card card)
        {
            
        }

        public void Taiaha(Card card)
        {
            
        }

        public void VitalitySurge(Card card)
        {
            HealEffects.healHero(this, 2);
            card.SendToGraveyard(this, cardsOnField);
            CardDrawingEffects.DrawCards(1);
        }
    }
}
