using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace SentinelsOfTheMultiverse.Data
{
    public class Card
    {
        string cardName;
        List<IEffect> effects;
        public Image cardImage = new Image();

        public Card(string cardPath)
        {
            cardImage.Source = new BitmapImage(new Uri(cardPath));
        }


    }
}
