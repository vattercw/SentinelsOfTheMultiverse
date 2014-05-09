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
    static class GameEngine
    {
        
        static List<Hero> heroes = new List<Hero>();
        static Villain villain;
        static GameEnvironment environment;

        static public int playerTurn = 0;
        public static bool isFirstTurn = true;

        //variables for specific card states
        public static bool volcanoInPlay = false;
        public static bool obsidianInPlay = false;
        public static bool lavaInPlay = false;

        public static int MAXPLAYER = 6;
        public static int VILLIANNUM = 0;
        public static int EVIRONMENTNUM = 7;
        public static string HERO_NAMESPACE = "SentinelsOfTheMultiverse.Data.Heroes.";
        public static string VILLAIN_NAMESPACE = "SentinelsOfTheMultiverse.Data.Villains.";
        public static string ENVIRONMENT_NAMESPACE = "SentinelsOfTheMultiverse.Data.Environments.";
        public static Boolean playerPlayedCard;
        public static Boolean playerUsedPower;
        public static int cardIDNum = 0;
        public static bool resumePlay;


        //private static bool getWinCon()
        //{
        //    return villain.lifeTotal <= 0;
        //}

        public static void initPlayers(List<string> heroesStr, string villainStr, string envStr)
        {
            
            for (int i = 0; i < heroesStr.Count; i++)
            {
                Hero newHero = (Hero)getClassFromString(heroesStr[i]);
                heroes.Add(newHero);
            }
            

            villain = (Villain)getClassFromString(villainStr);
            environment = (GameEnvironment)getClassFromString(envStr);
        }

        public static Object getClassFromString(string className)
        {
            Type hai = Type.GetType(className, true);
            Object o = (Activator.CreateInstance(hai));

            return o;
        }

        public static void nextTurn()
        {
            playerTurn++;
            if (playerTurn == getPlayers().Count)
            {
                playerTurn = 0;
            }
            getCurrentPlayer().playerTurn(playerPlayedCard, playerUsedPower);
            playerPlayedCard = false;
            playerUsedPower = false;
        }

        public static Card getCardFromID(int cardID){

            for (int i = 0; i < getPlayers().Count; i++)
            {
                for(int j = 0; j < getPlayers()[i].cardsOnField.Count; j++){
                    if (getPlayers()[i].cardsOnField[j].getCardID() == cardID)
                    {
                        return getPlayers()[i].cardsOnField[j];
                    }
                }
            }
            throw new CardNotFoundException();
        }

        public static void TearDownGameEngine()
        {
            playerTurn = 0;
            isFirstTurn = true;
            villain = null;
            heroes = null;
            environment = null;
        }

        #region GET SET REGION
        public static int getPlayerTurn()
        {
            return playerTurn;
        }

        public static IPlayer getCurrentPlayer()
        {
            return getPlayers()[playerTurn];
        }

        internal static List<IPlayer> getPlayers()
        {
            var list = new List<IPlayer>();
            list.AddRange(heroes);
            list.Add(villain);
            list.Add(environment);
            return list;
        }

        internal static List<Hero> getHeroes()
        {
            return heroes;
        }

        internal static Villain getVillain()
        {
            return villain;
        }

        internal static GameEnvironment getEnvironment()
        {
            return environment;
        }
        #endregion


        
    }
}
