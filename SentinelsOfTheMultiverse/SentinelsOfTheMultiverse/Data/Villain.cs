using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse.Data
{
    public class Villain : IPlayer
    {
        List<Minion> minions { get; set; }
        Deck deck { get; set; }


        public override void playerTurn()
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

        public override bool powerPhase()
        {
            throw new NotImplementedException();
        }

        public override void endPhase()
        {
            //conditionals for end turn effects
        }
    }
}
