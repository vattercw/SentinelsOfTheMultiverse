using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse.Data.Effects
{
    public static class CardDrawingEffects
    {
        public static void DrawCard(int numCards)
        {
            GameEngine.getCurrentPlayer().drawPhase(numCards);
        }

        public static void DiscardCardFromHand(List<Card> cardsToDiscard)
        {
            foreach (Card card in cardsToDiscard)
            {
                ((Hero)GameEngine.getCurrentPlayer()).DiscardCard(card);
            }
        }

        public static void DestroyCard(Card card, IPlayer player)
        {
            player.cardsOnField.Remove(card);
            player.graveyard.Add(card);
        }
    }
}
