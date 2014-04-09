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
        public int lifePoints;
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
            drawPhase(1);
        }

        public override void drawPhase(int numCards)
        {
            List<Card> drawnCards = deck.draw(numCards);
            cardsOnField.AddRange(drawnCards);
        }


        internal int getDamageAmplification()
        {
            //TODO: add damage amplification
            return 0;
        }

        internal List<Minion> getMinions()
        {
            throw new NotImplementedException();
        }
    }
}
