using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Collections.ObjectModel;

namespace SentinelsOfTheMultiverse
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<string> hand { get; set; }
        public ObservableCollection<string> deck { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            //initGame();
            
            Hero p1 = new Hero();
            hand = new ObservableCollection<string>(p1.hand);
            deck = new ObservableCollection<string>(p1.deck.getCards());
            this.DataContext = this;
        }

        public void initGame()
        {
            //Uri darkVisionary= new Uri("Images/Dark Visionary.png", UriKind.Relative);
                        
            //Uri legacy= new Uri("Images/Legacy.jpg", UriKind.Relative);

            //List<Uri> deck = new List<Uri> { };
            //for (int ii = 0; ii < 10; ii++)
            //{
            //    deck.Add(darkVisionary);
            //    deck.Add(legacy);
            //}

            //Image imgPlaceholder= new Image();
            //imgPlaceholder.Source = new BitmapImage(darkVisionary);

            //imgPlaceholder.MouseUp += (s, e) =>
            //{
            //    Random r = new Random();
            //    var randomUri = deck[r.Next(0, deck.Count)];
            //    imgPlaceholder.Source = new BitmapImage(randomUri);
            //};

            //handView.Children.Add(imgPlaceholder);
            //return imgPlaceholder;
        }
    }

    
}
