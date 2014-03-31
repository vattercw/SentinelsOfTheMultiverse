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
        string cardName;
        List<IEffect> effects;
        public Image cardImage = new Image();
        public string cardImageStr = "";

        public Card(string cardPath, string name)
        {
            cardImage.Source = getImageSource(cardPath);
            cardName = name;
            cardImageStr = cardPath;
        }
        public string getName()
        {
            return cardName;
        }

        private ImageSource getImageSource(string path)
        {
            BitmapImage src = new BitmapImage();
            src.BeginInit();
            src.UriSource = new Uri(path, UriKind.Absolute);
            src.EndInit();

            return src;
        }

    }
}
