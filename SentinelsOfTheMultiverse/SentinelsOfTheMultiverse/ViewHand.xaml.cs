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

            cardLayout = initGrid(hand);

            paintCards(hand);

            Content = cardLayout;
        }

        public void paintCards(List<Card> handToShow)
        {
            var numCards = handToShow.Count;
            for (int k = 0; k < numCards; k++)
            {
                Image temp = new Image();
                temp.Source = handToShow[k].cardImage.Source;
                Grid.SetColumn(temp, k);
                cardLayout.Children.Add(temp);
            }
        }

        public Grid initGrid(List<Card> handToShow)
        {
            Grid myGrid = new Grid();

            RowDefinition row = new RowDefinition();
            row.Height = GridLength.Auto;
            myGrid.RowDefinitions.Add(row);

            for (int kk = 0; kk < handToShow.Count ; kk++)
            {
                ColumnDefinition col = new ColumnDefinition();
                col.Width = GridLength.Auto;
                myGrid.ColumnDefinitions.Add(col);
            }
            return myGrid;
        }
    }
}
