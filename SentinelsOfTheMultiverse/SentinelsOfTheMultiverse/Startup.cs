using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SentinelsOfTheMultiverse
{
    public class Startup
    {
        List<string> _heroesStr;
        string _villainStr;
        string _envStr;

        internal void begin()
        {
            _heroesStr = new List<string>();
            _villainStr = GameEngine.VILLAIN_NAMESPACE + "BaronBlade";
            _envStr = GameEngine.ENVIRONMENT_NAMESPACE + "InsulaPrimus";

            _heroesStr.Add(GameEngine.HERO_NAMESPACE + "Haka");
            _heroesStr.Add(GameEngine.HERO_NAMESPACE + "Haka");
        }

        public List<string> getHeroesString()
        {
            return _heroesStr;
        }

        public string getVillainStr()
        {
            return _villainStr;
        }

        public string getEnvironmentString()
        {
            return _envStr;
        }
        
    }
}
