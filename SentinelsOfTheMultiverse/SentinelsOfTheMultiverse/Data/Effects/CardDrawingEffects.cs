using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse.Data.Effects
{
    public static class CardDrawingEffects
    {
        public static void DrawCards(int numCards, IPlayer player)
        {
            player.drawPhase(numCards);
        }

        public static void DiscardCardFromHand(List<Card> cardsToDiscard, Hero hero)
        {
            foreach (Card card in cardsToDiscard)
            {
                hero.DiscardCard(card);
            }
        }

        

    }
}
