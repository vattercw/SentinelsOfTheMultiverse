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
        public static bool resumePlay = false;
        public static bool primordial = false;

        public enum ForcedEffect { ConsiderThePrice, PrimordialPlant, DeviousDisruption, RiverOfLava };

        //private static bool getWinCon()
        //{
        //    return villain.lifeTotal <= 0;
        //}

        public static void initPlayers(List<string> heroesStr, string villainStr, string envStr)
        {
            for (int i = 0; i < heroesStr.Count; i++)
            {
                Hero newHero = (Hero)getClassFromString(heroesStr[i]);
                //TODO testing code to initialize certain cards in a players hand
                newHero.hand.RemoveRange(0, 2);
                if (newHero.GetType().Equals(typeof(Haka)))
                {
                    newHero.hand.Add(new Card("\\Images\\Hero\\Haka\\2-SavageMana.png"));
                    //newHero.hand.Add(new Card("C:\\Users\\rujirasl.000\\Documents\\GitHub\\SentinelsOfTheMultiverse\\SentinelsOfTheMultiverse\\SentinelsOfTheMultiverse\\Images\\Hero\\Haka\\3-EnduringIntercession.png"));
                }
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

            int numDead = 0;
            if (GameEngine.getVillain().graveyard.Count >= 15)
            {
                GameBoard.LoseCondition();
            }
            foreach (Hero hero in GameEngine.getHeroes())
            {
                if (hero.deck.cards.Count <= 0)
                {
                    GameBoard.LoseCondition();
                }
                if (hero.lifeTotal <= 0)
                {
                    numDead++;
                }
            }
            if (numDead >= GameEngine.getHeroes().Count)
            {
                GameBoard.LoseCondition();
            }
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
            heroes = new List<Hero>();
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
