using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse.Data.Heroes
{
    public class Haka: Hero
    {
        public Haka()
        {
            
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
    }
}
