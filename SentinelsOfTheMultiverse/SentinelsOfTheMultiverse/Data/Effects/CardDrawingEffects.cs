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
    }
}
