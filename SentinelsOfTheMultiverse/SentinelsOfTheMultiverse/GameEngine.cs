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

        private int playerTurn = 0;
        Image villainImage = new Image();

        public static int MAXPLAYER = 6;
        public static int VILLIANNUM = 0;
        public static int EVIRONMENTNUM = 7;
        public string HERO_NAMESPACE = "SentinelsOfTheMultiverse.Data.Heroes.";
        public string VILLAIN_NAMESPACE = "SentinelsOfTheMultiverse.Data.Villains.";
        public string ENVIRONMENT_NAMESPACE = "SentinelsOfTheMultiverse.Data.Environments.";

        public GameEngine()
        {
            initPlayers();
            //newTurn();
        }

        public void initPlayers()
        {
            List<string> heroesStr = new List<string>() {HERO_NAMESPACE+"Haka", HERO_NAMESPACE+"Haka" };
            string villainStr= VILLAIN_NAMESPACE+"BaronBlade";
            string envStr = ENVIRONMENT_NAMESPACE+"InsulaPrimus";
            
            for (int i = 0; i < heroesStr.Count; i++)
            {
                Hero newHero = (Hero)getClassFromString(heroesStr[i]);
                heroes.Add(newHero);
            }
            villain = (Villain)getClassFromString(villainStr);
            environment = (GameEnvironment)getClassFromString(envStr);

            List<IPlayer> playerNames = new List<IPlayer>();
            playerNames.AddRange(heroes);
            playerNames.Add(villain);
            playerNames.Add(environment);
        }

        public Object getClassFromString(string className)
        {
            Type hai = Type.GetType(className, true);
            Object o = (Activator.CreateInstance(hai));

            return o;
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

        #region GET SET REGION
        public int getPlayerTurn()
        {
            return playerTurn;
        }

        public Hero getCurrentPlayer()
        {
            return heroes[playerTurn];
        }

        internal List<IPlayer> getPlayers()
        {
            return players;
        }

        internal List<Hero> getHeroes()
        {
            return heroes;
        }

        internal Villain getVillain()
        {
            return villain;
        }

        internal GameEnvironment getEnvironment()
        {
            return environment;
        }
        #endregion
    }
}
