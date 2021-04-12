using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dvotc_websocket_csharp.Response;
using DVOTCQuoter;
using DVOTCQuoter.Json;
using Newtonsoft.Json;

namespace dvotc_websocket_csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            string websocketAddress = "websocket Address";
            string key = "Enter your Key here";
            string secret = "Enter your Secret here";

            MarketDataResponse lastMarketDataResponse = null;

            WebsocketClient client = new WebsocketClient(websocketAddress, key, secret);
            client.OnClose += () =>
            {
                Console.WriteLine("Websocket closed");
            };

            client.OnError += (errorMessage) =>
            {
                Console.WriteLine(errorMessage);
            };

            // socket opens
            client.OnOpen += () =>
            {
                // subscribing to BTC/USD market data updates
                SubscriptionRequest marketDataSubscription = new SubscriptionRequest();
                marketDataSubscription.Type = "subscribe";
                marketDataSubscription.Topic = "BTC/USD";
                marketDataSubscription.Event = "levels";

                try
                {
                    string json = JsonConvert.SerializeObject(marketDataSubscription);
                    client.Send(json);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                // subscribing to order updates
                SubscriptionRequest subscription = new SubscriptionRequest();
                subscription.Type = "subscribe";
                subscription.Topic = "order/#";
                subscription.Event = "order-updates";

                try
                {
                    string json = JsonConvert.SerializeObject(subscription);
                    client.Send(json);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            };

            client.OnMessage += (message) =>
            {
                Console.WriteLine(message);
                try
                {
                    BaseResponse response = JsonConvert.DeserializeObject<BaseResponse>(message);
                    if (response.Type == "subscribe")
                    {
                        if (response.Event == "levels")
                        {
                            MarketDataResponse marketDataResponse =
                                JsonConvert.DeserializeObject<MarketDataResponse>(message);
                            lastMarketDataResponse = marketDataResponse;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            };


            int screenInput;
            char inputValue;
            do
            {
                screenInput = Console.Read();
                inputValue = Convert.ToChar(screenInput);

                switch (inputValue)
                {
                    case 'v':
                        break;
                    case 'm':
                        DisplayMenu();
                        break;
                    case 'b':
                    {
                        try
                        {
                            PlaceOrderRequest orderRequest = new PlaceOrderRequest();
                            orderRequest.Type = "request-response";
                            orderRequest.Topic = "createorder";
                            orderRequest.Event = Guid.NewGuid().ToString();

                            orderRequest.Data.QuoteId = lastMarketDataResponse.Data.quoteId;
                            orderRequest.Data.OrderType = "MARKET";
                            orderRequest.Data.Asset = "BTC";
                            orderRequest.Data.CounterAsset = "USD";
                            orderRequest.Data.ClientTag = "Test-SampleOrder";
                            orderRequest.Data.Side = "Buy";


                            orderRequest.Data.Price =
                                lastMarketDataResponse.Data.levels.FirstOrDefault().buyPrice.ToString();
                            orderRequest.Data.Qty = "0.01";

                            string json = JsonConvert.SerializeObject(orderRequest);
                            client.Send(json);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                    }
                        break;
                    
                }
            } while (inputValue != 'q');

            Environment.Exit(0);
        }
        public static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("MENU");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine(" Enter 'q' to quit and exit the program");
            Console.WriteLine(" Enter 'm' to re-display this menu");
            Console.WriteLine(" Enter 'b' to place a buy test order");
        }
    }
}
