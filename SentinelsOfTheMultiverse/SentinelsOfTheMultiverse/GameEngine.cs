using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using SentinelsOfTheMultiverse.Data;
using System.Windows;
using System.Windows.Media;
using SentinelsOfTheMultiverse.Data.Villains;
using SentinelsOfTheMultiverse.Data.Heroes;

namespace SentinelsOfTheMultiverse
{
    class GameEngine
    {

        public Startup startScreen = new Startup();
        public List<IPlayer> players = new List<IPlayer>();
        public Villain villain;
        public GameEnvironment environment;
        public List<Hero> _heroes = new List<Hero>();

        private int playerTurn = 0;
        Image villainImage = new Image();

        public static int MAXPLAYER = 2;
        public static int VILLIANNUM = 0;
        public static int EVIRONMENTNUM = 3;

        public GameEngine()
        {
            begin();
        }
        public void begin()
        {

            List<String> playerNames = this.startScreen.begin();

            this.villain = new BaronBlade();
            Hero h1 = new Haka();
            Hero h2 = new Haka();

            _heroes = new List<Hero>() { h1, h2 };

            environment = new InsulaPrimus();


            for (int i = 1; i <= MAXPLAYER; i++)
            {
                Hero newHero = (Hero)System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(playerNames[i]);
                _heroes.Add(newHero);
            }
            villain = (Villain)System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(playerNames[VILLIANNUM]);
            environment = (GameEnvironment)System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(playerNames[EVIRONMENTNUM]);

        }

        public void newTurn()
        {

            players[playerTurn].playerTurn();
            playerTurn++;
            if (playerTurn == players.Count)
            {
                playerTurn = 0;
            }
        }
        public int getPlayerTurn()
        {
            return playerTurn;
        }

        public Hero getCurrentPlayer()
        {
            return _heroes[playerTurn];
        }

        internal List<Hero> getHeroes()
        {
            return _heroes;
        }

        internal Villain getVillain()
        {
            return villain;
        }

        internal GameEnvironment getEnvironment()
        {
            return environment;
        }
    }
}
