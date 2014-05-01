using SentinelsOfTheMultiverse.Data.Effects;
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
                MessageBox.Show("Select only one target to perform Elbow Smash.");
                Hero currentPlayer = (Hero)GameEngine.getCurrentPlayer();
                currentPlayer.hand.Add(card);
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
            if (GameBoard.discardedCardsThisTurn.Count < 1)
            {
                MessageBox.Show("You must discard are atleast one card to use this One-Shot.", "Discard Problem");
            }
            else
            {
                damageAmplificationFromPlayer += GameBoard.discardedCardsThisTurn.Count;
                card.SendToGraveyard(this, cardsOnField);
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
            
        }

        public void HakaOfShielding(Card card)
        {
            
        }

        public void Mere(Card card)
        {
            
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
            
        }
    }
}
