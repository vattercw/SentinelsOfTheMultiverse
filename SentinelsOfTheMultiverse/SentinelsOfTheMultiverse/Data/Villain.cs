using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse.Data
{
    public abstract class Villain : IPlayer
    {
        private List<Minion> minions = new List<Minion>();
              

        public Villain()
        {
            characterName = this.GetType().Name;
            deck = new Deck(characterName, IPlayer.PlayerType.Villain);
            cardsOnField = new List<Card>();
            graveyard = new List<Card>();
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
            if (EndPhase != null) {
                EndPhase();
            }
            drawPhase(1);//plays card from deck
        }
        
        internal event EndPhaseHandler EndPhase;
        internal delegate void EndPhaseHandler();
    

        public override void drawPhase(int numCards)
        {
            List<Card> drawnCards = deck.draw(numCards);
            cardsOnField.AddRange(drawnCards);
            for (int i = 0; i < drawnCards.Count; i++)
            {
                this.CardMethod(drawnCards[i]);
            }
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
