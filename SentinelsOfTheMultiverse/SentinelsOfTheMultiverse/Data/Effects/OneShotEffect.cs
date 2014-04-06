using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse.Data.Effects
{
    public abstract class OneShotEffect: IEffect
    {
        string effectName;

        public OneShotEffect()
        {
            effectName = GetType().Name;
        }

        public abstract void execute();
    }
}
