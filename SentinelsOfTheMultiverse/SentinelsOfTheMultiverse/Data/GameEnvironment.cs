using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse.Data
{
    public abstract class GameEnvironment : IPlayer
    {
              
        public GameEnvironment()
        {
            characterName = this.GetType().Name;
            deck = new Deck(characterName, IPlayer.PlayerType.Environment);
            deck.shuffle();
            graveyard = new List<Card>();
            cardsOnField = new List<Card>();
        }

        public override void playerTurn(bool playedCard=false, bool playedPower=false)
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
            drawPhase(1);
        }

        public override void drawPhase(int numCards)
        {
            cardsOnField.AddRange(deck.draw(numCards));

        }
    }
}
