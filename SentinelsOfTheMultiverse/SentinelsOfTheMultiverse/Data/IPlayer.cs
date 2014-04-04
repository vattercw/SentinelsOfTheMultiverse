using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse.Data
{
    public abstract class IPlayer
    {
        Deck deck { get;  set; }
        public string characterName {get; set;}
        int lifeTotal { get; set; }
        public List<Card> cardsOnField { get; set; }

        public abstract void playerTurn(bool playedCard, bool playedPower);
        public abstract void startPhase();
        public abstract Boolean playPhase();
        public abstract void endPhase();
        public abstract void drawPhase(int numCards);

        public enum PlayerType { Hero, Villain, Minion, Environment };
    }
}
