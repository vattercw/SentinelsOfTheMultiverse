using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse.Data
{
    abstract class GameEnvironment : IPlayer
    {
        public List<string> inPlay { get; set; }
        public Deck deck { get; set; }

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
            //deck.draw(1);
            return true;
        }

        public override void endPhase()
        {
            //conditionals for end turn effects
        }
    }
}
