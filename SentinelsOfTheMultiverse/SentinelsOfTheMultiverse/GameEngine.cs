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
        public List<Hero> heroes = new List<Hero>();
        public Villain villain;
        public static GameEnvironment environment;

        private static int playerTurn = 0;
        Image villainImage = new Image();

        public static int MAXPLAYER = 6;
        public static int VILLIANNUM = 0;
        public static int EVIRONMENTNUM = 7;
        public static string HERO_NAMESPACE = "SentinelsOfTheMultiverse.Data.Heroes.";
        public static string VILLAIN_NAMESPACE = "SentinelsOfTheMultiverse.Data.Villains.";
        public static string ENVIRONMENT_NAMESPACE = "SentinelsOfTheMultiverse.Data.Environments.";

        public GameEngine()
        {
            startScreen.begin();
            
            initPlayers();
            //newTurn();

            var cp = getPlayers()[0];

            //while (getWinCon())
            for(int ii=0;ii<5;ii++)
            {
                cp.playerTurn();
                nextTurn();
            }
        }



        //private bool getWinCon()
        //{
        //    return villain.getLifeTotal() <= 0;
        //}

        public void initPlayers()
        {
            var heroesStr= startScreen.getHeroesString();
            var villainStr = startScreen.getVillainStr();
            var envStr = startScreen.getEnvironmentString();
            
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

        public void nextTurn()
        {
            
            getPlayers()[playerTurn].playerTurn();

            playerTurn++;
            if (playerTurn == getPlayers().Count)
            {
                playerTurn = 0;
            }
        }

        #region GET SET REGION
        public int getPlayerTurn()
        {
            return playerTurn;
        }

        public IPlayer getCurrentPlayer()
        {
            return getPlayers()[playerTurn];
        }

        internal List<IPlayer> getPlayers()
        {
            var list = new List<IPlayer>();
            list.AddRange(heroes);
            list.Add(villain);
            list.Add(environment);
            return list;
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
