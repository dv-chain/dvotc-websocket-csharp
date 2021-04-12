using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using WebSocketSharp;

namespace DVOTCQuoter
{
    public class WebsocketClient
    {
        public event Action OnOpen;
        public event Action OnClose;
        public event Action<string> OnError;
        public event Action<string> OnMessage;

        string websocketAddress;
        WebSocket socket;
        string apiKey;
        string apiSecret;

        public WebsocketClient(string websocketAddress, string key, string secret)
        {
            this.websocketAddress = websocketAddress;
            this.apiKey = key;
            this.apiSecret = secret;
        }

        public void Start()
        {
            string timestamp = DateTime.UtcNow.Ticks + "";
            string timewindow = "5000";
            string signature = Signature(apiKey, apiSecret, timestamp, timewindow);
            socket = new WebSocket(websocketAddress);
            
            socket.CustomHeaders = new Dictionary<string, string>
            {
                { "DV-API-KEY", apiKey },
                { "DV-SIGNATURE", signature },
                { "DV-TIMESTAMP", timestamp },
                { "DV-TIMEWINDOW",  timewindow }
            };

            socket.OnOpen += Socket_OnOpen;
            socket.OnClose += Socket_OnClose;
            socket.OnError += Socket_OnError;
            socket.OnMessage += Socket_OnMessage;
            socket.Connect();
        }

        private string Signature(string key, string secret, string timestamp, string timewindow)
        {
            string rv = null;
            string payload = apiKey + timestamp + timewindow;
            var hash = new HMACSHA256(Encoding.ASCII.GetBytes(secret));
            byte[] prehash = hash.ComputeHash(Encoding.ASCII.GetBytes(payload));
            rv = Convert.ToBase64String(prehash);
            return rv;
        }


        public void Stop()
        {
            socket.Close();
        }

        public void Send(string message)
        {
            Console.WriteLine("Sending " + message);
            socket.Send(message);
        }

        private void Socket_OnOpen(object sender, EventArgs e)
        {
            OnOpen?.Invoke();
        }

        private void Socket_OnClose(object sender, CloseEventArgs e)
        {
            OnClose?.Invoke();
        }

        private void Socket_OnError(object sender, ErrorEventArgs e)
        {
            OnError?.Invoke(e.Message);
        }
        private void Socket_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.IsText)
            {
                OnMessage?.Invoke(e.Data);
            }
        }
    }
}
