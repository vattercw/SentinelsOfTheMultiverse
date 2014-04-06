using SentinelsOfTheMultiverse.Data;
using SentinelsOfTheMultiverse;
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
        //private GameEngine game = new GameEngine();
        private Grid gridLayout = new Grid();
        private ViewHand handViewer;

        private Card drawThisCard;

        private int NEXT_CARD = 3;

        #region Constants

            private string HERO_IMAGE_PATH="Images/Hero/";
            private string VILLAIN_IMAGE_PATH = "Images/Villain/";    

            private int VILLAIN_ROW_NUM=1;
            private int ENVIRONMENT_ROW_NUM=0;
            private int HERO_ROW_NUM = 2;    
            private double CARD_HEIGHT=200;
            private int DECK_COLUMN=2;
            private int CHARACTER_COLUMN= 0;
            private int INSTRUCTION_COLUMN = 1;
            


        #endregion

        public GameBoard()
        {
            InitializeComponent();

            updateBoard();

            this.SizeToContent = SizeToContent.WidthAndHeight;

            DataContext = this;

        }

        public void updateBoard()
        {

            gridLayout = initGrid();
            Button showHandButton = new Button();
            showHandButton.Content = "Show Your Hand!";
            showHandButton.Width = 150;
            showHandButton.Height = 50;
            showHandButton.Click += new RoutedEventHandler(View_Hand);

            Grid.SetRow(showHandButton, 0);

            Label currentPlayerLabel = new Label();
            currentPlayerLabel.Width= 200;
            currentPlayerLabel.Height= 100;
            //IPlayer currCharName= GameEngine.getCurrentPlayer();
            
            int currIndex = GameEngine.playerTurn;
            currentPlayerLabel.Content = currIndex;
            currentPlayerLabel.VerticalAlignment = VerticalAlignment.Top;
            
            Utility.addElementToGrid(currentPlayerLabel, 0, 0, gridLayout);

            gridLayout.Children.Add(showHandButton);

            List<Hero> heroes = GameEngine.getHeroes();
            Villain villain = GameEngine.getVillain();
            GameEnvironment env = GameEngine.getEnvironment();

            initBoard(heroes, villain, env);
            updatePlayersBoard();

            Content = gridLayout;
        }

        private void updatePlayersBoard()
	    {
	        Villain villain = GameEngine.getVillain();
	        GameEnvironment env = GameEngine.getEnvironment();
	
            for(int i= 0; i<GameEngine.getHeroes().Count; i++)
	        {
                Hero hero = GameEngine.getHeroes()[i];

	            for(int k = 0; k < hero.cardsOnField.Count; k++){
                    hero.cardsOnField[k].cardImage.Height = CARD_HEIGHT;
	                hero.cardsOnField[k].cardImage.MouseUp += new MouseButtonEventHandler(View_Card_Full);
	
	                Utility.addElementToGrid(hero.cardsOnField[k].cardImage, HERO_ROW_NUM + i, k+4, gridLayout);
	            }
	        }

            for (int k = 0; k < villain.cardsOnField.Count; k++)
            {
                villain.cardsOnField[k].cardImage.Height = CARD_HEIGHT;
                villain.cardsOnField[k].cardImage.MouseUp += new MouseButtonEventHandler(View_Card_Full);

                Utility.addElementToGrid(villain.cardsOnField[k].cardImage, VILLAIN_ROW_NUM, k + 4, gridLayout);
            }

            for (int k = 0; k < env.cardsOnField.Count; k++)
            {
                env.cardsOnField[k].cardImage.Height = CARD_HEIGHT;
                env.cardsOnField[k].cardImage.MouseUp += new MouseButtonEventHandler(View_Card_Full);

                Utility.addElementToGrid(env.cardsOnField[k].cardImage, ENVIRONMENT_ROW_NUM, k + 4, gridLayout);
            }
	    }


        public void initHandViewer()
        {
            Hero currentPlayer= (Hero)GameEngine.getCurrentPlayer();
            if (currentPlayer != null)
            {
                handViewer = new ViewHand(currentPlayer.getPlayerHand(), this);
            }
		}
		
        private Grid initGrid()
        {
            gridLayout.Children.RemoveRange(0, gridLayout.Children.Count);
            Grid myGrid = new Grid();

            for (int ll = 0; ll < GameEngine.getHeroes().Count+2; ll++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = GridLength.Auto;
                myGrid.RowDefinitions.Add(row);
            }

            for (int kk = 0; kk < 10; kk++)
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
            
            ImageSource villainImg = Utility.getImageSource(VILLAIN_IMAGE_PATH + villainName + "/NonPlayable/" + villainName + "_initial.png");
            ImageSource villainDeckBackImg = Utility.getImageSource(VILLAIN_IMAGE_PATH + villainName + "/NonPlayable/" + villainName + "_back.png");
            ImageSource villainInstImg = Utility.getImageSource(VILLAIN_IMAGE_PATH + villainName + "/NonPlayable/" + villainName + "_instr_front.png");

            ImageSource envDeckBackImg = Utility.getImageSource("Images/Environment/"+env.getCharacterName()+ "/NonPlayable/"+ "insula_primus_back.png");

            drawNPCBoard(villainImg, villainInstImg, villainDeckBackImg, envDeckBackImg);


            for (int ii = 0; ii < heroes.Count; ii++)
            {
                string heroName = heroes[ii].getCharacterName();

                ImageSource heroDeckBackImg = Utility.getImageSource(HERO_IMAGE_PATH + heroName + "/NonPlayable/" + heroName.ToLower() + "_back.png");
                ImageSource heroImg = Utility.getImageSource(HERO_IMAGE_PATH + heroName + "/NonPlayable/" + heroName.ToLower() + "_hero.png");
                drawHeroTemplate(heroDeckBackImg, heroImg, ii);
            }
        }
        

        //TODO: later add ImageSource graveYardTop
        private void drawHeroTemplate(ImageSource deckBack, ImageSource characterCard, int heroIndex)
        {
            int currentHeroRow = HERO_ROW_NUM + heroIndex;
            Image heroCharacterImg = CardImageFromImageSource(characterCard);
            Utility.addElementToGrid(heroCharacterImg, currentHeroRow, CHARACTER_COLUMN, gridLayout);

            Image deckBackImg = CardImageFromImageSource(deckBack);
            Utility.addElementToGrid(deckBackImg, currentHeroRow, DECK_COLUMN, gridLayout);
        }

        private void drawNPCBoard(ImageSource villainCard, ImageSource villainInstSrc, ImageSource villainDeckSrc, ImageSource envDeckSrc)
        {
            Image villainImage= CardImageFromImageSource(villainCard);
            Utility.addElementToGrid(villainImage, VILLAIN_ROW_NUM, CHARACTER_COLUMN, gridLayout);

            Image instructionImage = CardImageFromImageSource(villainInstSrc);
            Utility.addElementToGrid(instructionImage, VILLAIN_ROW_NUM, INSTRUCTION_COLUMN, gridLayout);

            Image villainDeckImage = CardImageFromImageSource(villainDeckSrc);
            Utility.addElementToGrid(villainDeckImage, VILLAIN_ROW_NUM, DECK_COLUMN, gridLayout);

            Image envDeckImg = CardImageFromImageSource(envDeckSrc);
            Utility.addElementToGrid(envDeckImg, ENVIRONMENT_ROW_NUM, DECK_COLUMN, gridLayout);

        }

        private Image CardImageFromImageSource(ImageSource imgSrc)
        {
            Image tempImage = new Image();
            tempImage.Height = CARD_HEIGHT;
            tempImage.MouseUp += new MouseButtonEventHandler(View_Card_Full);
            tempImage.Source = imgSrc;
            return tempImage;
        }

        private int getNextCard()
        {
            NEXT_CARD++;
            return NEXT_CARD - 1;
        }

        private void View_Hand(object sender, RoutedEventArgs e)
        {
            initHandViewer();
            handViewer.Visibility = Utility.SHOW;
            Button handVisibleButton = (Button)sender;         
        }

        private void View_Card_Full(object sender, MouseButtonEventArgs e)
        {
            Image expandCard = (Image)sender;

            ViewCard showCard = new ViewCard(expandCard.Source);

            showCard.Show();
        }

        internal void drawCardSelected(Card cardClicked)
        {
            Hero hero = (Hero)GameEngine.getCurrentPlayer();
            for(int i = 0; i < hero.getPlayerHand().Count; i++){
                if (hero.getPlayerHand()[i].Equals(cardClicked))
                {
                    drawThisCard = hero.getPlayerHand()[i];
                    hero.getPlayerHand().RemoveAt(i);
                    break;
                }

            }
            ((Hero)GameEngine.getCurrentPlayer()).cardsOnField.Add(drawThisCard);
        }
    }
}
