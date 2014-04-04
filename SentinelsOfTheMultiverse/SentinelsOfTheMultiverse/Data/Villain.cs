using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse.Data
{
    public abstract class Villain : IPlayer
    {
        List<Minion> minions { get; set; }
        Deck deck { get; set; }
        string characterName;
        int _lifeTotal;
        public List<Card> cardsOnField { get; set; }

        public Villain()
        {
            characterName = this.GetType().Name;
            deck = new Deck(characterName, IPlayer.PlayerType.Villain);
            cardsOnField = new List<Card>();
        }

        public String getCharacterName()
        {
            return characterName;
        }

        public override void playerTurn(bool isCardPlayed=false, bool isPowerUsed=false)
        {
            startPhase();
            playPhase();
            endPhase();
            GameEngine.nextTurn();
        }

        public override void startPhase()
        {
            //conditionals for start turn effects
        }

        public override Boolean playPhase()
        {
            
            return true;
        }


        public override void endPhase()
        {
            //conditionals for end turn effects
            //plays card from deck.
        }

        internal int getLifeTotal()
        {
            return _lifeTotal;
        }
        public override void drawPhase(int numCards)
        {
            cardsOnField.AddRange(deck.draw(numCards));
        }
    }
}
