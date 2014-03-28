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
            private System.Windows.Visibility SHOW = Visibility.Visible;
            private System.Windows.Visibility HIDE = Visibility.Hidden;

        #endregion

        public GameBoard()
        {
            InitializeComponent();
            
            DataContext = this;

            initBoard();
        }

        private void initBoard()
        {

            gridLayout = initGrid();
            Button showHandButton = new Button();
            showHandButton.Content = "Show Your Hand!";
            showHandButton.Width = 150;
            showHandButton.Height = 50;
            showHandButton.Click += new RoutedEventHandler(View_Hand);

            Grid.SetRow(showHandButton, 0);

            initHandViewer();
            gridLayout.Children.Add(showHandButton);
			
            List<Hero> heroes= game.getHeroes();
            Villain villain = game.getVillain();
            GameEnvironment env = game.getEnvironment();

            initPlayerBoard(heroes, villain, env);
            
            Content = gridLayout;

        }

        private void initHandViewer()
        {
            handViewer = new ViewHand(game.getCurrentPlayer().getPlayerHand());
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

        private void initPlayerBoard(List<Hero> heroes, Villain villain, GameEnvironment env)
        {
            
            for (int ii=0;ii<heroes.Count;ii++)
            {
                Image k = new Image();
                k.Height = 200;
                string heroName= heroes[ii].getCharacterName();


                k.Source = getImageSource(HERO_IMAGE_PATH + heroName + "/" + heroName.ToLower() + "_hero.png");
                addElementToGrid(k, ii+1, 0);
                gridLayout.Children.Add(k);

                //k.Source = getImageSource(HERO_IMAGE_PATH + heroName + "/" + heroName.ToLower() + "_back.png");
                //gridLayout.Children.Add(k);
            }
        }

        private void addElementToGrid(UIElement elem, int row, int col)
        {
            Grid.SetRow(elem, row);
            Grid.SetColumn(elem, col);
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

        private void View_Card_Full(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
