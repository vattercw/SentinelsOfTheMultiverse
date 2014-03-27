using SentinelsOfTheMultiverse.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse
{
    public abstract class Hero : IPlayer
    {

        public List<string> hand { get; set; }
        public Deck deck { get; set; }

        public Hero()
        {
            string heroName = this.GetType().Name;
            deck = new Deck(heroName);
        }


    

        public override void playerTurn()
        {
            startPhase();
            Boolean play = playPhase();
            Boolean power = powerPhase();
            if (play || power == true)
            {
                drawPhase(1);
            }
            else
            {
                drawPhase(2);
            }
            endPhase();
        }

        public override void startPhase()
        {
            //conditionals for start turn effects
        }

        public override Boolean playPhase()
        {
            return true;
        }

        public override Boolean powerPhase()
        {
            return true;
        }

        public void drawPhase(int numCards)
        {
            deck.draw(numCards);
        }

        public override void endPhase()
        {
            //conditionals need to be added for end turn effects
        }

    }
}
