using SentinelsOfTheMultiverse.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SentinelsOfTheMultiverse.Data.Minions.InsulaPrimus;

namespace SentinelsOfTheMultiverse.Data.Environments
{
    public class InsulaPrimus: GameEnvironment
    {
        public static void ObsidianField(Card card)
        {

        }

        public static void RiverOfLava(Card card)
        {

        }

        public static void VolcanicEruption(Card card)
        {

        }

        public static void PrimordialPlantLife(Card card)
        {

        }

        ////Minions
        public static void PterodactylThief(Card card)
        {
            PterodactylThief ptero = new PterodactylThief();
            GameEngine.getEnvironment().addMinion(ptero);
            Console.WriteLine("Pterodactyl Thief: " + ptero.health);
        }

        public static void VelociraptorPack(Card card)
        {
            VelociraptorPack velo = new VelociraptorPack();
            GameEngine.getEnvironment().addMinion(velo);
            Console.WriteLine("Velociraptor Pack: " + velo.health);
        }

        public static void EnragedTRex(Card card)
        {
            EnragedTRex rex = new EnragedTRex();
            GameEngine.getEnvironment().addMinion(rex);
            Console.WriteLine("Enraged T-Rex: " + rex.health);
        }

    }
}
