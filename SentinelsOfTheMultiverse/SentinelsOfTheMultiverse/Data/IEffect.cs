﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse.Data
{
    /*
     * The effect interface handles all "card actions" such as deal damage, prevent damage, discard cards, etc
     * */
    public abstract class IEffect
    {
        public enum DamageType { Projectile, Fire, Ice, Melee};
        public abstract void Execute();
    }
}
