using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace DVOTCQuoter.Json
{
    public class CancelOrderRequest
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("event")]
        public string Event { get; set; }
        [JsonProperty("topic")]
        public string Topic { get; set; }
        [JsonProperty("data")]
        public Data Data { get; set; }

        public CancelOrderRequest()
        {
            Data = new Data();
        }
    }
}
