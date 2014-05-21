using System;
using System.Collections.Generic;
using System.Drawing;
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

            List<String> langChoices = new List<string>(){"en-US", "ja-JP"};
            
            localizationChoicesBox.ItemsSource = langChoices;
            localizationChoicesBox.SelectedIndex = 0;
        }

        private void Start_Game(object sender, RoutedEventArgs e)
        {
            
            string selectedValue= (string)localizationChoicesBox.SelectedValue;
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(selectedValue);
            
            //Console.Write("value: " + SentinelsOfTheMultiverse.Properties.Resources.ResourceManager.GetString("Rampage"));
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

            heroesStr.Add(GameEngine.HERO_NAMESPACE + "Haka");
            heroesStr.Add(GameEngine.HERO_NAMESPACE + "Legacy");
            
            Console.WriteLine(heroesStr[1]);

            GameEngine.initPlayers(heroesStr, _villainStr, _envStr);
        }
    }
}
