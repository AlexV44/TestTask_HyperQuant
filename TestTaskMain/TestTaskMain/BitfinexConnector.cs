using ConnectorTest;
using System.Net.WebSockets;
using System.Text;
using TestTaskMain.Entity;

namespace TestTaskMain
{
    public class BitfinexConnector : ITestConnector
    {
        private String pubUrl = "https://api-pub.bitfinex.com/v2";
        private String wsUrl = "wss://api-pub.bitfinex.com/ws/2";
        private HttpClient httpClient = new HttpClient();
        private ClientWebSocket ws = new ClientWebSocket();
        private CancellationTokenSource cts = new CancellationTokenSource();

        #region ITestCon
        public event Action<Trade> NewBuyTrade;
        public event Action<Trade> NewSellTrade;
        public event Action<Candle> CandleSeriesProcessing;

        public Task<IEnumerable<Candle>> GetCandleSeriesAsync(string pair, int periodInSec, DateTimeOffset? from, DateTimeOffset? to = null, long? count = 0)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Trade>> GetNewTradesAsync(string pair, int maxCount)
        {
            throw new NotImplementedException();
        }

        public void SubscribeCandles(string pair, int periodInSec, DateTimeOffset? from = null, DateTimeOffset? to = null, long? count = 0)
        {
            throw new NotImplementedException();
        }

        public void SubscribeTrades(string pair, int maxCount = 100)
        {
            throw new NotImplementedException();
        }

        public void UnsubscribeCandles(string pair)
        {
            throw new NotImplementedException();
        }

        public void UnsubscribeTrades(string pair)
        {
            throw new NotImplementedException();
        }
        #endregion

    public async Task ConnectAsync()
        {
            try
            {
                await ws.ConnectAsync(new Uri(pubUrl), cts.Token);
                Console.WriteLine("WebSocket connected.");
            }
            catch (WebSocketException ex)
            {
                Console.WriteLine($"WebSocket connection error: {ex.Message}");
            }
        }

        private async Task SendAsync(string message)
        {
            if (ws.State == WebSocketState.Open)
            {
                try
                {
                    await ws.SendAsync(Encoding.UTF8.GetBytes(message), WebSocketMessageType.Text, true, cts.Token);
                }
                catch (WebSocketException ex)
                {
                    Console.WriteLine($"WebSocket send error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("WebSocket is not open.");
            }
        }

    }
}