using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SentinelsOfTheMultiverse.Data
{
    public class Card
    {
        public enum CardLocation { Hand, Deck, Graveyard };
        public enum CardType {OneShot, Ongoing, Equipment};
        string cardName;
        internal List<IPlayer.Ongoings> effects { get; set; }
        public Image cardImage = new Image();
        public string cardImageStr = "";
        public CardType cardType { get; set; }

        public Card(string cardPath, string name)
        {
            cardImage.Source = getImageSource(cardPath);
            cardName = name;
            cardImageStr = cardPath;
            effects = new List<IPlayer.Ongoings>();
        }
        public string getName()
        {
            return cardName;
        }

        private ImageSource getImageSource(string path)
        {
            BitmapImage src = new BitmapImage();
            src.BeginInit();
            src.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
            src.EndInit();

            return src;
        }

        public void SendToGraveyard(IPlayer player, List<Card> currentList)
        {
            if (cardType == CardType.Ongoing)
            {
                foreach (var effect in effects)
                    player.ongoingEffects.Remove(effect);
                player.updateOngoingEffects();
            }
            currentList.Remove(this);
            player.graveyard.Add(this);
        }
    }
}
