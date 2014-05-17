using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse.Data
{
    public abstract class Minion: Targetable
    {
        public int maxHealth { get; set; }
        private int _lifeTotal;
        public int lifeTotal {
            get {
                return _lifeTotal;
            }
            set {
                _lifeTotal = value;
                if (_lifeTotal <= 0) {
                    MinionDied();
                }
            }
        }
        public String  minionName;
        public Enum effectPhase;

        public enum MinionType { Ongoing, OnAttack, Start, End };

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
            return lifeTotal;
        }
        public abstract void executeEffect();

        public event MinionDeadHanlder MinionDied;
        public delegate void MinionDeadHanlder();
    }
}
