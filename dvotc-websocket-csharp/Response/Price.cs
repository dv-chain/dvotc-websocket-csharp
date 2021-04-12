using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVOTCQuoter.Json
{
    public class Price
    {
        public decimal sellPrice { get; set; }
        public decimal buyPrice { get; set; }
        public decimal maxQuantity { get; set; }
    }
}
