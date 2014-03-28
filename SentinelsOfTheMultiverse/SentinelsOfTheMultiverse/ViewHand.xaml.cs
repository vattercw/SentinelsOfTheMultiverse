using SentinelsOfTheMultiverse.Data;
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
    public partial class ViewHand : Window
    {
        Grid cardLayout = new Grid();

        public ViewHand(List<Card> hand)
        {
            InitializeComponent();

            paintCards(hand);

            Content = cardLayout;
        }

        public void paintCards(List<Card> handToShow)
        {
            var numCards = handToShow.Count;
            for (int k = 0; k < numCards; k++)
            {
                Image temp = new Image();
                temp.Height = 300;
                temp = handToShow[k].cardImage;
                Grid.SetColumn(temp, 0);
                cardLayout.Children.Add(temp);
            }
            RowDefinition row = new RowDefinition();
            row.Height = GridLength.Auto;

            cardLayout.RowDefinitions.Add(row);

            ColumnDefinition col = new ColumnDefinition();
            col.Width = GridLength.Auto;
            cardLayout.ColumnDefinitions.Add(col);
        }
    }
}
