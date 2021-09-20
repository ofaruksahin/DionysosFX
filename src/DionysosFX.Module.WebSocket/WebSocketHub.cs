using DionysosFX.Module.WebSocket.Internal;
using DionysosFX.Swan.Threading;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.Module.WebSocket
{
    public abstract class WebSocketHub : IDisposable
    {
        WebSocketModuleOptions options = null;

        ConcurrentDictionary<string, IHttpListenerWebSocketContext> clients = new ConcurrentDictionary<string, IHttpListenerWebSocketContext>();

        ConcurrentDictionary<string, List<IHttpListenerWebSocketContext>> groups = new ConcurrentDictionary<string, List<IHttpListenerWebSocketContext>>();

        public WebSocketHub()
        {
            PeriodicTask.Create(PeriodicTaskDoWork, 5);
        }

        private void SetOptions(WebSocketModuleOptions options)
        {
            this.options = options;
        }

        private async Task OnBeforeConnected(HttpListenerWebSocketContext webSocketContext)
        {
            IHttpListenerWebSocketContext _webSocketContext = new SystemHttpListenerWebSocketContext(webSocketContext);

            if (_webSocketContext.WebSocket.State == WebSocketState.Open)
            {
                await OnConnected(_webSocketContext);
                clients.TryAdd(_webSocketContext.SecWebSocketKey, _webSocketContext);
                try
                {
                    while (_webSocketContext.WebSocket.State == WebSocketState.Open)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            WebSocketReceiveResult result;
                            do
                            {
                                byte[] bytes = new byte[options.BufferSize];
                                result = await _webSocketContext.WebSocket.ReceiveAsync(bytes, CancellationToken.None);
                                ms.Write(bytes, 0, result.Count);
                            } while (!result.EndOfMessage);

                            if(result.MessageType == WebSocketMessageType.Text)
                            {
                                var message = Encoding.UTF8.GetString(ms.ToArray());
                                OnMessage(_webSocketContext, message);
                            }
                        }                                              
                    }
                    OnBeforeDisconnected(_webSocketContext);
                }
                catch (WebSocketException)
                {
                    OnBeforeDisconnected(_webSocketContext);
                }
            }
        }

        public virtual async Task OnConnected(IHttpListenerWebSocketContext context)
        {

        }

        private async Task OnBeforeDisconnected(IHttpListenerWebSocketContext context)
        {
            await OnDisconnected(context);
        }

        public virtual async Task OnDisconnected(IHttpListenerWebSocketContext context)
        {

        }

        public virtual async Task OnMessage(IHttpListenerWebSocketContext context,string message)
        {

        }

        private void PeriodicTaskDoWork()
        {
            List<string> removedClientKeys = clients
                .Where(f => f.Value.WebSocket.State != WebSocketState.Connecting && f.Value.WebSocket.State != WebSocketState.Open)
                .Select(f => f.Key)
                .ToList();

            foreach (var key in removedClientKeys)
                clients.TryRemove(key, out IHttpListenerWebSocketContext _ctx);
        }

        public void Dispose()
        {
        }
    }
}
