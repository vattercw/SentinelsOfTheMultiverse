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
            GameEngine game = new GameEngine();
            DataContext = game;
        }

        private void initBoard()
        {
            
        }

        
        private void View_Hand(object sender, RoutedEventArgs e)
        {

        }

        private void View_Card_Full(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
