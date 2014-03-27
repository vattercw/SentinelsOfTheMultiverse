using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse.Data
{
    interface IPlayer
    {
        Deck deck { get; private set; }
        string characterName;
        int lifeTotal { get; set; }
        
        public IPlayer()
        {
        }


        void playTurn();
        void beginPhase();
        void mainPhase();
        void endPhase();

    }
}
