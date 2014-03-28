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
<<<<<<< HEAD
    public abstract class Hero: IPlayer
=======
    public abstract class Hero : IPlayer
>>>>>>> 6f53958272daa2c939f4374ebabf318cce6471b1
    {

        public List<string> hand { get; set; }
        public Deck deck { get; set; }

        override string characterName;
        override int lifeTotal { get; set; }

        public Hero()
        {
            characterName = this.GetType().Name;
            deck = new Deck(characterName);
        }

<<<<<<< HEAD

        internal string getCharacterName()
        {
            throw new NotImplementedException();
        }
    }
=======
>>>>>>> 6f53958272daa2c939f4374ebabf318cce6471b1

    

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
