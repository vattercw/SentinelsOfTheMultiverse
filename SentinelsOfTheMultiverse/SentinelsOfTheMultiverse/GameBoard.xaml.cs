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
        private HandPanel playerHand = new HandPanel();

        #region Constants

            private string HERO_IMAGE_PATH="Images/Hero/";

        #endregion

        public GameBoard()
        {
            InitializeComponent();
            
            DataContext = this;

            initBoard();
        }

        private void initBoard()
        {
            Button showHandButton = new Button();
            showHandButton.Content = "Show Your Hand!";
            showHandButton.Width = 150;
            showHandButton.Height = 50;
            showHandButton.Click += new RoutedEventHandler(View_Hand);

            Grid.SetRow(showHandButton, 0);
            gridLayout.Children.Add(showHandButton);
			
            List<Hero> heroes= game.getHeroes();
            Villain villain = game.getVillain();
            GameEnvironment env = game.getEnvironment();

            initPlayerBoard(heroes, villain, env);
            
            Content = gridLayout;
        }

        private void initPlayerBoard(List<Hero> heroes, Villain villain, GameEnvironment env)
        {
            foreach (Hero h in heroes)
            {
                Image k = new Image();
                k.Height = 200;
                string heroName= h.getCharacterName();


                k.Source = getImageSource(HERO_IMAGE_PATH + heroName + "/" + heroName.ToLower() + "_hero.png");
                Grid.SetRow(k, 1);
                gridLayout.Children.Add(k);

                //k.Source = getImageSource(HERO_IMAGE_PATH + heroName + "/" + heroName.ToLower() + "_back.png");
                //gridLayout.Children.Add(k);
            }

            RowDefinition row = new RowDefinition ();
            row.Height= GridLength.Auto;

            RowDefinition row2 = new RowDefinition();
            row2.Height = GridLength.Auto;

            gridLayout.RowDefinitions.Add(row);
            gridLayout.RowDefinitions.Add(row2);

            ColumnDefinition col= new ColumnDefinition();
            col.Width = GridLength.Auto;
            gridLayout.ColumnDefinitions.Add(col);
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
            
        }

        private void View_Card_Full(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
