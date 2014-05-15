using SentinelsOfTheMultiverse.Data.Effects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SentinelsOfTheMultiverse.Data.Heroes
{
    public class Haka: Hero
    {
        public delegate void DiscardedAction(int discardedCards, Card card);
        public Haka()
        {
            lifeTotal = 34;
            maxHealth = 34;
        }

        override public void Power()
        {
            var target = GameBoard.cardClickedArray;
            if (target.Count > 1)
            {
                MessageBox.Show("Select only one target. \n No select target default's to the Villain.");
            }
            else if (target.Count == 1)
            {
                var villainMinions = GameEngine.getVillain().getMinions();
                var environMinions = GameEngine.getEnvironment().getMinions();

                Boolean minBool = false;

                List<Minion> minionsToAttack = null;

                foreach (Minion min in villainMinions)
                {
                    if (min.minionName == target[0].Name)
                    {
                        minionsToAttack.Add(min);
                        minBool = true;
                    }
                }

                foreach (Minion min in environMinions)
                {
                    if (min.minionName == target[0].Name)
                    {
                        minionsToAttack.Add(min);
                        minBool = true;
                    }
                }

                if (minBool)
                {
                    var targets = new List<Targetable>();
                    targets.AddRange(minionsToAttack);
                    DamageEffects.DealDamage(this,targets , 2, DamageEffects.DamageType.Melee);
                }
                else MessageBox.Show("Please select an appropriate card.");
            }
            else
            {
                DamageEffects.DealDamage(this, new List<Targetable>(){ GameEngine.getVillain()}, 2, DamageEffects.DamageType.Melee);

            }
        }


        public void Rampage(Card card)
        {
            card.cardType = Card.CardType.OneShot;
            Villain villain = GameEngine.getVillain();
            GameEnvironment environ = GameEngine.getEnvironment();
            var heroes = new List<Targetable>();
            heroes.AddRange(GameEngine.getHeroes());

            DamageEffects.DealDamage(this, heroes, 2, DamageEffects.DamageType.Melee);
            DamageEffects.DealDamage(this, GameEngine.getNonHeroTargets(), 5, DamageEffects.DamageType.Melee);
            card.SendToGraveyard(this, cardsOnField);
        }

        public void ElbowSmash(Card card)
        {
            card.cardType = Card.CardType.OneShot;

            var target = GameBoard.cardClickedArray;
            
            if (target.Count > 1)
            {
                MessageBox.Show("Select only one target to deal damage to. \n No select target default's to the Villain.");
                Hero currentPlayer = (Hero)GameEngine.getCurrentPlayer();
                cardsOnField.Remove(card);
                currentPlayer.hand.Add(card);
                GameEngine.playerPlayedCard = false;
            }
            else if (target.Count == 1)
            {
                var villainMinions = GameEngine.getVillain().getMinions();
                var environMinions = GameEngine.getEnvironment().getMinions();

                Boolean minBool = false;

                List<Minion> minionsToAttack = null;
                
                foreach (Minion min in villainMinions)
                {
                    if (min.minionName == target[0].Name)
                    {
                        minionsToAttack.Add(min);
                        minBool = true;
                    }
                }

                foreach (Minion min in environMinions)
                {
                    if (min.minionName == target[0].Name)
                    {
                        minionsToAttack.Add(min);
                        minBool = true;
                    }
                }

                if (minBool)
                {
                    var targets= new List<Targetable>();
                    targets.AddRange(minionsToAttack);
                    DamageEffects.DealDamage(this, targets, 3, DamageEffects.DamageType.Melee);
                    card.SendToGraveyard(this, cardsOnField);
                }
                else MessageBox.Show("Please select an appropriate card.");
            }
            else
            {
                DamageEffects.DealDamage(this, new List<Targetable>() { GameEngine.getVillain() }, 3, DamageEffects.DamageType.Melee);
                card.SendToGraveyard(this, cardsOnField);
            }
        }

        public void Dominion(Card dominionCard)
        {
            List<Card> allEnvCards = getAllEnvironmentCards();
            
            foreach (Card c in allEnvCards) {
                if (c.cardType == Card.CardType.Environment) {
                    c.CardDestroyed += new Card.CardDestroyedHandler(DominionEffect);
                }
            }
            dominionCard.CardDestroyed += Dominion_CardDestroyed;
        }

        private List<Card> getAllEnvironmentCards() {
            List<Card> allEnvCards = new List<Card>();
            allEnvCards.AddRange(GameEngine.getEnvironment().deck.cards);
            allEnvCards.AddRange(GameEngine.getEnvironment().cardsOnField);
            return allEnvCards;
        }

        private void Dominion_CardDestroyed(Card m, EventArgs e) {
            List<Card> envCards= getAllEnvironmentCards();
            foreach (Card card in envCards) {
                card.CardDestroyed += null;
            }
        }

        void DominionEffect(Card c, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to draw a card from Dominions Effect?", "Dominion Effect", MessageBoxButtons.YesNo);
            if(dialogResult == DialogResult.Yes)
                CardDrawingEffects.DrawCards(1, this);
        }

        public void EnduringIntercession(Card card)
        {
            card.cardPower += new Card.Power(EnduringIntercessionPower);
        }
        void EnduringIntercessionPower(Card sender, object[] args)
        {
            sender.SendToGraveyard(this, cardsOnField);
        }

        public void GroundPound(Card card)
        {
            
        }

        public object[] HakaOfBattle(Card card)
        {
            card.cardType = Card.CardType.OneShot;
            CardDrawingEffects.DrawCards(2, this);
            

            DiscardedAction act = HakaOfBattleContinuation;
            return new object[] { act,GameEngine.ForcedEffect.DiscardCurrentPlayer, GameEngine.getPlayers() };
        }

        private void HakaOfBattleContinuation(int numDiscardedCards, Card card)
        {
            DamageEffects.damageDealtHandlers.Add(HakaOfBattleDamageHandler);
        }

        int HakaOfBattleDamageHandler(object sender, Targetable receiver, int damageAmount, DamageEffects.DamageType damageType)
        {
            if (sender.Equals(this))
            {
                DamageEffects.damageDealtHandlers.Remove(HakaOfBattleDamageHandler);
                return GameBoard.discardedCardsThisTurn.Count;    
            }
            return 0;
        }
        
        public void TaMoko(Card card)
        {
            card.cardType = Card.CardType.Ongoing;
            card.CardDestroyed += TaMoko_Destroyed_Handler;
            DamageEffects.damageDealtHandlers.Add(TaMoko_Damage_Handler);
        }
        void TaMoko_Destroyed_Handler(Card card, EventArgs e)
        {
            DamageEffects.damageDealtHandlers.Remove(TaMoko_Damage_Handler);
        }

        int TaMoko_Damage_Handler(object sender, object receiver, int damageAmount, DamageEffects.DamageType damageType)
        {
            if (receiver.Equals(this))
                return -1;
            return 0;
        }

        public object[] HakaOfRestoration(Card card)
        {
            card.cardType = Card.CardType.OneShot;
            CardDrawingEffects.DrawCards(2, this);

            DiscardedAction act = HakaOfRestorationContinuation;
            return new object[] { act, GameEngine.ForcedEffect.DiscardCurrentPlayer, 1, card};
        }

        void HakaOfRestorationContinuation(int numDiscardedCards, Card card) {
            lifeTotal += numDiscardedCards;
            card.SendToGraveyard(this, cardsOnField);
        }
        

        public object[] HakaOfShielding(Card card)
        {
            card.cardType = Card.CardType.OneShot;
            CardDrawingEffects.DrawCards(2, this);

            DiscardedAction act = HakaOfShielding_Continuation;
            return new object[] { act, GameEngine.ForcedEffect.DiscardCurrentPlayer, 1, card };
        }

        private void HakaOfShielding_Continuation(int numDiscardedCards, Card card) {
            DamageEffects.damageDealtHandlers.Add(HakaOfShielding_DamageHandler);
        }

        int HakaOfShielding_DamageHandler(object sender, object receiver, int damageAmount, DamageEffects.DamageType damageType) {
            if (receiver.Equals(this)) {
                DamageEffects.damageDealtHandlers.Remove(HakaOfShielding_DamageHandler);
                return  GameBoard.discardedCardsThisTurn.Count * (-2);
            }
            return 0;
        }

        public void Mere(Card card)
        {
            card.cardPower = new Card.Power(MerePower);
        }

        public void MerePower(Card card, object[] args)
        {
            var target = GameBoard.cardClickedArray;
            if (target.Count > 1)
            {
                MessageBox.Show("Select only one target to deal damage to. \n No select target default's to the Villain.");
                Hero currentPlayer = (Hero)GameEngine.getCurrentPlayer();
                cardsOnField.Remove(card);
                currentPlayer.hand.Add(card);
                GameEngine.playerPlayedCard = false;
            }
            else if (target.Count == 1)
            {
                var villainMinions = GameEngine.getVillain().getMinions();
                var environMinions = GameEngine.getEnvironment().getMinions();

                Boolean minBool = false;

                List<Minion> minionAttack = null;

                foreach (Minion min in villainMinions)
                {
                    if (min.minionName == target[0].Name)
                    {
                        minionAttack.Add(min);
                        minBool = true;
                    }
                }

                foreach (Minion min in environMinions)
                {
                    if (min.minionName == target[0].Name)
                    {
                        minionAttack.Add(min);
                        minBool = true;
                    }
                }

                if (minBool)
                {
                    var targets = new List<Targetable>();
                    targets.AddRange(minionAttack);
                    DamageEffects.DealDamage(this, targets, 3, DamageEffects.DamageType.Melee);
                }
                else MessageBox.Show("Please select an appropriate card.");
            }
            else
            {
                DamageEffects.DealDamage(this, new List<Targetable>() { GameEngine.getVillain() }, 3, DamageEffects.DamageType.Melee);
            }
            card.SendToGraveyard(this, cardsOnField);
            CardDrawingEffects.DrawCards(1, this);
        }
 
        public void PunishTheWeak(Card card)
        {
            DamageEffects.damageDealtHandlers.Add(PunishTheWeak_Damage_Handler);
            card.cardPower = new Card.Power(PunishTheWeakPower);
        }

        private int PunishTheWeak_Damage_Handler(Targetable sender, Targetable receiver, int damageAmount, DamageEffects.DamageType damageType) {
            List<Targetable> sortedNonHeroes = getSortedNonHeroes();

            if (receiver.Equals(sortedNonHeroes[0]))
                return 1;
            else
                return -1;
        }

        private static List<Targetable> getSortedNonHeroes() {
            List<Targetable> nonHeroes = new List<Targetable>();
            nonHeroes.AddRange(GameEngine.getEnvironment().getMinions());
            nonHeroes.AddRange(GameEngine.getVillain().getMinions());
            nonHeroes.Add(GameEngine.getVillain());

            List<Targetable> sortedNonHeroes = nonHeroes.OrderBy(o => o.lifeTotal).ToList();
            return sortedNonHeroes;
        }

        void PunishTheWeakPower(Card card, object[] args)
        {
            DamageEffects.damageDealtHandlers.Remove(PunishTheWeak_Damage_Handler);
            card.SendToGraveyard(this, cardsOnField);
        }


        public object[] SavageMana(Card card)
        {
            DiscardedAction act = ConsiderThePriceOfVictoryDiscardAction;
            return new object[] { act, GameEngine.ForcedEffect.ConsiderThePrice};
        }

        

        void ConsiderThePriceOfVictoryDiscardAction(int discardedCards, Card card)
        {
            //finish doing the things for the discard method
            Console.WriteLine("number of discarded cards: " + discardedCards);
        }

        public void Taiaha(Card card)
        {
            card.cardPower = new Card.Power(TaiahaPower);   
        }
        
        void TaiahaPower(Card sender, object[] args) {
            //TODO: open window for player to choose targets
            //if (targets.Count <= 2) {
            //    foreach (Targetable target in targets) {
            //        DamageEffects.DealDamage(this, target, 3, DamageEffects.DamageType.Melee);
            //    }
            //}
        }

        public void VitalitySurge(Card card)
        {
            HealEffects.healHero(this, 2);
            card.SendToGraveyard(this, cardsOnField);
            CardDrawingEffects.DrawCards(1, this);
        }

        public override void DeathPower1()
        {
            throw new NotImplementedException();
        }

        public override void DeathPower2()
        {
            foreach (Hero hero in GameEngine.getHeroes())
            {
                if (hero.lifeTotal > 0)
                {
                    //TODO fix this
                    //hero.damageAmplificationToPlayer -= 2;
                }
            }
        }

        public override void DeathPower3()
        {
            foreach (Hero hero in GameEngine.getHeroes())
            {
                if (hero.lifeTotal > 0)
                {
                    hero.drawPhase(1);
                }
            }
        }
    }
}
