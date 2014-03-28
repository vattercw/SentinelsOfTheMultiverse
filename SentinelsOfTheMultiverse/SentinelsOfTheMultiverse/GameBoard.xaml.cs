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
using System.Windows.Shapes;

namespace SentinelsOfTheMultiverse
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {


        public Window1()
        {
            InitializeComponent();
            
            DataContext = this;
            initBoard();
        }

        private void initBoard()
        {
            StackPanel sp = new StackPanel();
            Button showHand = new Button();
            //showHand.Click += View_Hand();
            sp.Children.Add(new Button { Content = "Button" });

            Image i = new Image();
            i.Height = 200;
            i.Source = getImageSource("Images/Hero/Haka/haka_hero.png");
            
            //int q = src.PixelHeight;        // Image loads here
            sp.Children.Add(i);

            Content = sp;

        }

        private ImageSource getImageSource(string path){
            BitmapImage src = new BitmapImage();
            src.BeginInit();
            src.UriSource = new Uri(path, UriKind.Relative);
            src.EndInit();

            return src;
        }
        
        private void View_Hand(object sender, RoutedEventArgs e)
        {

        }

        private void View_Card_Full(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
