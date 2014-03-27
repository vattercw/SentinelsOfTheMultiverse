using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse.Data.Heroes
{
    class Haka: Hero
    {
        public Haka()
        {
            initDeck();
        }

        public List<string> hand
        {
            get
            {
                return this.hand;
            }
            set
            {
                this.hand = value;
            }
        }

        public Deck deck
        {
            get
            {
                return this.deck;
            }
            set
            {
                this.deck = value;
            }
        }


        public void initDeck()
        {
            
        }
    }
}
