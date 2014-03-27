using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse.Data
{
    public abstract class IPlayer
    {
        //Deck deck { get; private set; }
        //string characterName;
        //int lifeTotal { get; set; }

        Deck deck { get;  set; }
        string characterName;
        int lifeTotal { get; set; }

        
        //public IPlayer()
        //{

        //}


        public abstract void playerTurn();
        public abstract void startPhase();
        public abstract Boolean playPhase();
        public abstract Boolean powerPhase();
        public abstract void endPhase();
    }
}
