using SentinelsOfTheMultiverse.Data.Effects;
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


        public void Fortitude(Card card)
        {
            //this.damageAmplificationToPlayer -= 1;
        }

        public void DangerSense(Card card)
        {
            card.cardType = Card.CardType.Ongoing;
            this.immuneToEnvironment = true;
        }


        public void FlyingSmash(Card card)
        {
        }

        public void BackFistStrike(Card card){
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
                    DamageEffects.DealDamage(this, null, null, minionAttack, 4, DamageEffects.DamageType.Melee);
                    card.SendToGraveyard(this, cardsOnField);
                }
                else MessageBox.Show("Please select an appropriate card.");
            }
            else
            {
                DamageEffects.DealDamage(this, null, GameEngine.getVillain(), null, 4, DamageEffects.DamageType.Melee);
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
                    if (min.minionName == target[0].Name)
                    {
                        List<Minion> mins = new List<Minion>();
                        mins.Add(min);
                        DamageEffects.DealDamage(this, null, null, mins, 2, DamageEffects.DamageType.Melee);
                        break;
                    }
                }

                foreach (Minion min in environMinions)
                {
                    if (min.minionName == target[0].Name)
                    {
                        List<Minion> mins = new List<Minion>();
                        mins.Add(min);
                        DamageEffects.DealDamage(this, null, null, mins, 2, DamageEffects.DamageType.Melee);
                        break;
                    }
                }
            }
            else
            {
                DamageEffects.DealDamage(this, null, GameEngine.getVillain(), null, 2, DamageEffects.DamageType.Melee);
            }

            foreach (Hero hero in GameEngine.getHeroes())
            {
                HealEffects.healHero(hero, 1);
            }
        }

        public void InspiringPresence(Card card)
        {
        }

        public void LeadFromTheFront(Card card)
        {
        }

        public void SuperhumanDurability(Card card)
        {
            //this.damageAmplificationFromPlayer += 1;
            //TODO fix this card
        }

        public void NextEvolution(Card card)
        {
        }

        public void SurgeOfStrength(Card card)
        {
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
                    DamageEffects.DealDamage(this, null, null, minionAttack, 4, DamageEffects.DamageType.Melee);
                    card.SendToGraveyard(this, cardsOnField);
                }
                else MessageBox.Show("Please select an appropriate card.");
            }
            else
            {
                DamageEffects.DealDamage(this, null, GameEngine.getVillain(), null, 4, DamageEffects.DamageType.Melee);
                card.SendToGraveyard(this, cardsOnField);
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

        public override void DeathPower1()
        {
            throw new NotImplementedException();
        }

        public override void DeathPower2()
        {
            throw new NotImplementedException();
        }

        public override void DeathPower3()
        {
            throw new NotImplementedException();
        }

        
    }
}
