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
    public abstract class Hero: IPlayer
    {

        public List<Card> hand { get; set; }
        public Deck deck { get; set; }

        public string characterName;
        //override int lifeTotal { get; set; }

        public Hero()
        {
<<<<<<< HEAD
            string heroName = this.GetType().Name;
            deck = new Deck(heroName);
            hand.AddRange(deck.draw(4));
=======
            characterName = this.GetType().Name;
            deck = new Deck(characterName);
>>>>>>> 86bdd026b80359d8cad5488d8308b8865f0c4448
        }

        internal string getCharacterName()
        {
            return characterName;
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
            hand.AddRange(deck.draw(numCards));
        }

        public override void endPhase()
        {
            //conditionals need to be added for end turn effects
        }

    }
}
