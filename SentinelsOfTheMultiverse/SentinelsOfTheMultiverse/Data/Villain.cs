using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse.Data
{
    public abstract class Villain : IPlayer
    {
        List<Minion> minions { get; set; }

        List<Minion> endTurnMinion = new List<Minion>();
        List<Minion> startTurnMinion = new List<Minion>();
        List<Minion> onAttackMinion = new List<Minion>();
        List<Minion> ongoingMinion = new List<Minion>();

        

        public Villain()
        {
            characterName = this.GetType().Name;
            deck = new Deck(characterName, IPlayer.PlayerType.Villain);
            cardsOnField = new List<Card>();
            graveyard = new List<Card>();
        }

        public String getCharacterName()
        {
            return characterName;
        }

        public override void playerTurn(bool isCardPlayed=false, bool isPowerUsed=false)
        {
            startPhase();
            playPhase();
            endPhase();
            GameEngine.nextTurn();
        }

        public override void startPhase()
        {
            //conditionals for start turn effects
        }

        public override Boolean playPhase()
        {
            
            return true;
        }


        public override void endPhase()
        {
            //conditionals for end turn effects
            //plays card from deck.
            drawPhase(1);
        }

        public override void drawPhase(int numCards)
        {
            List<Card> drawnCards = deck.draw(numCards);
            cardsOnField.AddRange(drawnCards);
            for (int i = 0; i < drawnCards.Count; i++)
            {
                this.CardMethod(drawnCards[i]);
            }
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
