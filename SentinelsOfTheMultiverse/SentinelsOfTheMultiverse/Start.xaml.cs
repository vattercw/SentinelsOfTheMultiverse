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
    public partial class Start : Window
    {
        public Start()
        {
            InitializeComponent();

            Button startButton = new Button();
            startButton.Content = "Start!";
            startButton.Click += new RoutedEventHandler(Start_Game);
            Grid myGrid = new Grid();
            Content = startButton;
        }

        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        private void Start_Game(object sender, RoutedEventArgs e)
        {
            beginGame();
            GameBoard game = new GameBoard();
            game.Show();
            this.Close();
        }

        public void beginGame()
        {
            List<string> heroesStr = new List<string>();
            string _villainStr = GameEngine.VILLAIN_NAMESPACE + "BaronBlade";
            string _envStr = GameEngine.ENVIRONMENT_NAMESPACE + "InsulaPrimus";

            //heroesStr.Add(GameEngine.HERO_NAMESPACE + "Haka");
            heroesStr.Add(GameEngine.HERO_NAMESPACE + "Haka");

            GameEngine.initPlayers(heroesStr, _villainStr, _envStr);
        }
    }
}
