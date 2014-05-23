using SentinelsOfTheMultiverse.Data;
using SentinelsOfTheMultiverse.Data.Effects;
using SentinelsOfTheMultiverse.Data.Heroes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using SentinelsOfTheMultiverse.Data.Environments;
using SentinelsOfTheMultiverse.Data.Villains;

namespace SentinelsOfTheMultiverse
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class ViewHand : Window
    {
        Grid cardLayout = new Grid();

        StackPanel sideBar = new StackPanel();

        List<Card> handShow
        {
            get
            {
                return ((Hero)GameEngine.getCurrentPlayer()).getPlayerHand();
            }
            set
            {
                handShow = value;
            }
        }

        List<Card> cardClickedArray = new List<Card>();

        GameBoard gameBoard;


        public ViewHand(List<Card> hand, GameBoard game)
        {
            InitializeComponent();

            gameBoard = game;

            updateHandView();

            //addScrollBar();

            Closing += Window_Closed;
        }

        //private void addScrollBar()
        //{
        //    ScrollViewer viewer = new ScrollViewer();
        //    viewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
        //    Grid.SetRow(viewer, 2);
        //    cardLayout.Children.Add(viewer);
        //}

        private void Window_Closed(object sender, EventArgs e)
        {
            cardLayout.Children.RemoveRange(0, cardLayout.Children.Count + 1);
            gameBoard.updateBoard();
        }

        private void addButtons()
        {
            sideBar.Children.RemoveRange(0, sideBar.Children.Count);

            if (((Targetable)GameEngine.getCurrentPlayer()).lifeTotal <= 0)
            {

                Card death = new Card("Images\\Hero\\" + GameEngine.getCurrentPlayer().characterName + "\\" + GameEngine.getCurrentPlayer().characterName.ToLower() + "_death.jpg");
                Grid.SetColumn(death, 1);
                List<Card> deadHand = new List<Card>();
                deadHand.Add(death);
                initGrid(deadHand);

                Button power1Button = new Button();
                power1Button.Content = SentinelsOfTheMultiverse.Properties.Resources.ResourceManager.GetString("Power1");
                power1Button.Click += new RoutedEventHandler(Power1_Action);

                Button power2Button = new Button();
                power2Button.Content = SentinelsOfTheMultiverse.Properties.Resources.ResourceManager.GetString("Power2");
                power2Button.Click += new RoutedEventHandler(Power2_Action);

                Button power3Button = new Button();
                power3Button.Content = SentinelsOfTheMultiverse.Properties.Resources.ResourceManager.GetString("Power3");
                power3Button.Click += new RoutedEventHandler(Power3_Action);

                sideBar.Children.Add(power1Button);
                sideBar.Children.Add(power2Button);
                sideBar.Children.Add(power3Button);

            }
            else
            {

                Button discardButton = new Button();
                discardButton.Content = SentinelsOfTheMultiverse.Properties.Resources.ResourceManager.GetString("Discard");
                discardButton.Click += new RoutedEventHandler(Discard_Action);

                Button cancelButton = new Button();
                cancelButton.Content = SentinelsOfTheMultiverse.Properties.Resources.ResourceManager.GetString("CancelAction");
                cancelButton.Click += new RoutedEventHandler(Cancel_Action);

                Button closeButton = new Button();
                closeButton.Content = SentinelsOfTheMultiverse.Properties.Resources.ResourceManager.GetString("Close");
                closeButton.Click += new RoutedEventHandler(Close_Action);

                Button endTurnButton = new Button();
                endTurnButton.Content = SentinelsOfTheMultiverse.Properties.Resources.ResourceManager.GetString("EndTurn");
                endTurnButton.Click += new RoutedEventHandler(End_Turn);

                if (!GameEngine.playerPlayedCard)
                {
                    Button playButton = new Button();
                    playButton.Content = SentinelsOfTheMultiverse.Properties.Resources.ResourceManager.GetString("PlayCard");
                    playButton.Click += new RoutedEventHandler(Play_Card);
                    sideBar.Children.Add(playButton);
                }

                if (!GameEngine.playerUsedPower)
                {
                    Button powerButton = new Button();
                    powerButton.Content = SentinelsOfTheMultiverse.Properties.Resources.ResourceManager.GetString("PlayerPower");
                    powerButton.Click += new RoutedEventHandler(Power_Action);
                    sideBar.Children.Add(powerButton);
                }

                sideBar.Children.Add(cancelButton);
                sideBar.Children.Add(closeButton);
                sideBar.Children.Add(endTurnButton);
                sideBar.Children.Add(discardButton);
            }

        }

        private void Power3_Action(object sender, RoutedEventArgs e)
        {
            GameEngine.getCurrentPlayer().DeathPower1();
        }

        private void Power2_Action(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Power1_Action(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Power_Action(object sender, RoutedEventArgs e)
        {
            if (GameEngine.playerUsedPower == false)
            {
                GameEngine.getCurrentPlayer().Power();
                GameEngine.playerUsedPower = true;
            }
        }

        private void updateHandView()
        {
            cardLayout.Children.RemoveRange(0, cardLayout.Children.Count + 1);
            cardLayout = initGrid(handShow);
            paintCards();
            addButtons();
            cardLayout.Children.Add(sideBar);
            Content = cardLayout;
            SizeToContent = System.Windows.SizeToContent.WidthAndHeight;
        }

        private void Discard_Action(object sender, RoutedEventArgs e)
        {
            if (cardClickedArray.Count != 0)
            {
                CardDrawingEffects.DiscardCardFromHand(cardClickedArray, (Hero)GameEngine.getCurrentPlayer());
                updateHandView();
                gameBoard.updateBoard();
            }
            else MessageBox.Show( SentinelsOfTheMultiverse.Properties.Resources.ResourceManager.GetString("DiscardSomething"));
        }

        private void End_Turn(object sender, RoutedEventArgs e)
        {
            //TODO attempting to fix bug # 2
            //if (GameEngine.isFirstTurn)
            //{
            //    GameEngine.getCurrentPlayer().playerTurn(GameEngine.playerPlayedCard, GameEngine.playerUsedPower);
            //    GameEngine.isFirstTurn = false;
            //}
            foreach (Card clearCard in cardClickedArray)
            {
                clearCard.Effect = null;
            }
            cardClickedArray.Clear();

            gameBoard.Clear_Selection(sender, e);
            
            object[] result = GameEngine.getCurrentPlayer().endPhase();
            if (result != null) {
                switch((GameEngine.ForcedEffect)result[0]){
                    case GameEngine.ForcedEffect.ObsidianField:
                        if ((System.Windows.Forms.DialogResult)result[1] == System.Windows.Forms.DialogResult.Yes) {
                            Close();
                            
                            Hero currentPlayer = ((Hero)GameEngine.getCurrentPlayer());
                            DiscardFromHand discardHand = new DiscardFromHand(currentPlayer.hand, gameBoard, currentPlayer);
                            discardHand.Visibility = System.Windows.Visibility.Visible;
                            discardHand.ShowDialog();

                            GameEnvironment env = GameEngine.getEnvironment();
                            Card card = env.cardsOnField.Find(x => x.getName().Equals("ObsidianField"));
                            card.SendToGraveyard(env, env.cardsOnField);
                        }
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
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
            foreach (Card clearCard in cardClickedArray)
            {
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

        public void Play_Card(object sender, RoutedEventArgs e)
        {
            if (cardClickedArray.Count == 1)
            {
                this.Close();
                cardClickedArray[0].Height = 200;
                cardClickedArray[0].Effect = null;
                gameBoard.drawCardSelected(cardClickedArray[0]);
                object[] res = GameEngine.getCurrentPlayer().CardMethod(cardClickedArray[0]);

                if (res != null)
                {
                    switch ((GameEngine.ForcedEffect)res[1])
                    {
                        case GameEngine.ForcedEffect.DiscardCurrentPlayer:
                            GameBoard.discardedCardsThisTurn = new List<Card>();
                            Hero currHero = ((Hero)GameEngine.getCurrentPlayer());
                            DiscardFromHand discardedHand = new DiscardFromHand(currHero.getPlayerHand(), gameBoard, currHero);
                            discardedHand.Visibility = System.Windows.Visibility.Visible;
                            discardedHand.ShowDialog();

                            Haka.DiscardedAction discardedAction = (Haka.DiscardedAction)res[0];
                            discardedAction(GameBoard.discardedCardsThisTurn.Count, (Card)res[3]);
                            break;
                    }
                }
                else
                {
                    cardClickedArray.Clear();
                    GameEngine.playerPlayedCard = true;
                    gameBoard.updateBoard();
                    Cancel_Action(sender, e);
                    return;
                }

                cardClickedArray.Clear();
                GameEngine.playerPlayedCard = true;
                gameBoard.updateBoard();

            }
        }

    }    
}
