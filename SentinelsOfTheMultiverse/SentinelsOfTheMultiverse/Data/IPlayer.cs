using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse.Data
{
    public abstract class IPlayer
    {

        #region Properties
        public Deck deck { get;  set; }
        public string characterName {get; set;}
        public int lifeTotal { get; set; } //get rid of?
        public int maxHealth { get; set; } 
        public List<Card> cardsOnField { get; set; }
        public List<String> immunities = new List<String>();
        public List<Card> graveyard { get; set; }

        public bool immuneToEnvironment = false;

        abstract public void Power();

        public int damageAmplificationFromPlayer
        {
            get
            {
                return _damageAmplificationFromPlayer;
            }
            set
            {
                _damageAmplificationFromPlayer = value;
            }
        }
        public int damageAmplificationToPlayer { get; set; }

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
            private int _damageAmplificationFromPlayer;
            private int _numPowers = 1;
        #endregion

        

        public abstract void playerTurn(bool playedCard, bool playedPower);
        public abstract void startPhase();
        public abstract Boolean playPhase();
        public abstract void endPhase();
        public abstract void drawPhase(int numCards);

        public List<string> getImmunities()
        {
            return immunities;
        }

        public void addImmunity(String immunityName)
        {
            if (!immunities.Contains(immunityName))
            {
                immunities.Add(immunityName);
            }
        }

        public void removeImmunity(String immunityName)
        {
            if (immunities.Contains(immunityName))
            {
                immunities.Remove(immunityName);
            }
        }



        public enum PlayerType { Hero, Villain, Minion, Environment };

        internal void CardMethod(Card card)
        {
            MethodInfo theMethod = GetType().GetMethod(card.getName());
            theMethod.Invoke(this, new object[] { card });
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
            damageAmplificationFromPlayer = 0;
            damageAmplificationToPlayer = 0;
            foreach (Ongoings ongoingEffect in ongoingEffects)
            {
                ongoingEffect();
            }
        }


        internal delegate void Ongoings();
    }
}
