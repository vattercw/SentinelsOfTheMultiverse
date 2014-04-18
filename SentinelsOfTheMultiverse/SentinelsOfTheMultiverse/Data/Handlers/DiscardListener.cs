using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelsOfTheMultiverse.Data.Handlers
{
    public delegate void DiscardHandler(object sender, System.EventArgs e);

    public class DiscardListener : System.EventArgs
    {
        public class Discarder
        {
            public event DiscardHandler ForceDiscard;

            protected virtual void OnForceDiscard(DiscardListener e)
            {
                if (ForceDiscard != null) ForceDiscard(this, e);
            }
        }
    }
}
