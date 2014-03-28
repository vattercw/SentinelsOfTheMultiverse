using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SentinelsOfTheMultiverse
{
    class Startup
    {
        internal List<String> begin()
        {
            List<String> names = new List<String>();

            names.Add("BaronBlade");
            names.Add("Haka");
            names.Add(null);
            names.Add(null);
            names.Add(null);
            names.Add(null);
            names.Add(null);
            names.Add("InsulaPrimus");

            return names;
        }
    }
}
