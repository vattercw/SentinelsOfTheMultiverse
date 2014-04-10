using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;

namespace SentinelsOfTheMultiverse
{
    class CardPanel:PictureBox
    {
        public CardPanel(ImageSource cardImage)
        {
            Image magnifiedCard = new Image();
            magnifiedCard.Source = cardImage;

            this.Image = magnifiedCard;
        }

    }
}
