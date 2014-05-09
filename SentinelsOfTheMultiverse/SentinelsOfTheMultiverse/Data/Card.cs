using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SentinelsOfTheMultiverse.Data
{
    public class Card: Image
    {
        public enum CardLocation { Hand, Deck, Graveyard };
        public enum CardType { OneShot, Ongoing, Equipment, Environment };
        string cardName;

        private readonly double CARD_HEIGHT = 200;
        internal List<IPlayer.Ongoings> effects { get; set; }
        public string cardImageStr = "";
        public CardType cardType { get; set; }
        private int cardID;
        private int numOfCard;

        public event CardDestroyedHandler CardDestroyed;
        public delegate void CardDestroyedHandler(Card m, EventArgs e);

        public Power cardPower;
        public delegate void Power(object sender, object[] arguments);

        public Card(string cardPath)
        {
            Source = getImageSource(cardPath);
            cardName = Path.GetFileNameWithoutExtension(cardPath);
            if (cardName.Contains(Utility.splitDelimeter))
            {
                cardName= cardName.Split(Utility.splitDelimeter)[1];
            }
            cardImageStr = cardPath;
            effects = new List<IPlayer.Ongoings>();
            cardID = GameEngine.cardIDNum;
            Height = CARD_HEIGHT;
            GameEngine.cardIDNum +=1;
            SetLocalizedTooltip();

        }


        private void SetLocalizedTooltip()
        {
            string localizedName = SentinelsOfTheMultiverse.Properties.Resources.ResourceManager.GetString(cardName);
            if (!String.IsNullOrEmpty(localizedName))
            {
                ToolTip tooltip = new ToolTip();
                tooltip.DataContext = this;
                string localizedEffect = SentinelsOfTheMultiverse.Properties.Resources.ResourceManager.GetString(cardName + "Effect");
                tooltip.Content = localizedName + " | " + localizedEffect;
                ToolTipService.SetToolTip(this, tooltip);
            }
        }

        public string getName()
        {
            return cardName;
        }

        public int getCardID()
        {
            return cardID;
        }

        public void setNumOfCard(int number)
        {
            numOfCard = number;
        }

        public int getNumOfCard()
        {
            return numOfCard;
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
            if(CardDestroyed != null)
                CardDestroyed(this, null);
            
            currentList.Remove(this);
            player.graveyard.Add(this);
        }
    }
}
