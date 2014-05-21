
﻿using SentinelsOfTheMultiverse.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse
{
    public abstract class Hero : IPlayer
    {

        public List<Card> hand { get; set; }

        public Hero()
        {
            //ongoingEffects = new List<Ongoings>();
            characterName = this.GetType().Name;
            deck = new Deck(characterName, IPlayer.PlayerType.Hero);
            deck.shuffle();
            graveyard = new List<Card>();
            hand = new List<Card>();
            cardsOnField = new List<Card>();
            drawPhase(4);
        }

        public string getCharacterName()
        {
            return characterName;
        }

        public override void playerTurn(bool isCardPlayed, bool isPowerUsed)
        {
            startPhase();

            for (int i = 1; i < numPowers; i++)
            {
                //powerPhase();
            }

            if (isCardPlayed || isPowerUsed)
            {
                drawPhase(1);
            }
            else
            {
                drawPhase(2);
            }
            endPhase();
        }
        
        
        public override void startPhase()
        {
            //conditionals for start turn effects
            if (StartPhaseStarted != null) {
                StartPhaseStarted();
            }
        }
        public event StartPhaseHandler StartPhaseStarted;
        public delegate void StartPhaseHandler();

        public override Boolean playPhase()
        {

            //do playphase things
          
            return true;
        }

        public Boolean powerPhase()
        {
            
            return true;
        }

        public override List<Card> drawPhase(int numCards)
        {
            hand.AddRange(deck.draw(numCards));
            return null;//This return only matters for env and villain
        }

        public override void endPhase()
        {
            //conditionals need to be added for end turn effects
            if (EndPhase != null)
                EndPhase();
        }
        public event EndPhaseHandler EndPhase;
        public delegate void EndPhaseHandler();

        public List<Card> getPlayerHand()
        {
            return hand;
        }

        public void DiscardCard(Card card)
        {
            GameBoard.discardedCardsThisTurn.Add(card);
            hand.Remove(card);
            graveyard.Add(card);
        }

    }
}
