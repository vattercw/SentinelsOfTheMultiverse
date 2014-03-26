using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse
{
    public interface Hero
    {
        public List<string> hand { get; set; }
        public Deck deck{get;set;}

        public Hero(string heroName)
        {
            deck = new Deck();
            deck.shuffle();
            deck.printDeck();
            
            hand= deck.draw(7);
            deck.printDeck();
        }

    }

    public class Deck
    {
        List<string> cards;

        public Deck()
        {
            cards = new List<string>();
            for (int ii = 0; ii < 20; ii++)
            {
                cards.Add("card" + ii);
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
                string value = cards[k];
                cards[k] = cards[n];
                cards[n] = value;
            }
        }

        public List<string> draw(int numberToDraw)
        {
            List<string> drawnCards = new List<string>();
            for (int jj = 0; jj < numberToDraw; jj++)
            {
                drawnCards.Add(cards[0]);
                cards.RemoveAt(0);
            }

            return drawnCards;
        }

        public void printDeck()
        {
            Debug.WriteLine("deck: " + String.Join(", ", this.cards));
        }

        public List<string> getCards()
        {
            return this.cards;
        }
    }
}
