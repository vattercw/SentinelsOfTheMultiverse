using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse.Data
{
    public abstract class IPlayer
    {
        Deck deck { get;  set; }
        string characterName;
        int lifeTotal { get; set; }

<<<<<<< HEAD
        public abstract void playTurn();
        public abstract void beginPhase();
        public abstract void mainPhase();
=======
        
        //public IPlayer()
        //{

        //}


        public abstract void playerTurn();
        public abstract void startPhase();
        public abstract Boolean playPhase();
        public abstract Boolean powerPhase();
>>>>>>> 6f53958272daa2c939f4374ebabf318cce6471b1
        public abstract void endPhase();
    }
}
