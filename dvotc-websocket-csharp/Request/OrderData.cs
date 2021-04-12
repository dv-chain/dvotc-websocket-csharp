using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace DVOTCQuoter.Json
{
    public class OrderData
    {
        [JsonProperty("quoteId")]
        public string QuoteId { get; set; }
        [JsonProperty("orderType")]
        public string OrderType { get; set; }
        [JsonProperty("asset")]
        public string Asset { get; set; }
        [JsonProperty("counterAsset")]
        public string CounterAsset { get; set; }
        [JsonProperty("price")]
        public string Price { get; set; }
        [JsonProperty("limitPrice")]
        public string LimitPrice { get; set; }
        [JsonProperty("qty")]
        public string Qty { get; set; }
        [JsonProperty("side")]
        public string Side { get; set; }
        [JsonProperty("clientTag")]
        public string ClientTag { get; set; }
    }
}
