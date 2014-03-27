using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SentinelsOfTheMultiverse.Data;

namespace SentinelsOfTheMultiverse
{
    class GameEngine
    {
        public Startup startScreen = new Startup();
        public GameBoard board = new GameBoard();
        public List<IPlayer> players = new List<IPlayer>();
        public List<Hero> heroes = new List<Hero>();
        public Villian villian;
        public GameEnvironment environment;
        private int playerTurn = 0;

        public void begin()
        {
            String villianName;
            List<String> heroNames;
            String environmentName;

            //find out how to return more than one value appropriately
            villianName, heroNames, environmentName= this.startScreen.begin();

            this.villian = (Villian) System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(villianName);
            for(int i = 0; i < heroNames.Count; i++){
                Hero newHero = (Hero) System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(heroNames[i]);
                this.heroes.Add(newHero);
            }
            this.environment = (GameEnvironment) System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(environmentName);
            
            this.board.initialize();


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
