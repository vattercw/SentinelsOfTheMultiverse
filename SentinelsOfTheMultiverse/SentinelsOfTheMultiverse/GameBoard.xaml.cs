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
    public partial class GameBoard : Window
    {
        private GameEngine game = new GameEngine();
        private Grid gridLayout = new Grid();
        private ViewHand handViewer;

        #region Constants

            private string HERO_IMAGE_PATH="Images/Hero/";
            private string VILLAIN_IMAGE_PATH = "Images/Villain/";    
            private System.Windows.Visibility SHOW = Visibility.Visible;
            private System.Windows.Visibility HIDE = Visibility.Hidden;

            private int VILLAIN_ROW_NUM=1;
            private int ENVIRONMENT_ROW_NUM=0;
            private int HERO_ROW_NUM = 2;    
            private double CARD_HEIGHT=200;
            private int DECK_COLUMN=2;
            


        #endregion

        public GameBoard()
        {
            InitializeComponent();
            
            DataContext = this;

            gridLayout = initGrid();
            Button showHandButton = new Button();
            showHandButton.Content = "Show Your Hand!";
            showHandButton.Width = 150;
            showHandButton.Height = 50;
            showHandButton.Click += new RoutedEventHandler(View_Hand);

            Grid.SetRow(showHandButton, 0);

            initHandViewer();
            gridLayout.Children.Add(showHandButton);

            List<Hero> heroes = game.getHeroes();
            Villain villain = game.getVillain();
            GameEnvironment env = game.getEnvironment();

            initBoard(heroes, villain, env);

            Content = gridLayout;
        }

        private void initHandViewer()
        {
            Hero currentPlayer= (Hero)game.getCurrentPlayer();
            handViewer = new ViewHand(currentPlayer.getPlayerHand());
		}
		
        private Grid initGrid()
        {
            Grid myGrid = new Grid();

            for (int ll = 0; ll < game.getHeroes().Count+2; ll++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = GridLength.Auto;
                myGrid.RowDefinitions.Add(row);
            }

            for (int kk = 0; kk < 3; kk++)
            {
                ColumnDefinition col = new ColumnDefinition();
                col.Width = GridLength.Auto;
                myGrid.ColumnDefinitions.Add(col);
            }
            return myGrid;
        }

        private void initBoard(List<Hero> heroes, Villain villain, GameEnvironment env)
        {
            string villainName = villain.getCharacterName();
            
            ImageSource villainImg = getImageSource(VILLAIN_IMAGE_PATH + villainName + "/" + villainName + "_initial.png");
            ImageSource villainDeckBackImg = getImageSource(VILLAIN_IMAGE_PATH + villainName + "/" + villainName + "_back.png");
            ImageSource villainInstImg = getImageSource(VILLAIN_IMAGE_PATH + villainName + "/" + villainName + "_instr_front.png");
            initPlayerTemplate(villainDeckBackImg, villainImg, villainInstImg);

            //TODO: don't hardcode things. thats bad
            ImageSource envDeckBackImg = getImageSource("Images/Environment/insula_primus/back_of_card.png");
            initPlayerTemplate(envDeckBackImg);


            for (int ii = 0; ii < heroes.Count; ii++)
            {
                string heroName = heroes[ii].getCharacterName();

                ImageSource heroDeckBackImg= getImageSource(HERO_IMAGE_PATH + heroName + "/" + heroName.ToLower() + "_back.png");
                ImageSource heroImg= getImageSource(HERO_IMAGE_PATH + heroName + "/" + heroName.ToLower() + "_hero.png");
                initPlayerTemplate(heroDeckBackImg, heroImg);
            }   
        }

        //TODO: later add ImageSource graveYardTop
        private void initPlayerTemplate(ImageSource deckBack, ImageSource characterCard=null, ImageSource characterInstructions=null)
        {
            int deckBackRow;
            if (characterInstructions != null)//villain
            {
                Image charInst = new Image();
                charInst.Height = CARD_HEIGHT;
                charInst.Source = characterInstructions;

                addElementToGrid(charInst, VILLAIN_ROW_NUM, 1);

                Image charCardImg = new Image();
                charCardImg.Height = CARD_HEIGHT;
                charCardImg.Source = characterCard;
                addElementToGrid(charCardImg, VILLAIN_ROW_NUM, 0);

                deckBackRow = VILLAIN_ROW_NUM;
            }
            else if (characterCard == null)//environment
            {
                deckBackRow = ENVIRONMENT_ROW_NUM;
            }
            else//hero
            {
                Image heroCharacterImg = new Image();
                heroCharacterImg.Height = CARD_HEIGHT;
                heroCharacterImg.Source = characterCard;
                addElementToGrid(heroCharacterImg, HERO_ROW_NUM, 0);

                deckBackRow = HERO_ROW_NUM;
            }

            Image deckBackImg = new Image();
            deckBackImg.Height = CARD_HEIGHT;
            deckBackImg.Source = deckBack;
            addElementToGrid(deckBackImg, deckBackRow, DECK_COLUMN);
        }

        private void addElementToGrid(UIElement elem, int row, int col)
        {
            Grid.SetRow(elem, row);
            Grid.SetColumn(elem, col);
            gridLayout.Children.Add(elem);
        }


        private ImageSource getImageSource(string path)
        {
            BitmapImage src = new BitmapImage();
            src.BeginInit();
            src.UriSource = new Uri(path, UriKind.Relative);
            src.EndInit();

            return src;
        }

        private void View_Hand(object sender, RoutedEventArgs e)
        {
            lock(this)
            {
                if (!handViewer.IsVisible)
                {
                    handViewer.Visibility = SHOW;
                    Button handVisibleButton = (Button)sender;
                    handVisibleButton.Content = "Hide Player Hand!";
                }
                else if (handViewer.IsVisible)
                {
                    handViewer.Visibility = HIDE;
                    Button handHiddenButton = (Button)sender;
                    handHiddenButton.Content = "Show Player Hand!";
                }
            }
        }

        private void View_Card_Full(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
