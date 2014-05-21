using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse.Data
{
    public abstract class IPlayer: Targetable
    {

        #region Properties
        public Deck deck { get;  set; }
        public string characterName {get; set;}
        public int lifeTotal { get; set; }
        public int maxHealth { get; set; } 
        public List<Card> cardsOnField { get; set; }
        public List<String> immunities = new List<String>();
        public List<Card> graveyard { get; set; }
        public List<Card> discards { get; set; }

        abstract public void Power();

        abstract public void DeathPower1();

        abstract public void DeathPower2();

        abstract public void DeathPower3();

  


        public int numPowers
        {
            get
            {
                return _numPowers;
            }
            set
            {
                _numPowers = value;
            }

        }

        #endregion
        #region PrivateFields
            private int _numPowers = 1;
        #endregion

        

        public abstract void playerTurn(bool playedCard, bool playedPower);
        public abstract void startPhase();
        public abstract Boolean playPhase();
        public abstract void endPhase();
        public abstract List<Card> drawPhase(int numCards);


        public enum PlayerType { Hero, Villain, Minion, Environment };

        internal object[] CardMethod(Card card)
        {
            MethodInfo theMethod = GetType().GetMethod(card.getName());
            object[] result = (object[])theMethod.Invoke(this, new object[] { card });
            return result;
        }

        //public enum EventType{Attack, Discard, DrawCard};
        //private Dictionary<EventType, List<EventHandler>> _ongoingEventHandlers = new Dictionary<EventType, List<EventHandler>>();
        //internal Dictionary<EventType, List<EventHandler>> OngoingEventHandlers
        //{
        //    get { return _ongoingEventHandlers; }
        //    set
        //    {
        //        _ongoingEventHandlers = value;
        //    }
        //}


        private List<Ongoings> _ongoingEffects = new List<Ongoings>();
        internal List<Ongoings> ongoingEffects
        {
            get { return _ongoingEffects; }
            set
            {
                _ongoingEffects = value;
                updateOngoingEffects();
            }
        }

        public void updateOngoingEffects()
        {
            foreach (Ongoings ongoingEffect in ongoingEffects)
            {
                ongoingEffect();
            }
        }


        internal delegate void Ongoings();

    }
}
