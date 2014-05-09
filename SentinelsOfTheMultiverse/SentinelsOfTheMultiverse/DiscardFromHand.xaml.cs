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
    public partial class DiscardFromHand : Window
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


        public DiscardFromHand(List<Card> hand, GameBoard game)
        {
            InitializeComponent();
            
            gameBoard = game;

            updateHandView();

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

        private void updateHandView(){
            cardLayout.Children.RemoveRange(0, cardLayout.Children.Count+1);
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
        

        private void Close_Action(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Cancel_Action(object sender, RoutedEventArgs e)
        {
            foreach(Card clearCard in cardClickedArray){
                clearCard.Effect = null;
            }
            cardClickedArray.Clear();
        }

        public void paintCards()
        {
            var numCards = handShow.Count;
            for (int k = 0; k < numCards; k++)
            {
                Grid.SetColumn(handShow[k], k);
                handShow[k].AddHandler(UIElement.MouseDownEvent, new RoutedEventHandler(Card_Selection_Handler), true);

                handShow[k].Height = 400;
                handShow[k].Margin = Utility.cardSpacing;

                cardLayout.Children.Add(handShow[k]);
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
    }
}
