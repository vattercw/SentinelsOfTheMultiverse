using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SentinelsOfTheMultiverse.Data.Environments;

namespace SentinelsOfTheMultiverse.Data
{
    public abstract class GameEnvironment : IPlayer
    {

        List<Minion> minions { get; set; }
              
        public GameEnvironment()
        {
            characterName = this.GetType().Name;
            deck = new Deck(characterName, IPlayer.PlayerType.Environment);
            deck.shuffle();
            graveyard = new List<Card>();
            cardsOnField = new List<Card>();
            minions = new List<Minion>();
        }

        public override void playerTurn(bool playedCard=false, bool playedPower=false)
        {
            //startPhase();
            //playPhase();
            //endPhase();
            //GameEngine.nextTurn();
        }

        public override void startPhase()
        {
          
            //conditionals for start turn effects
        }

        public override Boolean playPhase()
        {
            //drawPhase(1);
            return true;
        }

        public override object[] endPhase()
        {
            if (EndPhase != null) {
                return EndPhase();
            }
            return null;
        }
        internal event EndPhaseHandler EndPhase;
        internal delegate object[] EndPhaseHandler();

        public override List<Card> drawPhase(int numCards)
        {
            List<Card> drawnCards = deck.draw(numCards);
            cardsOnField.AddRange(drawnCards);
            return drawnCards;
        }

        public List<Minion> getMinions()
        {
            return minions;
        }

        public void addMinion(Minion minion)
        {
            minions.Add(minion);
        }


        internal void removeMinion(Minion minion)
        {
            minions.Remove(minion);
        }
    }
}
