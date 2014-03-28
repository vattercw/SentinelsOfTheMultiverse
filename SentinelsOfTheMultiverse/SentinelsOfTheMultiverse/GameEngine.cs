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
        public List<Hero> heroes = new List<Hero>();
        public Villain villain;
        public GameEnvironment environment;
        public List<Hero> _heroes = new List<Hero>();

        private int playerTurn = 0;
        Image villainImage = new Image();

        public static int MAXPLAYER = 6;
        public static int VILLIANNUM = 0;
        public static int EVIRONMENTNUM = 7;

        public GameEngine()
        {
            //begin();
        }



        public void begin()
        {

            List<String> playerNames = this.startScreen.begin();

            this.villain = new BaronBlade();
            Hero h1 = new Haka();
            Hero h2 = new Haka();

            heroes = new List<Hero>() { h1, h2 };

            environment = new InsulaPrimus();


            for (int i = 1; i <= MAXPLAYER; i++)
            {
                Hero newHero = (Hero)System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(playerNames[i]);
                this.heroes.Add(newHero);
            }
            this.villain = (Villain)System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(playerNames[VILLIANNUM]);
            this.environment = (GameEnvironment)System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(playerNames[EVIRONMENTNUM]);


        }

        public void newTurn()
        {

            this.players[playerTurn].playerTurn();
            this.playerTurn++;
            if (this.playerTurn == this.players.Count)
            {
                this.playerTurn = 0;
            }
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
