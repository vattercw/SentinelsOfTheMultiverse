using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse.Data
{
    public class Deck
    {
        List<Card> cards = new List<Card>();

        public Deck(string hero)
        {
            var files = Directory.GetFiles(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Images\\Hero\\" + hero);

            foreach (var filename in files)
            {
                Card card = new Card(filename);
                cards.Add(card);
            }
        }


        public void shuffle()
        {
            Random rng = new Random();
            int n = this.cards.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card temp = cards[k];
                cards[k] = cards[n];
                cards[n] = temp;
            }
        }

        public List<Card> draw(int numberToDraw)
        {
            List<Card> drawnCards = new List<Card>();
            for (int jj = 0; jj < numberToDraw; jj++)
            {
                drawnCards.Add(cards[0]);
                cards.RemoveAt(0);
            }

            return drawnCards;
        }

        public void printDeck()
        {
            //Debug.WriteLine("deck: " + String.Join(", ", this.cards));
        }

        public List<Card> getCards()
        {
            return this.cards;
        }
    }
}
