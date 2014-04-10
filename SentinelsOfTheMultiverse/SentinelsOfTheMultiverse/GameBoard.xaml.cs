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
        public List<Card> selectedCards= new List<Card>();
        public List<Card> cardClickArray = new List<Card>();
        public List<Image> imageSelectedArray = new List<Image>();

        private int NEXT_CARD = 3;

        #region Constants

            private readonly string HERO_IMAGE_PATH="Images/Hero/";
            private readonly string VILLAIN_IMAGE_PATH = "Images/Villain/";
            private static readonly string GRAVEYARD_IMAGE_PATH = "Images/Graveyard.png";

            private readonly int VILLAIN_ROW=1;
            private readonly int ENVIRONMENT_ROW=0;
            private readonly int HERO_ROW = 2;    
        
            private readonly double CARD_HEIGHT=200;

            private readonly int DECK_COLUMN=2;
            private readonly int CHARACTER_COLUMN= 0;
            private readonly int INSTRUCTION_COLUMN = 3;
            private readonly int GRAVEYARD_COLUMN= 1;

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

            addShowHandButton();

            addCurrentPlayerLabel();

            List<Hero> heroes = GameEngine.getHeroes();
            Villain villain = GameEngine.getVillain();
            GameEnvironment env = GameEngine.getEnvironment();

            initBoard(heroes, villain, env);
            updatePlayersBoard();

            Content = gridLayout;
        }

        private void addCurrentPlayerLabel()
        {
            Label currentPlayerLabel = new Label();
            currentPlayerLabel.Width = 200;
            currentPlayerLabel.Height = 100;

            int currIndex = GameEngine.playerTurn;
            currentPlayerLabel.Content = currIndex;
            currentPlayerLabel.VerticalAlignment = VerticalAlignment.Top;
            Utility.addElementToGrid(currentPlayerLabel, 0, 0, gridLayout);
        }

        private void addShowHandButton()
        {
            Button showHandButton = new Button();
            showHandButton.Content = "Show Your Hand!";
            showHandButton.Width = 150;
            showHandButton.Height = 50;
            showHandButton.Click += new RoutedEventHandler(View_Hand);
            Utility.addElementToGrid(showHandButton, 0, 0, gridLayout);
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
	                hero.cardsOnField[k].cardImage.MouseDown += new MouseButtonEventHandler(Mouse_Click_Listener);

	                Utility.addElementToGrid(hero.cardsOnField[k].cardImage, HERO_ROW + i, k+4, gridLayout);
	            }
	        }

            for (int k = 0; k < villain.cardsOnField.Count; k++)
            {
                villain.cardsOnField[k].cardImage.Height = CARD_HEIGHT;
                villain.cardsOnField[k].cardImage.MouseDown += new MouseButtonEventHandler(Mouse_Click_Listener);

                Utility.addElementToGrid(villain.cardsOnField[k].cardImage, VILLAIN_ROW, k + 4, gridLayout);
            }

            for (int k = 0; k < env.cardsOnField.Count; k++)
            {
                env.cardsOnField[k].cardImage.Height = CARD_HEIGHT;
                env.cardsOnField[k].cardImage.MouseDown += new MouseButtonEventHandler(Mouse_Click_Listener);

                Utility.addElementToGrid(env.cardsOnField[k].cardImage, ENVIRONMENT_ROW, k + 4, gridLayout);
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

            ImageSource envDeckBackImg = Utility.getImageSource("Images/Environment/"+env.characterName+ "/NonPlayable/"+ "insula_primus_back.png");

            drawNPCBoard(villainImg, villainInstImg, villainDeckBackImg, envDeckBackImg);


            for (int ii = 0; ii < heroes.Count; ii++)
            {
                drawHeroTemplate(heroes[ii], HERO_ROW+ ii);
            }
        }
        
        private void drawHeroTemplate(Hero hero, int currentHeroRow)
        {
            string heroName = hero.getCharacterName();
            ImageSource deckBack = Utility.getImageSource(HERO_IMAGE_PATH + heroName + "/NonPlayable/" + heroName.ToLower() + "_back.png");
            ImageSource characterCard = Utility.getImageSource(HERO_IMAGE_PATH + heroName + "/NonPlayable/" + heroName.ToLower() + "_hero.png");
            
            Image heroCharacterImg = CardImageFromImageSource(characterCard);
            Utility.addElementToGrid(heroCharacterImg, currentHeroRow, CHARACTER_COLUMN, gridLayout);

            Image deckBackImg = CardImageFromImageSource(deckBack);
            Utility.addElementToGrid(deckBackImg, currentHeroRow, DECK_COLUMN, gridLayout);

            ImageSource graveyardSrc;
            if (hero.graveyard.Count == 0)
            {
                graveyardSrc = Utility.getImageSource(GRAVEYARD_IMAGE_PATH);
            }else{
                graveyardSrc = hero.graveyard[hero.graveyard.Count-1].cardImage.Source;
            }
            Image graveYardImg = CardImageFromImageSource(graveyardSrc);
            Utility.addElementToGrid(graveYardImg, currentHeroRow, GRAVEYARD_COLUMN, gridLayout);
        }

        private void drawNPCBoard(ImageSource villainCard, ImageSource villainInstSrc, ImageSource villainDeckSrc, ImageSource envDeckSrc)
        {
            Image villainImage= CardImageFromImageSource(villainCard);
            Utility.addElementToGrid(villainImage, VILLAIN_ROW, CHARACTER_COLUMN, gridLayout);

            Image instructionImage = CardImageFromImageSource(villainInstSrc);
            Utility.addElementToGrid(instructionImage, VILLAIN_ROW, INSTRUCTION_COLUMN, gridLayout);

            Image villainDeckImage = CardImageFromImageSource(villainDeckSrc);
            Utility.addElementToGrid(villainDeckImage, VILLAIN_ROW, DECK_COLUMN, gridLayout);

            Image envDeckImg = CardImageFromImageSource(envDeckSrc);
            Utility.addElementToGrid(envDeckImg, ENVIRONMENT_ROW, DECK_COLUMN, gridLayout);
            
            ImageSource graveyardSrc = Utility.getImageSource(GRAVEYARD_IMAGE_PATH);
            Image envGraveYardImg = CardImageFromImageSource(graveyardSrc);
            Utility.addElementToGrid(envGraveYardImg, ENVIRONMENT_ROW, GRAVEYARD_COLUMN, gridLayout);

            Image villainGraveYardImg = CardImageFromImageSource(graveyardSrc);
            Utility.addElementToGrid(villainGraveYardImg, VILLAIN_ROW, GRAVEYARD_COLUMN, gridLayout);
        }

        private Image CardImageFromImageSource(ImageSource imgSrc)
        {
            Image tempImage = new Image();
            tempImage.Height = CARD_HEIGHT;
            tempImage.MouseDown += new MouseButtonEventHandler(Mouse_Click_Listener);
            tempImage.Source = imgSrc;
            tempImage.Margin = Utility.cardSpacing;
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

        private void Mouse_Click_Listener(object sender, MouseButtonEventArgs e)
        {

            if (e.ClickCount == 2)
            {
                Image expandCard = (Image)sender;

                ViewCard showCard = new ViewCard(expandCard.Source);

                showCard.Show();
            }
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
