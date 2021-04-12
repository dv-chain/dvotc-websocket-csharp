using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVOTCQuoter.Json
{
    public class OrderAcknowledgement
    {
        public string _id { get; set; }
        public string createdAt { get; set; }
        public string limitPrice { get; set; }
        public string price { get; set; }
        public string quantity { get; set; }
        public string side { get; set; }
        public string asset { get; set; }
        public string counterAsset { get; set; }
        public string status { get; set; }
        public string clientTag { get; set; }
    }
}
