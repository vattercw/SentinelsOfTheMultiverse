using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse.Data.Effects
{
    public static class DamageEffects
    {
        public static List<DamageHandler> damageDealtHandlers = new List<DamageEffects.DamageHandler>();
        public delegate int DamageHandler(Targetable sender, Targetable receiver, ref int damageAmount, DamageType damageType);

        public enum DamageType { Projectile, Fire, Ice, Melee, Toxic, Lightning, All };


        public static bool inPlayBacklash { get; set; }

        public static void DealDamage(Targetable sender, List<Targetable> receivers, int damageAmount, DamageType damageType)
        {
            foreach (Targetable receiver in receivers)
            {
                int damageModifiers = 0;
                if (damageDealtHandlers.Count != 0)
                {
                    DamageHandler[] dummyDamageHandlers = new DamageHandler[damageDealtHandlers.Count];
                    damageDealtHandlers.CopyTo(dummyDamageHandlers);
                    foreach (DamageHandler dmgHand in dummyDamageHandlers)
                    {
                        damageModifiers += dmgHand(sender, receiver, ref damageAmount, damageType);
                        
                        if (damageAmount == 0){ //meaning the user is immune
                            damageModifiers = 0; //could add another pointer param for invincible in dmgHand 
                            break;
                        }
                    }
                }
                if (damageModifiers + damageAmount > receiver.maxHealth)
                    receiver.lifeTotal = receiver.maxHealth;
                else
                    receiver.lifeTotal -= damageModifiers + damageAmount;
            }
        }
    }
}
