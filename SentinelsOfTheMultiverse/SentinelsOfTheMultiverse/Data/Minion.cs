using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse.Data
{
    class Minion
    {
        private int maxHealth;
        private int health;
        private String  minionName;


        public Minion(String name, int maxHealthNum)
        {
            maxHealth = maxHealthNum;
            health = maxHealthNum;
            minionName = name;
        }

        public String getMinionName()
        {
            return minionName;
        }

        public int getCurrentHealth()
        {
            return health;
        }

    }
}
