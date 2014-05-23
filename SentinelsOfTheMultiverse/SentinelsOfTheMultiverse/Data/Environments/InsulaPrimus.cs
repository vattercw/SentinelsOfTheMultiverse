﻿using SentinelsOfTheMultiverse.Data;
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
            foreach (Hero hero in GameEngine.getHeroes()) {
                //TODO add EndPhaseCompletedHandler somehow
                hero.EndPhase += ObsidianField_EndPhaseCompletedHandler;
            }
            DamageEffects.damageDealtHandlers.Add(ObsidianFieldHandler);
        }

        public int ObsidianFieldHandler(Targetable sender, Targetable receiver, ref int damageAmount, DamageEffects.DamageType damageType)
        {
            return 1;
        }

        object[] ObsidianField_EndPhaseCompletedHandler() {
            DialogResult dialogResult = MessageBox.Show(SentinelsOfTheMultiverse.Properties.Resources.ResourceManager.GetString("ObsidianFieldDiscard"), "Dominion Effect", MessageBoxButtons.YesNo);            
            return new object[]{GameEngine.ForcedEffect.ObsidianField, dialogResult};
        }

        public override void Power()
        {
            return;
        }


        public void RiverOfLava(Card card)
        {
            //TODO complete this method
            List<Hero> targets = GameEngine.getHeroes();
        }

        public void VolcanicEruption(Card card)
        {
            //TODO complete this method
            List<Hero> targets = GameEngine.getHeroes();
        }

       

        public object[] PrimordialPlantLife(Card card)
        {
            card.cardType = Card.CardType.OneShot;

            List<Hero> targets = GameEngine.getHeroes();
            DiscardedAction act = PrimordialPlantLife_Continuation;

            return new object[] { act, GameEngine.ForcedEffect.PrimordialPlant, GameEngine.getHeroes(), card };
        }

        public delegate void DiscardedAction(int discardedCards, Hero target, Card card);

        public void PrimordialPlantLife_Continuation(int discardedCards, Hero target, Card card)
        {
            if (discardedCards == 0)
            {
                DamageEffects.DealDamage(this, new List<Targetable>() { target }, 4, DamageEffects.DamageType.Toxic);
            }
            else
            {
                DamageEffects.DealDamage(this, new List<Targetable>() { target }, 2, DamageEffects.DamageType.Toxic);
            }
        }

        ////Minions

        public void VelociraptorPack(Card card)
        {
            VelociraptorPack velo = new VelociraptorPack();
            card.Minion = velo;
            card.CardDestroyed += VelociraptorPack_Destroyed;
            EndPhase += velo.executeEffect;
            velo.MinionDied += () => card.SendToGraveyard(this, cardsOnField);
        }

        void VelociraptorPack_Destroyed(Card c, EventArgs e)
        {
            removeMinion(c.Minion);
            EndPhase -= c.Minion.executeEffect;
        }

        public void EnragedTRex(Card card) {
            EnragedTRex trex = new EnragedTRex();
            card.Minion = trex;
            card.CardDestroyed += EnragedTRex_Destroyed;
            EndPhase += trex.executeEffect;
            trex.MinionDied += () => card.SendToGraveyard(this, cardsOnField);
            addMinion(trex);
        }
        void EnragedTRex_Destroyed(Card c, EventArgs e) {
            removeMinion(c.Minion);
            EndPhase -= c.Minion.executeEffect;
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
