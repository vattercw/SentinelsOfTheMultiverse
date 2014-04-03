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

        public GameEnvironment()
        {
            envName = this.GetType().Name;
            deck = new Deck(envName, IPlayer.PlayerType.Environment);
            deck.shuffle();
        }

        public List<string> inPlay 
        { 
            get{
                return _inPlay;
        }   set{
                _inPlay = value;
            } 
        }

        public Deck deck 
        { 
            get{
                return _deck;
        }   set{
                _deck = value;
            } 
        }

        public override void playerTurn()
        {
            startPhase();
            playPhase();
            endPhase();
        }

        public override void startPhase()
        {
            if (inPlay.Count == 0)
            {
                return;
            }
            //conditionals for start turn effects
        }

        public override Boolean playPhase()
        {
            
            return true;
        }

        public override void endPhase()
        {
            //conditionals for end turn effects
            deck.draw(1);
        }
    }
}
