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
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class DiscardFromBoard : Window
    {
        Grid cardLayout = new Grid();

        StackPanel sideBar = new StackPanel();

        List<Image> imageSelectedArray = new List<Image>();

        List<Card> boardShow = new List<Card>();

        List<Card> cardClickedArray = new List<Card>();

        GameBoard gameBoard;


        public DiscardFromBoard(GameBoard game)
        {
            InitializeComponent();
            
            gameBoard = game;

            updateBoardView();

            boardShow = GameEngine.getCurrentPlayer().cardsOnField;

            Closing += Window_Closed;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            cardLayout.Children.RemoveRange(0, cardLayout.Children.Count+1);
            gameBoard.updateBoard();
        }

        private void addButtons()
        {
            sideBar.Children.RemoveRange(0, sideBar.Children.Count);
            Button discardButton = new Button();
            discardButton.Content = "Discard";
            discardButton.Click += new RoutedEventHandler(Discard_Action);

            Button closeButton = new Button();
            closeButton.Content = "No Discard.";
            closeButton.Click += new RoutedEventHandler(Close_Action);

            sideBar.Children.Add(closeButton);
            sideBar.Children.Add(discardButton);

        }

        private void updateBoardView(){
            cardLayout.Children.RemoveRange(0, cardLayout.Children.Count+1);
            cardLayout = initGrid(boardShow);
            paintCards();
            addButtons();
            cardLayout.Children.Add(sideBar);
            Content = cardLayout;
            SizeToContent = System.Windows.SizeToContent.WidthAndHeight;
        }

        private void Discard_Action(object sender, RoutedEventArgs e)
        {
            var currentPlayer = GameEngine.getCurrentPlayer();

            if (cardClickedArray.Count > 0)
            {
                foreach (Card toDiscard in cardClickedArray)
                {
                    if (currentPlayer.cardsOnField.Contains(toDiscard)) toDiscard.SendToGraveyard(currentPlayer, currentPlayer.cardsOnField);
                }
            }
            updateBoardView();
            gameBoard.updateBoard();
        }
        

        private void Close_Action(object sender, RoutedEventArgs e)
        {
            if (cardClickedArray.Count > 0)
            {
                this.Close();
            }
        }

        public void paintCards()
        {
            var numCards = boardShow.Count;
            for (int k = 0; k < numCards; k++)
            {
                Grid.SetColumn(boardShow[k], k);
                boardShow[k].AddHandler(UIElement.MouseDownEvent, new RoutedEventHandler(Card_Selection_Handler), true);

                boardShow[k].Height = 400;
                boardShow[k].Margin = Utility.cardSpacing;

                cardLayout.Children.Add(boardShow[k]);
            }
        }

        public void Card_Selection_Handler(object sender, RoutedEventArgs e)
        {
            var cardClicked = (Card)sender;
            if (!cardClickedArray.Contains(cardClicked))
            {
                cardClicked.Effect = Utility.selectionGlowHero();
                cardClickedArray.Add(cardClicked);
            }
            else
            {
                cardClicked.Effect = null;
                cardClickedArray.Remove(cardClicked);
            }
        }

        public Grid initGrid(List<Card> BoardToShow)
        {
            Grid myGrid = new Grid();

            RowDefinition row = new RowDefinition();
            row.Height = GridLength.Auto;
            myGrid.RowDefinitions.Add(row);

            for (int kk = 0; kk < BoardToShow.Count + 1; kk++)
            {
                ColumnDefinition col = new ColumnDefinition();
                col.Width = GridLength.Auto;
                myGrid.ColumnDefinitions.Add(col);
            }
            ColumnDefinition colm = new ColumnDefinition();
            colm.Width = GridLength.Auto;
            myGrid.ColumnDefinitions.Add(colm);
            Grid.SetColumn(sideBar, BoardToShow.Count + 2);

            return myGrid;
        }
    }
}
