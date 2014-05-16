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
using SentinelsOfTheMultiverse.Data;

namespace SentinelsOfTheMultiverse
{
    //[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class ViewCard : Window
    {
        public Image magnifiedCard = new Image();
        private GameBoard gameBoard;

        public ViewCard(ImageSource cardImage, GameBoard board)
        {
            InitializeComponent();
            
            magnifiedCard.Source = cardImage;

            Button powerButton = new Button();
            powerButton.Content = "Play Power";
            powerButton.Click += new RoutedEventHandler(Power_Action);

            StackPanel sp = new StackPanel();
            sp.Children.Add(powerButton);
            sp.Children.Add(magnifiedCard);
            

            sp.CanVerticallyScroll = true;

            sp.MouseDown += new MouseButtonEventHandler(CloseWindow);

            gameBoard = board;
            Content = sp;
        }

        private void Power_Action(object sender, RoutedEventArgs e)
        {
            if (GameEngine.playerUsedPower == false || (GameEngine.getCurrentPlayer().numPowers == 2 && GameEngine.playerUsedSecondPower == false))
            {
                Card card;
                for (int i = 0; i < GameEngine.getCurrentPlayer().cardsOnField.Count; i++)
                {
                    if (GameEngine.getCurrentPlayer().cardsOnField[i].Source.Equals(magnifiedCard.Source))
                    {
                        card = GameEngine.getCurrentPlayer().cardsOnField[i];
                        //if card.haspower = true....
                        if (GameEngine.getCurrentPlayer().numPowers == 2)
                        {
                            GameEngine.getCurrentPlayer().numPowers = 1;
                        }
                        if (card.cardPower != null)
                        {
                            card.cardPower(card,null);
                            if (GameEngine.getCurrentPlayer().numPowers == 2 && GameEngine.playerUsedPower == true)
                            {
                                GameEngine.playerUsedSecondPower = true;
                            }
                            GameEngine.playerUsedPower = true;
                            
                            gameBoard.updateBoard();
                            Close();
                        }
                        //GameEngine.getCurrentPlayer().CardMethod(card);
                        
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        public void CloseWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                this.Close();
            }
        }

    }
}
