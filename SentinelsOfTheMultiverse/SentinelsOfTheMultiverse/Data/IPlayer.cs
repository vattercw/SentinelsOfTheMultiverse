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
        public List<Card> cardsOnField { get; set; }
        public List<String> immunities = new List<String>();
        public List<Card> graveyard { get; set; }

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

        #endregion
        #region PrivateFields
            private int _damageAmplificationFromPlayer;
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
            theMethod.Invoke(this, new object[]{card});
        }

        internal List<Ongoings> ongoingEffects { get; set; } 
        internal delegate void Ongoings();
    }
}
