using SentinelsOfTheMultiverse.Data.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse.Data.Heroes
{
    public class Legacy : Hero
    {
        public Legacy()
        {
            lifeTotal = 32;
        }

        public void Fortitude(Card card)
        {
            this.damageAmplificationToPlayer -= 1;
        }

        public void DangerSense(Card card)
        {
        }

        public void FlyingSmash(Card card)
        {
        }

        public void BackFistStrike(Card card){
        }

        public void BolsterAllies(Card card)
        {
        }

        public void MotivationalCharge(Card card)
        {
        }

        public void InspiringPresence(Card card)
        {
        }

        public void LeadFromTheFront(Card card)
        {
        }

        public void SuperhumanDurability(Card card)
        {
            this.damageAmplificationFromPlayer += 1;
        }

        public void NextEvolution(Card card)
        {
        }

        public void SurgeOfStrength(Card card)
        {
        }

        public void Thokk(Card card)
        {
            if (GameBoard.selectedCards.Count==1)
            {
                
            }
            drawPhase(1);
        }

        public void TheLegacyRing(Card card)
        {
            this.numPowers = 2;
        }

        public void TakeDown(Card card)
        {
        }
    }
}
