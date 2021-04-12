using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace DVOTCQuoter.Json
{
    public class PlaceOrderRequest
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("event")]
        public string Event { get; set; }
        [JsonProperty("topic")]
        public string Topic { get; set; }
        [JsonProperty("data")]
        public OrderData Data { get; set; }

        public PlaceOrderRequest()
        {
            Data = new OrderData();
        }
    }
}
