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
using System.Windows.Media.Effects;
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

        StackPanel sideBar = new StackPanel();

        Image imageSelected = null;

        List<Card> handShow;

        Card cardClicked = null;

        GameBoard gameBoard;


        public ViewHand(List<Card> hand, GameBoard game)
        {
            InitializeComponent();

            handShow = hand;
            gameBoard = game;

            cardLayout = initGrid(hand);

            paintCards(hand);


            cardLayout.Children.Add(sideBar);

            Button playButton = new Button();
            playButton.Content = "Play Card";

            Button cancelButton = new Button();
            cancelButton.Content = "Cancel Action";

            Button closeButton = new Button();
            closeButton.Content = "Close";

            Button endTurnButton = new Button();
            endTurnButton.Content = "End Turn!";

            playButton.Click += new RoutedEventHandler(Play_Card);

            closeButton.Click += new RoutedEventHandler(Close_Action);

            cancelButton.Click += new RoutedEventHandler(Cancel_Action);

            endTurnButton.Click += new RoutedEventHandler(End_Turn);

            sideBar.Children.Add(playButton);

            sideBar.Children.Add(cancelButton);

            sideBar.Children.Add(closeButton);

            sideBar.Children.Add(endTurnButton);


            Content = cardLayout;
                 
        }
        
        private void End_Turn(object sender, RoutedEventArgs e)
        {
            GameEngine.nextTurn();
            gameBoard.initBoard();
            this.Hide();
        }

        private void Close_Action(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Cancel_Action(object sender, RoutedEventArgs e)
        {
            imageSelected = null;
        }

        public void paintCards(List<Card> handToShow)
        {
            var numCards = handToShow.Count;
            for (int k = 0; k < numCards; k++)
            {
                Image temp = new Image();
                temp.Source = handToShow[k].cardImage.Source;
                
                Grid.SetColumn(temp, k);
                temp.AddHandler(UIElement.MouseUpEvent, new RoutedEventHandler(card_mouseUp_eventHandler), true);

                temp.Margin = Utility.cardSpacing;

                cardLayout.Children.Add(temp);
            }
        }

        public void card_mouseUp_eventHandler(object sender, RoutedEventArgs e)
        {
            if (imageSelected != null) imageSelected.Effect = null;

            imageSelected = (Image)sender;
            imageSelected.Effect = Utility.selectionGlow();
            

            for (int i = 0; i < handShow.Count ; i++)
            {
                if (handShow[i].cardImage.Source == imageSelected.Source)
                {
                    cardClicked = handShow[i];
                    break;
                }
            }
           
        }

        public Grid initGrid(List<Card> handToShow)
        {
            Grid myGrid = new Grid();

            RowDefinition row = new RowDefinition();
            row.Height = GridLength.Auto;
            myGrid.RowDefinitions.Add(row);

            for (int kk = 0; kk < handToShow.Count + 1; kk++)
            {
                ColumnDefinition col = new ColumnDefinition();
                col.Width = GridLength.Auto;
                myGrid.ColumnDefinitions.Add(col);
            }
            ColumnDefinition colm = new ColumnDefinition();
            colm.Width = GridLength.Auto;
            myGrid.ColumnDefinitions.Add(colm);
            Grid.SetColumn(sideBar, handToShow.Count + 2);

            return myGrid;
        }

        public void Play_Card(object sender, RoutedEventArgs e) {
            if (imageSelected == null)
            {
                return;
            }
            else
            {
                gameBoard.drawCardSelected(cardClicked);
                GameEngine.playerPlayedCard = true;
                this.Hide();
            }
        }
    }
}
