﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using SentinelsOfTheMultiverse.Data;

namespace SentinelsOfTheMultiverse
{
    class GameEngine
    {

        public Startup startScreen = new Startup();
        public List<IPlayer> players = new List<IPlayer>();
        public List<Hero> heroes = new List<Hero>();
        public Villian villian;
        public GameEnvironment environment;
        private int playerTurn = 0;
        Image villainImage=new Image();

        public static int MAXPLAYER = 6;
        public static int VILLIANNUM = 6;
        public static int EVIRONMENTNUM = 7;

        public GameEngine()
        {
            begin();
        }

        public void begin()
        {

            //find out how to return more than one value appropriately
            //villianName, heroNames, environmentName= this.startScreen.begin();

            //this.villian = new Villian(villianName);
            //for(int i = 0; i < heroNames.Count; i++){
                
            //    Hero newHero = (Hero) System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(heroNames[i]);
            //    this.heroes.Add(newHero);
            //}
            //this.environment = new GameEnvironment(environmentName);
            
            //this.board.initialize();

            //List<String> playerNames = this.startScreen.begin();

           
            //for(int i = 0; i < MAXPLAYER; i++){
            //    Hero newHero = (Hero) System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(playerNames[i]);
            //    this.heroes.Add(newHero);
            //}
            //this.villian = (Villian) System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(playerNames[VILLIANNUM]);
            //this.environment = (GameEnvironment) System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(playerNames[EVIRONMENTNUM]);
            
            villainImage.Source = new BitmapImage(new Uri("C:\\Users\\vattercw\\Documents\\GitHub\\SentinelsOfTheMultiverse\\SentinelsOfTheMultiverse\\SentinelsOfTheMultiverse\\Images\\Villain\\baron_blade\\bb_back.png"));



        }

        public void newTurn()
        {
            this.players[playerTurn].playTurn();
            this.playerTurn++;
            if (this.playerTurn == this.players.Count)
            {
                this.playerTurn = 0;
            }
        }
        

    }
}
