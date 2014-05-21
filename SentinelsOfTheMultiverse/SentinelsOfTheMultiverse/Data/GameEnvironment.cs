using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SentinelsOfTheMultiverse.Data.Environments;

namespace SentinelsOfTheMultiverse.Data
{
    public abstract class GameEnvironment : IPlayer
    {

        List<Minion> minions { get; set; }


        List<Minion> endTurnMinion = new List<Minion>();
        List<Minion> startTurnMinion = new List<Minion>();
        List<Minion> onAttackMinion = new List<Minion>();
        List<Minion> ongoingMinion = new List<Minion>();
              
        public GameEnvironment()
        {
            characterName = this.GetType().Name;
            deck = new Deck(characterName, IPlayer.PlayerType.Environment);
            deck.shuffle();
            graveyard = new List<Card>();
            cardsOnField = new List<Card>();

        }

        public override void playerTurn(bool playedCard=false, bool playedPower=false)
        {
            startPhase();
            //playPhase();
            //endPhase();
            //GameEngine.nextTurn();
        }

        public override void startPhase()
        {
          
            //conditionals for start turn effects
        }

        public override Boolean playPhase()
        {
            //drawPhase(1);
            return true;
        }

        public override void endPhase()
        {
            if (EndPhase != null) {
                EndPhase();
            }
        }
        internal event EndPhaseHandler EndPhase;
        internal delegate void EndPhaseHandler();

        public override List<Card> drawPhase(int numCards)
        {
            List<Card> drawnCards = deck.draw(numCards);
            cardsOnField.AddRange(drawnCards);
            return drawnCards;
            //for (int i = 0; i < drawnCards.Count; i++)
            //{
            //    object[] res = CardMethod(drawnCards[i]);

            ////    switch ((GameEngine.ForcedEffect)res[1])
            ////    {
            ////        case GameEngine.ForcedEffect.PrimordialPlant:
            ////            foreach (Hero hero in GameEngine.getHeroes())
            ////            {
            ////                //TODO get the forced discards to work with the GUI
            ////                //GameBoard.discardedCardsThisTurn = new List<Card>();
            ////                //DiscardFromBoard handView = new DiscardFromBoard();
            ////                //handView.Visibility = System.Windows.Visibility.Visible;
            ////                //handView.ShowDialog();

            ////                InsulaPrimus.DiscardedAction discardAction = (InsulaPrimus.DiscardedAction)res[0];
            ////                discardAction(GameBoard.discardedCardsThisTurn.Count, hero, (Card)res[2]);
            ////            }
            ////            break;



            ////        case GameEngine.ForcedEffect.RiverOfLava:
            ////            break;
            ////    }
            //}

        }
        internal List<Minion> getStartPhaseMinions()
        {
            return startTurnMinion;
        }

        internal List<Minion> getEndPhaseMinions()
        {
            return endTurnMinion;
        }

        internal List<Minion> getOnAttackMinions()
        {
            return onAttackMinion;
        }

        internal List<Minion> getOngoingEffectMinions()
        {
            return ongoingMinion;
        }

        public List<Minion> getMinions()
        {
            List<Minion> allMinions = new List<Minion>();
            allMinions.AddRange(getStartPhaseMinions());
            allMinions.AddRange(getEndPhaseMinions());
            allMinions.AddRange(getOnAttackMinions());
            allMinions.AddRange(getOngoingEffectMinions());
            return allMinions;
        }

        public void addMinion(Minion minion)
        {
            if (minion.effectPhase.Equals(Minion.MinionType.Start))
            {
                startTurnMinion.Add(minion);
                return;
            }

            if (minion.effectPhase.Equals(Minion.MinionType.End))
            {
                endTurnMinion.Add(minion);
                return;
            }

            if (minion.effectPhase.Equals(Minion.MinionType.OnAttack))
            {
                onAttackMinion.Add(minion);
                return;
            }

            if (minion.effectPhase.Equals(Minion.MinionType.Ongoing))
            {
                ongoingMinion.Add(minion);
                return;
            }
            else
            {
                return;
            }
        }


        internal void removeMinion(Minion minion)
        {
            if (minion.effectPhase.Equals(Minion.MinionType.Start))
            {
                startTurnMinion.Remove(minion);
                return;
            }

            if (minion.effectPhase.Equals(Minion.MinionType.End))
            {
                endTurnMinion.Remove(minion);
                return;
            }

            if (minion.effectPhase.Equals(Minion.MinionType.OnAttack))
            {
                onAttackMinion.Remove(minion);
                return;
            }

            if (minion.effectPhase.Equals(Minion.MinionType.Ongoing))
            {
                ongoingMinion.Remove(minion);
                return;
            }
            else
            {
                return;
            }
        }
    }
}
