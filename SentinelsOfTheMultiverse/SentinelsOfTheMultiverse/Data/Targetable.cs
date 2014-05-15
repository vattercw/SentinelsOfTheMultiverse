using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse.Data {
    public interface Targetable {
        int lifeTotal { get; set; }
        int maxHealth { get; set; } 
    }
}
