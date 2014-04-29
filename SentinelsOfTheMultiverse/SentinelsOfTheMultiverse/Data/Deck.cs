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
        public List<Card> cards { get; set; }

        public Deck(string characterName, IPlayer.PlayerType playerType)
        {
            var files = Directory.GetFiles(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Images\\"+playerType+"\\" + characterName);
            cards = new List<Card>();

            foreach (var fileName in files)
            {
                var cardTitle = Path.GetFileNameWithoutExtension(fileName);

                if (fileName.Contains(Utility.splitDelimeter))
                {
                    var number = Utility.getNumOfCards(cardTitle);
                    var title = Utility.removeNumOfCards(cardTitle);

                    Card card = new Card(fileName, title);

                    Console.WriteLine("\n" + title);
                    for (int k = 0; k < number; k++)
                    {
                        cards.Add(card);
                    }

                }
                else
                {
                    Card card = new Card(fileName, cardTitle);
                    cards.Add(card);
                }
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
    }
}
