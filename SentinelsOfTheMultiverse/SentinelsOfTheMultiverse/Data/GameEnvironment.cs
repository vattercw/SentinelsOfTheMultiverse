using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse.Data
{
    public abstract class GameEnvironment : IPlayer
    {
        private List<string> _inPlay= new List<string>();
        private Deck _deck;
        private string envName;
        public List<Card> cardsOnField { get; set; }

        public GameEnvironment()
        {
            envName = this.GetType().Name;
            deck = new Deck(envName, IPlayer.PlayerType.Environment);
            deck.shuffle();
            cardsOnField = new List<Card>();
        }

        public string getCharacterName()
        {
            return envName;
        }

        public Deck deck 
        { 
            get{
                return _deck;
        }   set{
                _deck = value;
            } 
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
            throw new NotImplementedException();
        }

    }
}
