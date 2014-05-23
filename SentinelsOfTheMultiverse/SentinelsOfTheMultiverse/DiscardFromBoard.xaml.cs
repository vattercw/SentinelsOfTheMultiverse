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
using System.Resources;
using System.Reflection;

namespace SentinelsOfTheMultiverse
{

    

    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class DiscardFromBoard : Window
    {

        public static ResourceManager Translator = new ResourceManager("SentinelsOfTheMultiverse.Resources.ja-JP", Assembly.Load("SentinelsOfTheMultiverse"));

        Grid cardLayout = new Grid();

        StackPanel sideBar = new StackPanel();

        List<Image> imageSelectedArray = new List<Image>();

        List<Card> boardShow = new List<Card>();

        List<Card> cardClickedArray = new List<Card>();

        IPlayer currentPlayer;
        GameBoard gameBoard;

        int minimumToDiscard = -1;
        int numDiscarded = 0;


        public DiscardFromBoard(GameBoard game, IPlayer player)
        {
            InitializeComponent();
            
            gameBoard = game;
            currentPlayer = player;
            boardShow = player.cardsOnField;

            updateBoardView();

            Closing += Window_Closed;
        }

        public DiscardFromBoard(GameBoard game, IPlayer player, int min)
        {
            InitializeComponent();

            gameBoard = game;

            currentPlayer = player;
            boardShow = player.cardsOnField;

            updateBoardView();

            minimumToDiscard = min;

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
            discardButton.Content = SentinelsOfTheMultiverse.Properties.Resources.ResourceManager.GetString("Discard"); 
            discardButton.Click += new RoutedEventHandler(Discard_Action);

            Button closeButton = new Button();
            closeButton.Content = SentinelsOfTheMultiverse.Properties.Resources.ResourceManager.GetString("Done"); 
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
            var count = cardClickedArray.Count;
            GameBoard.discardedCardsThisTurn.AddRange(cardClickedArray);
            if (cardClickedArray.Count > 0)
            {
                for (int k = 0; k < count; k++)
                {
                    if (currentPlayer.cardsOnField[k].Name.Equals(cardClickedArray[k].Name))
                    {
                        currentPlayer.cardsOnField[k].SendToGraveyard(currentPlayer, currentPlayer.cardsOnField);
                        numDiscarded += 1;
                    }
                }
            }
            updateBoardView();
            gameBoard.updateBoard();
        }
        

        private void Close_Action(object sender, RoutedEventArgs e)
        {
            if (minimumToDiscard >= numDiscarded || minimumToDiscard == -1)
            {
                this.Close();
            }
            else MessageBox.Show( SentinelsOfTheMultiverse.Properties.Resources.ResourceManager.GetString("PleaseDiscard") + (minimumToDiscard - numDiscarded) +  SentinelsOfTheMultiverse.Properties.Resources.ResourceManager.GetString("MoreCards"));
        }

        public void paintCards()
        {
            var numCards = boardShow.Count;
            for (int k = 0; k < numCards; k++)
            {
                Card myImage = new Card(boardShow[k].Source.ToString());

                Grid.SetColumn(myImage, k);
                myImage.AddHandler(UIElement.MouseDownEvent, new RoutedEventHandler(Card_Selection_Handler), true);

                myImage.Height = 400;
                myImage.Margin = Utility.cardSpacing;

                cardLayout.Children.Add(myImage);
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
