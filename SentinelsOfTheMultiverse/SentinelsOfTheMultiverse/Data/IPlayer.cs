using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse.Data
{
    abstract class IPlayer
    {
        Deck deck { get;  set; }
        string characterName;
        int lifeTotal { get; set; }
        
        public IPlayer()
        {
        }


        public abstract void playTurn();
        public abstract void beginPhase();
        public abstract void mainPhase();
        public abstract void endPhase();
    }
}
