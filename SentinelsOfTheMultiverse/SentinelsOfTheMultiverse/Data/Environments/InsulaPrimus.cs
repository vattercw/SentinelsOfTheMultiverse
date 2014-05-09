using SentinelsOfTheMultiverse.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SentinelsOfTheMultiverse.Data.Minions.InsulaPrimus;
using SentinelsOfTheMultiverse.Data.Effects;

namespace SentinelsOfTheMultiverse.Data.Environments
{
    public class InsulaPrimus: GameEnvironment
    {
        public void ObsidianField(Card card)
        {
            card.cardType = Card.CardType.Ongoing;
            GameEngine.obsidianInPlay = true;

            DamageEffects.GlobalDamageAmplification++;

            card.SendToGraveyard(this, cardsOnField);
            //TODO: player discard 2 cards to get rid of
        }


        public override void Power()
        {
            return;
        }


        public void RiverOfLava(Card card)
        {
            List<Hero> targets = getTargets(GameEngine.getHeroes());
            GameEngine.lavaInPlay = true;
        }

        public void VolcanicEruption(Card card)
        {
            List<Hero> targets = getTargets(GameEngine.getHeroes());
            GameEngine.volcanoInPlay = true;
        }

       

        public void PrimordialPlantLife(Card card)
        {
            card.cardType = Card.CardType.OneShot;

            List<Hero> targets = getTargets(GameEngine.getHeroes());

            //TODO: remove ongoing card to reduce damage of certain heros to 2
            DamageEffects.DealDamage(null, null, null, 2, DamageEffects.DamageType.Toxic);

            DamageEffects.DealDamage(targets, null, null, 4, DamageEffects.DamageType.Toxic);
        }

        ////Minions
        public void PterodactylThief(Card card)
        {
            card.cardType = Card.CardType.Environment;
            PterodactylThief ptero = new PterodactylThief();
            GameEngine.getEnvironment().addMinion(ptero);
            Console.WriteLine("Pterodactyl Thief: " + ptero.health);
        }

        public void VelociraptorPack(Card card)
        {
            VelociraptorPack velo = new VelociraptorPack();
            GameEngine.getEnvironment().addMinion(velo);
            Console.WriteLine("Velociraptor Pack: " + velo.health);
        }

        public void EnragedTRex(Card card)
        {
            EnragedTRex rex = new EnragedTRex();
            GameEngine.getEnvironment().addMinion(rex);
            Console.WriteLine("Enraged T-Rex: " + rex.health);
        }

        //used to determin heroes not immune to environment
        private List<Hero> getTargets(List<Hero> list)
        {
            List<Hero> targets = new List<Hero>();
            foreach (Hero hero in list)
            {
                if (hero.immuneToEnvironment == false)
                {
                    targets.Add(hero);
                }
            }
            return targets;
        }


        public override void DeathPower1()
        {
            return;
        }

        public override void DeathPower2()
        {
            return;
        }

        public override void DeathPower3()
        {
            return;
        }
    }
}
