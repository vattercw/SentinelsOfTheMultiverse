
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
        public Deck deck { get; set; }
        string characterName;
        public List<Card> cardsOnField { get; set; }

        //override int lifeTotal { get; set; }

        public Hero()
        {
            characterName = this.GetType().Name;
            deck = new Deck(characterName, IPlayer.PlayerType.Hero);
            deck.shuffle();
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
        }

        public override Boolean playPhase()
        {
            return true;
        }

        public Boolean powerPhase()
        {
            return true;
        }

        public override void drawPhase(int numCards)
        {
            hand.AddRange(deck.draw(numCards));
        }

        public override void endPhase()
        {
            //conditionals need to be added for end turn effects
        }
        public List<Card> getPlayerHand()
        {
            return hand;
        }
    }
}
