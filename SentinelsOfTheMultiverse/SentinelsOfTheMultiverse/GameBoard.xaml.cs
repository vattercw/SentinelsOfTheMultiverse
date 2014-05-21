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
using System.Windows.Media.Effects;
using SentinelsOfTheMultiverse.Data.Effects;

namespace SentinelsOfTheMultiverse
{
    public partial class GameBoard : Window
    {
        //private GameEngine game = new GameEngine();
        private Grid gridLayout = new Grid();
        private ViewHand handViewer;
        private DiscardFromBoard boardViewer;

        private Card drawThisCard;
        public static List<Card> discardedCardsThisTurn = new List<Card>();
        public static List<Card> cardClickedArray = new List<Card>();

        private int NEXT_CARD = 3;

        #region Constants

            private readonly string HERO_IMAGE_PATH="Images/Hero/";
            private readonly string VILLAIN_IMAGE_PATH = "Images/Villain/";
            private static readonly string GRAVEYARD_IMAGE_PATH = "Images/Graveyard.png";

            private readonly int VILLAIN_ROW=1;
            private readonly int ENVIRONMENT_ROW=0;
            private readonly int HERO_ROW = 2;    

            private readonly int DECK_COLUMN=3;
            private readonly int CHARACTER_COLUMN= 1;
            private readonly int INSTRUCTION_COLUMN = 4;
            private readonly int GRAVEYARD_COLUMN= 2;

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
            initButtons();
            addCurrentPlayerLabel();

            List<Hero> heroes = GameEngine.getHeroes();
            Villain villain = GameEngine.getVillain();
            GameEnvironment env = GameEngine.getEnvironment();

            initBoard(heroes, villain, env);
            updatePlayersBoard();

            Content = gridLayout;
        }

        #region LabelsAndButtons
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

        private void addHealthLabel(IPlayer playerRujisMom, int row)
        {
            Label playerHealthLabel = new Label();
            playerHealthLabel.Width = 50;
            playerHealthLabel.Height = 40;

            playerHealthLabel.Content = playerRujisMom.lifeTotal;
            Utility.addElementToGrid(playerHealthLabel, row, 0, gridLayout);
        }

        private void initButtons()
        {


            Button cancelSelectionButton = new Button();
            cancelSelectionButton.Content = "Cancel Selection";
            cancelSelectionButton.Width = 150;
            cancelSelectionButton.Height = 50;
            cancelSelectionButton.Click += new RoutedEventHandler(Clear_Selection);
            Utility.addElementToGrid(cancelSelectionButton, 4, 2, gridLayout);

            Button discardButton = new Button();
            discardButton.Content = "Discard From Field";
            discardButton.Width = 150;
            discardButton.Height = 50;
            discardButton.Click += new RoutedEventHandler(Discard_Action);
            Utility.addElementToGrid(discardButton, 4, 3, gridLayout);

            var currentPlayer = GameEngine.getCurrentPlayer();
            if(typeof(Hero).IsAssignableFrom(currentPlayer.GetType())){
                Button showHandButton = new Button();
                showHandButton.Content = "Show Your Hand!";
                showHandButton.Width = 150;
                showHandButton.Height = 50;
                showHandButton.Click += new RoutedEventHandler(View_Hand);
                Utility.addElementToGrid(showHandButton, 4, 0, gridLayout);
            }else{
            //if (currentPlayer.GetType().Equals(typeof(Data.Villains.BaronBlade)) || currentPlayer.GetType().Equals(typeof(Data.Environments.InsulaPrimus))) {
                Button playEnvVilButton = new Button();
                playEnvVilButton.Content = "Play " + GameEngine.getCurrentPlayer().characterName + "Turn";
                playEnvVilButton.Width = 150;
                playEnvVilButton.Height = 50;
                playEnvVilButton.Click += new RoutedEventHandler(PlayEnvVil_Action);
                Utility.addElementToGrid(playEnvVilButton, 4, 0, gridLayout);
            }
            
        }

        private void PlayEnvVil_Action(object sender, RoutedEventArgs e) {
            var currentPlayer = GameEngine.getCurrentPlayer();
            
            List<Card> drawnCards = currentPlayer.drawPhase(1);
            for (int i = 0; i < drawnCards.Count; i++) {
                var res = currentPlayer.CardMethod(drawnCards[i]);
                if (res != null)
                {
                    switch ((GameEngine.ForcedEffect)res[1]) {
                        case GameEngine.ForcedEffect.RiverOfLava:
                            break;
                        case GameEngine.ForcedEffect.PrimordialPlant:
                            foreach (Hero hero in GameEngine.getHeroes()) {
                                GameBoard.discardedCardsThisTurn = new List<Card>();
                                //TODO Discard from board doesn't work
                                //DiscardFromBoard handView = new DiscardFromBoard(this);
                                //handView.Visibility = System.Windows.Visibility.Visible;
                                //handView.ShowDialog();
                                discardedCardsThisTurn = new List<Card>();

                                Data.Environments.InsulaPrimus.DiscardedAction discardAction = (Data.Environments.InsulaPrimus.DiscardedAction)res[0];
                                discardAction(GameBoard.discardedCardsThisTurn.Count, hero, (Card)res[3]);
                            }
                            drawnCards[0].SendToGraveyard(currentPlayer, currentPlayer.cardsOnField);
                            break;
                    }
                }
            }
            currentPlayer.endPhase();
            GameEngine.nextTurn();
            updateBoard();
        }

        #endregion

        private void updatePlayersBoard()
	    {
	        Villain villain = GameEngine.getVillain();
	        GameEnvironment env = GameEngine.getEnvironment();
	
            for(int i= 0; i<GameEngine.getHeroes().Count; i++)
	        {
                Hero hero = GameEngine.getHeroes()[i];

	            for(int k = 0; k < hero.cardsOnField.Count; k++){
	                hero.cardsOnField[k].MouseDown += new MouseButtonEventHandler(Mouse_Click_Listener);
                    hero.cardsOnField[k].Margin = Utility.cardSpacing;

	                Utility.addElementToGrid(hero.cardsOnField[k], HERO_ROW + i, k+4, gridLayout);
	            }
	        }

            for (int k = 0; k < villain.cardsOnField.Count; k++)
            {
                villain.cardsOnField[k].Margin = Utility.cardSpacing;
                villain.cardsOnField[k].MouseDown += new MouseButtonEventHandler(Mouse_Click_Listener);

                Utility.addElementToGrid(villain.cardsOnField[k], VILLAIN_ROW, k + 5, gridLayout);
            }

            for (int k = 0; k < env.cardsOnField.Count; k++)
            {
                env.cardsOnField[k].Margin = Utility.cardSpacing;
                env.cardsOnField[k].MouseDown += new MouseButtonEventHandler(Mouse_Click_Listener);

                Utility.addElementToGrid(env.cardsOnField[k], ENVIRONMENT_ROW, k + 4, gridLayout);
            }
	    }


        #region HandViewer
        private void View_Hand(object sender, RoutedEventArgs e)
        {
            var currentPlayer = GameEngine.getCurrentPlayer();
            
            initHandViewer((Hero)currentPlayer);
            handViewer.Visibility = Utility.SHOW;
            //Button handVisibleButton = (Button)sender;
        }

        public void initHandViewer(Hero currentPlayer)
        {
            if (currentPlayer != null)
            {
                handViewer = new ViewHand(currentPlayer.getPlayerHand(), this);
            }
		}

        internal void drawCardSelected(Card cardClicked)
        {
            Hero hero = (Hero)GameEngine.getCurrentPlayer();
            for (int i = 0; i < hero.getPlayerHand().Count; i++)
            {
                if (hero.getPlayerHand()[i].Equals(cardClicked))
                {
                    drawThisCard = hero.getPlayerHand()[i];
                    hero.getPlayerHand().RemoveAt(i);
                    break;
                }
            }
            ((Hero)GameEngine.getCurrentPlayer()).cardsOnField.Add(drawThisCard);
        }
        #endregion

        private Grid initGrid()
        {
            removeHandlers();
            gridLayout.Children.RemoveRange(0, gridLayout.Children.Count);
            Grid myGrid = new Grid();

            for (int ll = 0; ll < GameEngine.getHeroes().Count+3; ll++)
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
            drawNPCBoard(villain, env);

            for (int ii = 0; ii < heroes.Count; ii++)
            {
                drawHeroTemplate(heroes[ii], HERO_ROW+ ii);
            }
        }
        
        private void drawHeroTemplate(Hero hero, int currentHeroRow)
        {
            string heroName = hero.getCharacterName();
            Card deckBack =new Card(HERO_IMAGE_PATH + heroName + "/NonPlayable/" + heroName.ToLower() + "_back.png");
            deckBack.MouseDown += new MouseButtonEventHandler(Mouse_Click_Listener);
            Card characterCard = new Card(HERO_IMAGE_PATH + heroName + "/NonPlayable/" + heroName.ToLower() + "_hero.png");
            characterCard.MouseDown += new MouseButtonEventHandler(Mouse_Click_Listener);
            
            Utility.addElementToGrid(characterCard, currentHeroRow, CHARACTER_COLUMN, gridLayout);
            Utility.addElementToGrid(deckBack, currentHeroRow, DECK_COLUMN, gridLayout);

            addHealthLabel(hero, currentHeroRow);

            //TODO: Graveyard not sure if this hsould be the card or the image changed.
            Card graveYardImg = new Card(GRAVEYARD_IMAGE_PATH);
            if (hero.graveyard.Count == 0)
            {
                graveYardImg.Source = Utility.getImageSource(GRAVEYARD_IMAGE_PATH);
            }else{
                graveYardImg.Source = hero.graveyard[hero.graveyard.Count - 1].Source;
            }
            graveYardImg.MouseDown += new MouseButtonEventHandler(Mouse_Click_Listener);
            Utility.addElementToGrid(graveYardImg, currentHeroRow, GRAVEYARD_COLUMN, gridLayout);
        }

        private void drawNPCBoard(Villain villain, GameEnvironment env)
        {
            string villainName = villain.getCharacterName();
            Card villainCard = new Card(VILLAIN_IMAGE_PATH + villainName + "/NonPlayable/" + villainName + "_initial.png");
            villainCard.MouseDown += new MouseButtonEventHandler(Mouse_Click_Listener);
            Card villainDeck = new Card(VILLAIN_IMAGE_PATH + villainName + "/NonPlayable/" + villainName + "_back.png");
            villainDeck.MouseDown += new MouseButtonEventHandler(Mouse_Click_Listener);
            Card villainInst = new Card(VILLAIN_IMAGE_PATH + villainName + "/NonPlayable/" + villainName + "_instr_front.png");
            villainInst.MouseDown += new MouseButtonEventHandler(Mouse_Click_Listener);
            Card envDeck = new Card("Images/Environment/" + env.characterName + "/NonPlayable/" + "insula_primus_back.png");
            envDeck.MouseDown += new MouseButtonEventHandler(Mouse_Click_Listener);

            Utility.addElementToGrid(villainCard, VILLAIN_ROW, CHARACTER_COLUMN, gridLayout);
            Utility.addElementToGrid(villainInst, VILLAIN_ROW, INSTRUCTION_COLUMN, gridLayout);
            Utility.addElementToGrid(villainDeck, VILLAIN_ROW, DECK_COLUMN, gridLayout);
            Utility.addElementToGrid(envDeck, ENVIRONMENT_ROW, DECK_COLUMN, gridLayout);

            addHealthLabel(villain, VILLAIN_ROW);

            Card envGraveImg = new Card(GRAVEYARD_IMAGE_PATH);
            if (env.graveyard.Count == 0) {
                envGraveImg.Source = Utility.getImageSource(GRAVEYARD_IMAGE_PATH);
            } else {
                envGraveImg.Source = env.graveyard[env.graveyard.Count - 1].Source;
            }
            envGraveImg.MouseDown += new MouseButtonEventHandler(Mouse_Click_Listener);
            Utility.addElementToGrid(envGraveImg, ENVIRONMENT_ROW, GRAVEYARD_COLUMN, gridLayout);


            Card vilGraveImg = new Card(GRAVEYARD_IMAGE_PATH);
            if (villain.graveyard.Count == 0) {
                vilGraveImg.Source = Utility.getImageSource(GRAVEYARD_IMAGE_PATH);
            } else {
                vilGraveImg.Source = villain.graveyard[villain.graveyard.Count - 1].Source;
            }
            vilGraveImg.MouseDown += new MouseButtonEventHandler(Mouse_Click_Listener);
            Utility.addElementToGrid(vilGraveImg, VILLAIN_ROW, GRAVEYARD_COLUMN, gridLayout);
        }

  
        private int getNextCard()
        {
            NEXT_CARD++;
            return NEXT_CARD - 1;
        }

        private void Mouse_Click_Listener(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                Image expandCard = (Image)sender;

                ViewCard showCard = new ViewCard(expandCard.Source, this);
                showCard.ShowDialog();
            }
            else if (e.ClickCount == 1)
            {
                Card_Selection_Handler(sender, e);
            }
        }

        public void BreakDown()
        {
            drawThisCard = null;
            discardedCardsThisTurn = null;
            cardClickedArray = null;
            gridLayout = null;
            handViewer = null;
        }
        #region Handlers
        public void removeHandlers()
        {
            Villain villain = GameEngine.getVillain();
            GameEnvironment env = GameEngine.getEnvironment();

            for (int i = 0; i < GameEngine.getHeroes().Count; i++)
            {
                Hero hero = GameEngine.getHeroes()[i];

                for (int k = 0; k < hero.cardsOnField.Count; k++)
                {
                    hero.cardsOnField[k].MouseDown -= new MouseButtonEventHandler(Mouse_Click_Listener);
                }
            }

            for (int k = 0; k < villain.cardsOnField.Count; k++)
            {
                villain.cardsOnField[k].MouseDown -= new MouseButtonEventHandler(Mouse_Click_Listener);
            }

            for (int k = 0; k < env.cardsOnField.Count; k++)
            {
                env.cardsOnField[k].MouseDown -= new MouseButtonEventHandler(Mouse_Click_Listener);
            }
        }
        public void Clear_Selection(object sender, RoutedEventArgs e)
        {
            foreach (Card clearCard in cardClickedArray)
            {
                clearCard.Effect = null;
            }
            cardClickedArray.Clear();
        }

        public void Card_Selection_Handler(object sender, RoutedEventArgs e)
        {
            Villain villain = GameEngine.getVillain();
            GameEnvironment env = GameEngine.getEnvironment();
            Boolean canSelectHero = false;
            Boolean canSelectVillain = false;
            Boolean canSelectEnv = false;

            var cardClicked = (Card)sender;

            foreach (Hero hero in GameEngine.getHeroes())
            {
                if(hero.cardsOnField.Contains(cardClicked)) canSelectHero = true;
            }
            if (env.cardsOnField.Contains(cardClicked)) canSelectEnv = true;
            if (villain.cardsOnField.Contains(cardClicked)) canSelectVillain = true;

            if (canSelectHero || canSelectEnv || canSelectVillain)
            {
                if (!cardClickedArray.Contains(cardClicked))
                {
                    if (env.cardsOnField.Contains(cardClicked)) cardClicked.Effect = Utility.selectionGlowEnvironment();
                    else if (villain.cardsOnField.Contains(cardClicked)) cardClicked.Effect = Utility.selectionGlowVillain();
                    else cardClicked.Effect = Utility.selectionGlowHero();
                    cardClickedArray.Add(cardClicked);
                }
                else
                {
                    cardClicked.Effect = null;
                    cardClickedArray.Remove(cardClicked);
                }
            }
        }

        private void Discard_Action(object sender, RoutedEventArgs e)
        {
            var currentPlayer = GameEngine.getCurrentPlayer();

            foreach (Card toDiscard in cardClickedArray){
                if(currentPlayer.cardsOnField.Contains(toDiscard)) toDiscard.SendToGraveyard(currentPlayer, currentPlayer.cardsOnField);
            }
            cardClickedArray.Clear();
            updateBoard();
        }

        public List<Card> getCardSelection()
        {
            return cardClickedArray;
        }

        public void clearCardSelection()
        {
            cardClickedArray.Clear();
        }
        #endregion

        public static void WinCondition()
        {
            Window win = new Window();
            win.Title = "You win!";
            win.Activate();
        }

        internal static void LoseCondition()
        {
            Window lose = new Window();
            lose.Title = "You lose!";
            lose.Activate();
        }
        public void GameBoardTeardown()
        {
            gridLayout = null;
            handViewer = null;
            discardedCardsThisTurn = null;
            cardClickedArray = null;
        }
    }
}
