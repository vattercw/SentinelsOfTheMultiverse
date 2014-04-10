using SentinelsOfTheMultiverse.Data;
using System;
using System.Collections;
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

        ArrayList imageSelectedArray = new ArrayList();

        List<Card> handShow;

        ArrayList cardClickedArray = new ArrayList();

        GameBoard gameBoard;


        public ViewHand(List<Card> hand, GameBoard game)
        {
            InitializeComponent();

            handShow = hand;
            gameBoard = game;

            cardLayout = initGrid(hand);

            paintCards(hand);


            cardLayout.Children.Add(sideBar);

            if (!GameEngine.playerPlayedCard)
            {
                Button playButton = new Button();
                playButton.Content = "Play Card";
                playButton.Click += new RoutedEventHandler(Play_Card);
                sideBar.Children.Add(playButton);
            }

            Button cancelButton = new Button();
            cancelButton.Content = "Cancel Action";

            Button closeButton = new Button();
            closeButton.Content = "Close";

            Button endTurnButton = new Button();
            endTurnButton.Content = "End Turn!";

            closeButton.Click += new RoutedEventHandler(Close_Action);

            cancelButton.Click += new RoutedEventHandler(Cancel_Action);

            endTurnButton.Click += new RoutedEventHandler(End_Turn);

            sideBar.Children.Add(cancelButton);

            sideBar.Children.Add(closeButton);

            sideBar.Children.Add(endTurnButton);

            SizeToContent = System.Windows.SizeToContent.WidthAndHeight;
            Content = cardLayout;
        }
        
        private void End_Turn(object sender, RoutedEventArgs e)
        {
            //TODO attempting to fix bug # 2
            //if (GameEngine.isFirstTurn)
            //{
            //    GameEngine.getCurrentPlayer().playerTurn(GameEngine.playerPlayedCard, GameEngine.playerUsedPower);
            //    GameEngine.isFirstTurn = false;
            //}
            GameEngine.nextTurn();
            gameBoard.updateBoard();
            this.Close();
        }

        private void Close_Action(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Cancel_Action(object sender, RoutedEventArgs e)
        {
            for(int k = 0; k < imageSelectedArray.Count; k++)
            {
                if (((Image)imageSelectedArray[k]) != null)
                {
                    ((Image)imageSelectedArray[k]).Effect = null;
                }
            }
            imageSelectedArray.Clear();
            cardClickedArray.Clear();
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
            imageSelectedArray.Add((Image)sender);
            foreach (Image imageSelected in imageSelectedArray)
            {
                imageSelected.Effect = Utility.selectionGlow();
            }


            for (int i = 0; i < handShow.Count; i++)
            {
                for (int k = 0; k < imageSelectedArray.Count; k++)
                {
                    if (handShow[i].cardImage.Source == ((Image)imageSelectedArray[k]).Source && !cardClickedArray.Contains(handShow[i]))
                    {
                        cardClickedArray.Add(handShow[i]);
                        break; 
                    }
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
            if (cardClickedArray.Count == 1)
            {
                gameBoard.drawCardSelected((Card)cardClickedArray[0]);
                GameEngine.getCurrentPlayer().CardMethod(((Card)cardClickedArray[0]).getName());
                GameEngine.playerPlayedCard = true;
                gameBoard.updateBoard();
                this.Close();
            }
            else
            {
                Cancel_Action(sender, e);
                return;
            }
        }
    }
}
