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

            //card.SendToGraveyard(this, cardsOnField);
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

       

        public object[] PrimordialPlantLife(Card card)
        {
            card.cardType = Card.CardType.OneShot;

            List<Hero> targets = getTargets(GameEngine.getHeroes());

            DiscardedAction act = PrimordialPlantLifeDiscardAction;

            return new object[] { act, GameEngine.ForcedEffect.PrimordialPlant, GameEngine.getHeroes() };

        }

        public delegate void DiscardedAction(int discardedCards, Hero target);

        void PrimordialPlantLifeDiscardAction(int discardedCards, Hero target)
        {
            if (discardedCards > 0)
            {
                DamageEffects.DealDamage(null, null, null, 2, DamageEffects.DamageType.Toxic);
            }
            else
            {
                DamageEffects.DealDamageToHero(target, 4, DamageEffects.DamageType.Toxic);
            }
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
