using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse.Data
{
    public abstract class Minion
    {
        public int maxHealth;
        public int health;
        public String  minionName;
        public String effectPhase;


        public Minion()
        {
            minionName = this.GetType().Name;

        }

        public String getMinionName()
        {
            return minionName;
        }

        public int getCurrentHealth()
        {
            return health;
        }


        internal int getDamageAmplification()
        {
            return 0; //TODO: implement
        }

        public abstract static void executeEffect();
    }
}
