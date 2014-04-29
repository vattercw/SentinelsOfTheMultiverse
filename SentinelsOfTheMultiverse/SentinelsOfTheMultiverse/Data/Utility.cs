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
        public static Thickness cardSpacing = new Thickness(6, 4, 6, 4);
        public static System.Windows.Visibility SHOW = Visibility.Visible;
        public static System.Windows.Visibility HIDE = Visibility.Hidden;
        public static char splitDelimeter = '-';

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

        public static DropShadowEffect selectionGlowHero()
        {
            DropShadowEffect glowEffect = new DropShadowEffect();
            glowEffect.Direction = 220;
            glowEffect.ShadowDepth = 5;
            glowEffect.BlurRadius = 5;

            Color cardGlowColor = new Color();
            cardGlowColor.ScA = 1;
            cardGlowColor.ScR = 0.30F;
            cardGlowColor.ScG = 0F;
            cardGlowColor.ScB = 0.49F;

            glowEffect.Color = cardGlowColor;

            return glowEffect;
        }

        public static DropShadowEffect selectionGlowVillain()
        {
            DropShadowEffect glowEffect = new DropShadowEffect();
            glowEffect.Direction = 220;
            glowEffect.ShadowDepth = 5;
            glowEffect.BlurRadius = 5;

            Color cardGlowColor = new Color();
            cardGlowColor.ScA = 1;
            cardGlowColor.ScR = 1;
            cardGlowColor.ScG = 0.1F;
            cardGlowColor.ScB = 0.25F;

            glowEffect.Color = cardGlowColor;

            return glowEffect;
        }

        public static DropShadowEffect selectionGlowEnvironment()
        {
            DropShadowEffect glowEffect = new DropShadowEffect();
            glowEffect.Direction = 220;
            glowEffect.ShadowDepth = 5;
            glowEffect.BlurRadius = 5;

            Color cardGlowColor = new Color();
            cardGlowColor.ScA = 1;
            cardGlowColor.ScR = 0.10F;
            cardGlowColor.ScG = 1;
            cardGlowColor.ScB = 0.3F;

            glowEffect.Color = cardGlowColor;

            return glowEffect;
        }

        public static string removeNumOfCards(string name)
        {
            return name.Split(splitDelimeter)[1];
        }

        public static int getNumOfCards(string name)
        {
            return Convert.ToInt16(name.Split(splitDelimeter)[0]);
        }
    }
}