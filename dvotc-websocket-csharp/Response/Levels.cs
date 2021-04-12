using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVOTCQuoter.Json
{
    public class Levels
    {
        public List<Price> levels { get; set; }
        public long lastUpdate { get; set; }
        public string quoteId { get; set; }
        public string market { get; set; }

        public Levels()
        {
            levels = new List<Price>();
        }
    }
}
