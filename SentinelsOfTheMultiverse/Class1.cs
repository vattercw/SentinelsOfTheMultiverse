using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse
{
    class GameEngine
    {
        public Startup startScreen = new Startup();
        public Board board = new Board();
        public List<Player> players = new List<Player>();
        public List<Hero> heroes = new List<Hero>();
        public Villian villian;
        public GameEnvironment environment;

        public void begin()
        {
            String villianName;
            List<String> heroNames;
            String environmentName;

            //find out how to return more than one value appropriately
            villianName, heroNames, environmentName= this.startScreen.begin();

            this.villian = new Villian(villianName);
            for(int i = 0; i < heroNames.Count; i++){
                Hero newHero = new Hero(heroNames[i]);
                this.heroes.Add(newHero);
            }
            this.environment = new GameEnvironment(environmentName);
            
            this.board.initialize();


        }
        

    }
}
