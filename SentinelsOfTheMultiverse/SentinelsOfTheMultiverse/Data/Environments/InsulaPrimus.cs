using SentinelsOfTheMultiverse.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SentinelsOfTheMultiverse.Data.Minions.InsulaPrimus;
using SentinelsOfTheMultiverse.Data.Effects;
using System.Windows.Forms;

namespace SentinelsOfTheMultiverse.Data.Environments
{
    public class InsulaPrimus: GameEnvironment
    {
        public void ObsidianField(Card card)
        {
            card.cardType = Card.CardType.Ongoing;
            GameEngine.obsidianInPlay = true;
            foreach (Hero hero in GameEngine.getHeroes()) {

            }
            DamageEffects.damageDealtHandlers.Add(ObsidianFieldHandler);
        }

        public int ObsidianFieldHandler(Targetable sender, Targetable receiver, ref int damageAmount, DamageEffects.DamageType damageType)
        {
            return 1;
        }

        void ObsidianField_EndPhaseCompletedHandler() {
            DialogResult dialogResult = MessageBox.Show("Do you want to discard 2 cards to remove Obsidian Field?", "Dominion Effect", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes) {
                //TODO somehow prompt the user for 2 cards to discard
                
                Card card = cardsOnField.Find(x => x.getName().Equals("ObsidianField"));
                card.SendToGraveyard(this, cardsOnField);
            }
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
            DiscardedAction act = PrimordialPlantLife_Continuation;

            return new object[] { act, GameEngine.ForcedEffect.PrimordialPlant, GameEngine.getHeroes(), card };
        }

        public delegate void DiscardedAction(int discardedCards, Hero target, Card card);

        public void PrimordialPlantLife_Continuation(int discardedCards, Hero target, Card card)
        {
            if (discardedCards == 0)
            {
                DamageEffects.DealDamage(this,new List<Targetable>(){target}, 4, DamageEffects.DamageType.Toxic);
            }
            card.SendToGraveyard(this, cardsOnField);
        }

        ////Minions
        public void PterodactylThief(Card card)
        {
            card.cardType = Card.CardType.Environment;
            PterodactylThief ptero = new PterodactylThief();
            GameEngine.getEnvironment().addMinion(ptero);
            Console.WriteLine("Pterodactyl Thief: " + ptero.lifeTotal);
        }

        public void VelociraptorPack(Card card)
        {
            VelociraptorPack velo = new VelociraptorPack();
            GameEngine.getEnvironment().addMinion(velo);
            Console.WriteLine("Velociraptor Pack: " + velo.lifeTotal);
        }

        public void EnragedTRex(Card card)
        {
            EnragedTRex rex = new EnragedTRex();
            GameEngine.getEnvironment().addMinion(rex);
            Console.WriteLine("Enraged T-Rex: " + rex.lifeTotal);
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
