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

        public Villain()
        {
            characterName = this.GetType().Name;
            deck = new Deck(characterName, IPlayer.PlayerType.Villain);
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
        }

        public override void startPhase()
        {
            //conditionals for start turn effects
        }

        public override Boolean playPhase()
        {
            //deck.draw(1);
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
    }
}
