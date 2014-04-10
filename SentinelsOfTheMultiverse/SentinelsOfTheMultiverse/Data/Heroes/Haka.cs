using SentinelsOfTheMultiverse.Data.Effects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse.Data.Heroes
{
    public class Haka: Hero
    {

       

        public Haka()
        {
            lifePoints = 34;    
        }

        public void Rampage()
        {
            Villain villain = GameEngine.getVillain();

            DamageEffects.DealDamage(null, villain, villain.getMinions(), 2, DamageEffects.DamageType.Melee);
            //destroy the card here
        }

        public void ElbowSmash()
        {

        }


    }
}
