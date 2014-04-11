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
        Deck deck { get;  set; }
        public string characterName {get; set;}
        int lifeTotal { get; set; } //get rid of?
        public List<Card> cardsOnField { get; set; }
        public List<String> immunities = new List<String>();

        //public IPlayer()
        //{
        //    characterName = this.GetType().Name;
        //}

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

        internal void CardMethod(string methodName)
        {
            //MethodInfo theMethod = GetType().GetMethod(methodName);
            //theMethod.Invoke(this, null);
        }
    }
}
