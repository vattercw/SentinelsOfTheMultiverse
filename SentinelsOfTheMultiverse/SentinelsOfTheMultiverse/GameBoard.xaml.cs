﻿using SentinelsOfTheMultiverse.Data;
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
            initBoard();

            DataContext = this;

        }

        public void initBoard()
        {

            gridLayout = initGrid();
            Button showHandButton = new Button();
            showHandButton.Content = "Show Your Hand!";
            showHandButton.Width = 150;
            showHandButton.Height = 50;
            showHandButton.Click += new RoutedEventHandler(View_Hand);

            Grid.SetRow(showHandButton, 6);

            Label currentPlayerLabel = new Label();
            currentPlayerLabel.Width= 200;
            currentPlayerLabel.Height= 100;
            //IPlayer currCharName= GameEngine.getCurrentPlayer();
            
            int currIndex = GameEngine.playerTurn;
            currentPlayerLabel.Content = currIndex;
            currentPlayerLabel.VerticalAlignment = VerticalAlignment.Top;
            
            Utility.addElementToGrid(currentPlayerLabel, 0, 0, gridLayout);

            initHandViewer();
            gridLayout.Children.Add(showHandButton);

            List<Hero> heroes = GameEngine.getHeroes();
            Villain villain = GameEngine.getVillain();
            GameEnvironment env = GameEngine.getEnvironment();

            initBoard(heroes, villain, env);

            Content = gridLayout;
        }

        private void initHandViewer()
        {

            Hero currentPlayer= (Hero)GameEngine.getCurrentPlayer();
            if (currentPlayer != null)
            {
                handViewer = new ViewHand(currentPlayer.getPlayerHand(), this);
            }

		}
		
        private Grid initGrid()
        {
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
            initPlayerTemplate(villainDeckBackImg, villainImg, villainInstImg);

            ImageSource envDeckBackImg = Utility.getImageSource("Images/Environment/InsulaPrimus/NonPlayable/insula_primus_back.png");

            initPlayerTemplate(envDeckBackImg);


            for (int ii = 0; ii < heroes.Count; ii++)
            {
                string heroName = heroes[ii].getCharacterName();

                ImageSource heroDeckBackImg = Utility.getImageSource(HERO_IMAGE_PATH + heroName + "/NonPlayable/" + heroName.ToLower() + "_back.png");
                ImageSource heroImg = Utility.getImageSource(HERO_IMAGE_PATH + heroName + "/NonPlayable/" + heroName.ToLower() + "_hero.png");
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

                charInst.MouseUp += new MouseButtonEventHandler(View_Card_Full);
                Utility.addElementToGrid(charInst, VILLAIN_ROW_NUM, 1, gridLayout);

                Image charCardImg = new Image();
                charCardImg.Height = CARD_HEIGHT;
                charCardImg.Source = characterCard;
                charCardImg.MouseUp += new MouseButtonEventHandler(View_Card_Full);
                Utility.addElementToGrid(charCardImg, VILLAIN_ROW_NUM, 0, gridLayout);

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
                heroCharacterImg.MouseUp += new MouseButtonEventHandler(View_Card_Full);
                Utility.addElementToGrid(heroCharacterImg, HERO_ROW_NUM, 0, gridLayout);

                deckBackRow = HERO_ROW_NUM;
            }

            Image deckBackImg = new Image();
            deckBackImg.Height = CARD_HEIGHT;
            deckBackImg.Source = deckBack;
            deckBackImg.MouseUp += new MouseButtonEventHandler(View_Card_Full);
            Utility.addElementToGrid(deckBackImg, deckBackRow, DECK_COLUMN, gridLayout);
        }

        private int getNextCard()
        {
            NEXT_CARD++;
            return NEXT_CARD - 1;
        }

        private void View_Hand(object sender, RoutedEventArgs e)
        {
            lock (this)
            {
                handViewer.Visibility = SHOW;
                Button handVisibleButton = (Button)sender;
            }
              
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
            drawThisCard.cardImage.Height = CARD_HEIGHT;
            drawThisCard.cardImage.MouseUp += new MouseButtonEventHandler(View_Card_Full);
            Utility.addElementToGrid(drawThisCard.cardImage,HERO_ROW_NUM, getNextCard(), gridLayout);
            initHandViewer();
        }
    }
}
