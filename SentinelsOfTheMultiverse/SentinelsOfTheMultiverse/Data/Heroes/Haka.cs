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
            lifeTotal = 34;    
        }

        public void Rampage(Card card)
        {
            Villain villain = GameEngine.getVillain();

            DamageEffects.DealDamage(null, villain, villain.getMinions(), 2, DamageEffects.DamageType.Melee);
            CardDrawingEffects.DestroyCard(card, this);
        }

        public void ElbowSmash(Card card)
        {

        }


    }
}
