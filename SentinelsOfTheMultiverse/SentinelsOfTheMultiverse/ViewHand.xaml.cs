using SentinelsOfTheMultiverse.Data;
using SentinelsOfTheMultiverse.Data.Effects;
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

        List<Image> imageSelectedArray = new List<Image>();

        List<Card> handShow
        {
            get
            {
                return ((Hero)GameEngine.getCurrentPlayer()).getPlayerHand();
            }
            set
            {
                handShow= value;
            }
        }

        List<Card> cardClickedArray = new List<Card>();

        GameBoard gameBoard;


        public ViewHand(List<Card> hand, GameBoard game)
        {
            InitializeComponent();

            gameBoard = game;

            updateHandView();

            Closing += Window_Closed;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            gameBoard.updateBoard();
        }

        private void addButtons()
        {
            sideBar.Children.RemoveRange(0, sideBar.Children.Count);
            Button discardButton = new Button();
            discardButton.Content = "Discard";
            discardButton.Click += new RoutedEventHandler(Discard_Action);

            Button cancelButton = new Button();
            cancelButton.Content = "Cancel Action";
            cancelButton.Click += new RoutedEventHandler(Cancel_Action);

            Button closeButton = new Button();
            closeButton.Content = "Close";
            closeButton.Click += new RoutedEventHandler(Close_Action);

            Button endTurnButton = new Button();
            endTurnButton.Content = "End Turn!";
            endTurnButton.Click += new RoutedEventHandler(End_Turn);
            
            if (!GameEngine.playerPlayedCard)
            {
                Button playButton = new Button();
                playButton.Content = "Play Card";
                playButton.Click += new RoutedEventHandler(Play_Card);
                sideBar.Children.Add(playButton);
            }

            sideBar.Children.Add(cancelButton);
            sideBar.Children.Add(closeButton);
            sideBar.Children.Add(endTurnButton);
            sideBar.Children.Add(discardButton);
        }

        private void updateHandView(){
            cardLayout.Children.RemoveRange(0, cardLayout.Children.Count);
            cardLayout = initGrid(handShow);
            paintCards();
            addButtons();
            cardLayout.Children.Add(sideBar);
            Content = cardLayout;
            SizeToContent = System.Windows.SizeToContent.WidthAndHeight;
        }

        private void Discard_Action(object sender, RoutedEventArgs e)
        {
            CardDrawingEffects.DiscardCardFromHand(cardClickedArray);
            updateHandView();
            gameBoard.updateBoard();
        }
        
        private void End_Turn(object sender, RoutedEventArgs e)
        {
            //TODO attempting to fix bug # 2
            //if (GameEngine.isFirstTurn)
            //{
            //    GameEngine.getCurrentPlayer().playerTurn(GameEngine.playerPlayedCard, GameEngine.playerUsedPower);
            //    GameEngine.isFirstTurn = false;
            //}
            gameBoard.Clear_Selection(sender, e);
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
                if (imageSelectedArray[k] != null)
                {
                    imageSelectedArray[k].Effect = null;
                }
            }
            imageSelectedArray.Clear();
            cardClickedArray.Clear();
        }

        public void paintCards()
        {
            var numCards = handShow.Count;
            for (int k = 0; k < numCards; k++)
            {
                Image temp = new Image();
                temp.Source = handShow[k].cardImage.Source;
                
                Grid.SetColumn(temp, k);
                temp.AddHandler(UIElement.MouseDownEvent, new RoutedEventHandler(Card_Selection_Handler), true);

                temp.Margin = Utility.cardSpacing;

                cardLayout.Children.Add(temp);
            }
        }

        public void Card_Selection_Handler(object sender, RoutedEventArgs e)
        {
            if (!imageSelectedArray.Contains((Image)sender)) imageSelectedArray.Add((Image)sender);

            for (int i = 0; i < handShow.Count; i++)
            {
                for (int k = 0; k < imageSelectedArray.Count; k++)
                {
                    if (handShow[i].cardImage.Source == imageSelectedArray[k].Source && !cardClickedArray.Contains(handShow[i]))
                    {
                        if (imageSelectedArray[k].Effect == null) imageSelectedArray[k].Effect = Utility.selectionGlowHero();
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
                gameBoard.drawCardSelected(cardClickedArray[0]);
                GameEngine.getCurrentPlayer().CardMethod(cardClickedArray[0]);
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
