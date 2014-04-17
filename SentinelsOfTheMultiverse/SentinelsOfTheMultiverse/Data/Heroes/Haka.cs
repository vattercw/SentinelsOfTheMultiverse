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
            DamageEffects.DealDamage(null, villain, villain.getMinions(), 5, DamageEffects.DamageType.Melee);
            DamageEffects.DealDamage(GameEngine.getHeroes(), null,null, 2, DamageEffects.DamageType.Melee);
            card.SendToGraveyard(this, cardsOnField);
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
            //check that only happens once
            damageAmplificationFromPlayer += GameBoard.discardedCardsThisTurn.Count;
            card.SendToGraveyard(this, cardsOnField);
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
