﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse.Data.Minions.InsulaPrimus
{
    class VelociraptorPack : Minion
    {
        public VelociraptorPack()
        {
            maxHealth = 5;
            lifeTotal = 5;
            effectPhase = Minion.MinionType.End;
            //GameEngine.getVillain().addMinion(this, effectPhase);
        }

        public override void executeEffect()
        {
            throw new NotImplementedException();
        }
    }
}
