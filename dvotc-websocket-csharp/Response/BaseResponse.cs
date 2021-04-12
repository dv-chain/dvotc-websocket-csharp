using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace dvotc_websocket_csharp.Response
{
    public class BaseResponse
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("topic")]
        public string Topic { get; set; }
        [JsonProperty("event")]
        public string Event { get; set; }
    }
}
