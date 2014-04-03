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
    public partial class ViewCard : Window
    {
        public ViewCard(ImageSource cardImage)
        {
            InitializeComponent();

            Image magnifiedCard = new Image();
            magnifiedCard.Source = cardImage;

            StackPanel sp = new StackPanel();
            sp.Children.Add(magnifiedCard);

            sp.MouseUp += new MouseButtonEventHandler(CloseWindow);

            Content = sp;
        }

        public void CloseWindow(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

    }
}
