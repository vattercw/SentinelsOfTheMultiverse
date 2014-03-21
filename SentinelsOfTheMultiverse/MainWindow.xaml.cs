using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SentinelsOfTheMultiverse
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            initGame();
        }

        public Image initGame()
        {
            Uri darkVisionary= new Uri("Images/Dark Visionary.png", UriKind.Relative);
                        
            Uri legacy= new Uri("Images/Legacy.jpg", UriKind.Relative);

            List<Uri> deck = new List<Uri> { };
            deck.Add(darkVisionary);
            deck.Add(legacy);

            Image imgPlaceholder= new Image();
            imgPlaceholder.Source = new BitmapImage(darkVisionary);

            imgPlaceholder.MouseUp += (s, e) =>
            {
                Random r = new Random();
                var randomUri = deck[r.Next(0, deck.Count)];
                imgPlaceholder.Source = new BitmapImage(randomUri);
            };

            gridView.Children.Add(imgPlaceholder);
            return imgPlaceholder;
        }
    }
}
