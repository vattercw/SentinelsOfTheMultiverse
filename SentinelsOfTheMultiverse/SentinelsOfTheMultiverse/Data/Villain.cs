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
        Deck deck { get; set; }
        string characterName;
        public int lifePoints;
        public List<Card> cardsOnField { get; set; }

        List<Minion> endTurnMinion = new List<Minion>();
        List<Minion> startTurnMinion = new List<Minion>();
        List<Minion> onAttackMinion = new List<Minion>();
        List<Minion> ongoingMinion = new List<Minion>();

        public Villain()
        {
            characterName = this.GetType().Name;
            deck = new Deck(characterName, IPlayer.PlayerType.Villain);
            cardsOnField = new List<Card>();
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
                this.CardMethod(drawnCards[i].getName());
            }
        }


        internal int getDamageAmplification()
        {
            //TODO: add damage amplification
            return 0;
        }

        internal List<Minion> getStartPhaseMinions()
        {
            return startTurnMinion;//TODO fix later
        }

        internal List<Minion> getEndPhaseMinions()
        {
            return endTurnMinion;//TODO fix later
        }

        internal List<Minion> getOnAttackMinions()
        {
            return onAttackMinion;//TODO fix later
        }

        internal List<Minion> getOngoingEffectMinions()
        {
            return ongoingMinion;//TODO fix later
        }

        public void addMinion(Minion minion, String type)
        {
            if (type.Equals("start"))
            {
                startTurnMinion.Add(minion);
                return;
            }

            if (type.Equals("end"))
            {
                endTurnMinion.Add(minion);
                return;
            }

            if (type.Equals("attacked"))
            {
                onAttackMinion.Add(minion);
                return;
            }

            if (type.Equals("ongoing"))
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
            if (minion.effectPhase.Equals("start"))
            {
                startTurnMinion.Remove(minion);
                return;
            }

            if (minion.effectPhase.Equals("end"))
            {
                endTurnMinion.Remove(minion);
                return;
            }

            if (minion.effectPhase.Equals("attacked"))
            {
                onAttackMinion.Remove(minion);
                return;
            }

            if (minion.effectPhase.Equals("ongoing"))
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
