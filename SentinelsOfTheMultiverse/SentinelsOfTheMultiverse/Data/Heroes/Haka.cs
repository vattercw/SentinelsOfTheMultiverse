using SentinelsOfTheMultiverse.Data.Effects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            DamageEffects.DealDamage(null, villain, villain.getMinions(), 2, DamageEffects.DamageType.Melee);
            CardDrawingEffects.DestroyCard(card, this);
        }

        /*
        public void ElbowSmash(Card card)
        {
            throw new NotImplementedException();
        }

        public void Dominion(Card card)
        {
            throw new NotImplementedException();
        }

        public void EnduringIntercession(Card card)
        {
            throw new NotImplementedException();
        }

        public void GroundPound(Card card)
        {
            throw new NotImplementedException();
        }
        */

        public void HakaOfBattle(Card card)
        {
            card.cardType = Card.CardType.OneShot;
            CardDrawingEffects.DrawCards(2);
            if (GameBoard.discardedCardsThisTurn.Count < 1)
            {
                //TODO: tell user they must discard at least one card
                throw new NotImplementedException("user must discard at least one card");
            }
            damageAmplificationFromPlayer += GameBoard.discardedCardsThisTurn.Count;
            CardDrawingEffects.DestroyCard(card, this);
        }
        
        public void TaMoko(Card card)
        {
            card.cardType = Card.CardType.Ongoing;
           
            TaMokoEffect();
            ongoingEffects.Add(TaMokoEffect);
        }

        void TaMokoEffect()
        {
            damageAmplificationToPlayer -= 1;
        }
/*
        public void HakaOfRestoration(Card card)
        {
            throw new NotImplementedException();
        }

        public void HakaOfShielding(Card card)
        {
            throw new NotImplementedException();
        }

        public void Mere(Card card)
        {
            throw new NotImplementedException();
        }

        public void PunishTheWeak(Card card)
        {
            throw new NotImplementedException();
        }

        public void SavageMana(Card card)
        {
            throw new NotImplementedException();
        }

        public void Taiaha(Card card)
        {
            throw new NotImplementedException();
        }
 * 
        public void VitalitySurge(Card card)
        {
            throw new NotImplementedException();
        }
 * */
    }
}
