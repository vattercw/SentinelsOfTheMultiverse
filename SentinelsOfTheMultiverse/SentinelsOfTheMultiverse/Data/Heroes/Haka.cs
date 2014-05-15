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
            maxHealth = 34;
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
                    DamageEffects.DealDamage(this, null, null, minionAttack, 2, DamageEffects.DamageType.Melee);

                }
                else MessageBox.Show("Please select an appropriate card.");
            }
            else
            {
                DamageEffects.DealDamage(this, null, GameEngine.getVillain(), null, 2, DamageEffects.DamageType.Melee);

            }
        }


        public void Rampage(Card card)
        {
            card.cardType = Card.CardType.OneShot;
            Villain villain = GameEngine.getVillain();
            GameEnvironment environ = GameEngine.getEnvironment();
            DamageEffects.DealDamage(this, GameEngine.getHeroes(), null, null, 2, DamageEffects.DamageType.Melee);
            DamageEffects.DealDamage(this, null, villain, villain.getMinions(), 5, DamageEffects.DamageType.Melee);
            //hit thews dern environment minionz tew
            DamageEffects.DealDamage(this, null, null, environ.getMinions(), 5, DamageEffects.DamageType.Melee);
            card.SendToGraveyard(this, cardsOnField);
        }

        public void ElbowSmash(Card card)
        {
            card.cardType = Card.CardType.OneShot;

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
                    DamageEffects.DealDamage(this, null, null, minionAttack, 3, DamageEffects.DamageType.Melee);
                    card.SendToGraveyard(this, cardsOnField);
                }
                else MessageBox.Show("Please select an appropriate card.");
            }
            else
            {
                DamageEffects.DealDamage(this, null, GameEngine.getVillain(), null, 3, DamageEffects.DamageType.Melee);
                card.SendToGraveyard(this, cardsOnField);
            }
        }

        public void Dominion(Card card)
        {
            //add card destroyed event handler
            //foreach(IPlayer p in GameEngine.getPlayers()){
            List<Card> allEnvCards = new List<Card>();

            allEnvCards.AddRange(GameEngine.getEnvironment().deck.cards);
            allEnvCards.AddRange(GameEngine.getEnvironment().cardsOnField);
            foreach (Card c in allEnvCards)
            {
                if (c.cardType == Card.CardType.Environment)
                {
                    c.CardDestroyed += new Card.CardDestroyedHandler(DominionEffect);
                }
            }
            //}
            
        }
        void DominionEffect(Card c, EventArgs e)
        {
            CardDrawingEffects.DrawCards(15, this);
            c.CardDestroyed += null;
        }

        public void EnduringIntercession(Card card)
        {
            card.cardPower += new Card.Power(EnduringIntercessionPower);
        }
        void EnduringIntercessionPower(Card sender, object[] args)
        {
            sender.SendToGraveyard(this, cardsOnField);
        }

        public void GroundPound(Card card)
        {
            
        }

        public object[] HakaOfBattle(Card card)
        {
            card.cardType = Card.CardType.OneShot;
            CardDrawingEffects.DrawCards(2, this);
            

            DiscardedAction act = HakaOfBattleContinued;
            return new object[] { act,GameEngine.ForcedEffect.DiscardCurrentPlayer, GameEngine.getPlayers() };
        }

        private void HakaOfBattleContinued(int numDiscardedCards)
        {
            DamageEffects.damageDealtHandlers.Add(HakaOfBattleDamageHandler);
        }

        int HakaOfBattleDamageHandler(object sender, object receiver, int damageAmount, DamageEffects.DamageType damageType)
        {
            if (receiver.Equals(this))
            {
                DamageEffects.damageDealtHandlers.Remove(HakaOfBattleDamageHandler);
                return GameBoard.discardedCardsThisTurn.Count;    
            }
            return 0;
        }
        
        public void TaMoko(Card card)
        {
            
            //TODO: fix this
            card.cardType = Card.CardType.Ongoing;
            card.CardDestroyed += TaMoko_Destroyed_Handler;
            DamageEffects.damageDealtHandlers.Add(TaMoko_Damage_Handler);
        }
        void TaMoko_Destroyed_Handler(Card card, EventArgs e)
        {
            DamageEffects.damageDealtHandlers.Remove(TaMoko_Damage_Handler);
        }

        int TaMoko_Damage_Handler(object sender, object receiver, int damageAmount, DamageEffects.DamageType damageType)
        {
            if (receiver.Equals(this))
                return -1;
            return 0;
        }

        public void HakaOfRestoration(Card card)
        {
            card.cardType = Card.CardType.OneShot;
            CardDrawingEffects.DrawCards(2, this);

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
                //TODO fix
                //DamageEffects.damageDealtHandlers.Add();
                //damageAmplificationFromPlayer += GameBoard.discardedCardsThisTurn.Count;
                card.SendToGraveyard(this, cardsOnField);
            }
        }
        

        public void HakaOfShielding(Card card)
        {
            //todo
            //int damageReduction = 3;
        }

        public void Mere(Card card)
        {
            card.cardPower = new Card.Power(MerePower);
        }
        public void MerePower(Card card, object[] args)
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
                    DamageEffects.DealDamage(this, null, null, minionAttack, 3, DamageEffects.DamageType.Melee);
                }
                else MessageBox.Show("Please select an appropriate card.");
            }
            else
            {
                DamageEffects.DealDamage(this, null, GameEngine.getVillain(), null, 3, DamageEffects.DamageType.Melee);
            }
            card.SendToGraveyard(this, cardsOnField);
            CardDrawingEffects.DrawCards(1, this);
        }
 
        public void PunishTheWeak(Card card)
        {
            List<Minion> minions= GameEngine.getEnvironment().getMinions();
            minions.AddRange(GameEngine.getVillain().getMinions());
            Villain villain = GameEngine.getVillain();

            //TODO: get lowest nonhero
            //add event for damage on that minion/villain
            //if not lowestnonhero, event to decrease damage

            card.cardPower = new Card.Power(PunishTheWeakPower);
        }

        void PunishTheWeakPower(Card card, object[] args)
        {
            card.SendToGraveyard(this, cardsOnField);
        }

        public object[] SavageMana(Card card)
        {
            DiscardedAction act = ConsiderThePriceOfVictoryDiscardAction;
            return new object[] { act, GameEngine.ForcedEffect.ConsiderThePrice, GameEngine.getPlayers() };
        }

        public delegate void DiscardedAction(int discardedCards);

        void ConsiderThePriceOfVictoryDiscardAction(int discardedCards)
        {
            //finish doing the things for the discard method
            Console.WriteLine("number of discarded cards: " + discardedCards);
        }

        public void Taiaha(Card card)
        {
            
        }

        public void VitalitySurge(Card card)
        {
            HealEffects.healHero(this, 2);
            card.SendToGraveyard(this, cardsOnField);
            CardDrawingEffects.DrawCards(1, this);
        }

        public override void DeathPower1()
        {
            throw new NotImplementedException();
        }

        public override void DeathPower2()
        {
            foreach (Hero hero in GameEngine.getHeroes())
            {
                if (hero.lifeTotal > 0)
                {
                    //TODO fix this
                    //hero.damageAmplificationToPlayer -= 2;
                }
            }
        }

        public override void DeathPower3()
        {
            foreach (Hero hero in GameEngine.getHeroes())
            {
                if (hero.lifeTotal > 0)
                {
                    hero.drawPhase(1);
                }
            }
        }
    }
}
