using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;

namespace SentinelsOfTheMultiverse
{
    class Utility
    {
        public static Thickness cardSpacing = new Thickness(5, 5, 5, 5);
        public static System.Windows.Visibility SHOW = Visibility.Visible;
        public static System.Windows.Visibility HIDE = Visibility.Hidden;

        public static ImageSource getImageSource(string path)
        {
            BitmapImage src = new BitmapImage();
            src.BeginInit();
            src.UriSource = new Uri(path, UriKind.Relative);
            src.EndInit();

            return src;
        }

        public static void addElementToGrid(UIElement elem, int row, int col, Grid gridLayout)
        {
            Grid.SetRow(elem, row);
            Grid.SetColumn(elem, col);
            gridLayout.Children.Add(elem);
        }

        public static DropShadowEffect selectionGlow()
        {
            DropShadowEffect glowEffect = new DropShadowEffect();
            glowEffect.Direction = 220;
            glowEffect.ShadowDepth = 5;
            glowEffect.BlurRadius = 5;

            Color cardGlowColor = new Color();
            cardGlowColor.ScA = 1;
            cardGlowColor.ScR = 1;
            cardGlowColor.ScG = 0.53F;
            cardGlowColor.ScB = 0.59F;

            glowEffect.Color = cardGlowColor;

            return glowEffect;
        }
    }
}