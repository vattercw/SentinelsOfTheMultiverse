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
        private String name;


        public Minion(String minionName, int maxHealthNum)
        {
            maxHealth = maxHealthNum;
            health = maxHealthNum;
            name = minionName;
        }


    }
}
