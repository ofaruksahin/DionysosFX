using DionysosFX.Module.WebSocket.Internal;
using DionysosFX.Swan.Threading;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;

namespace DionysosFX.Module.WebSocket
{
    public abstract class WebSocketHub : IDisposable
    {
        ConcurrentDictionary<string, IHttpListenerWebSocketContext> activeClients = new ConcurrentDictionary<string, IHttpListenerWebSocketContext>();

        public WebSocketHub()
        {
            PeriodicTask.Create(PeriodicTaskDoWork, 5);
        }

        private void OnBeforeConnected(HttpListenerWebSocketContext webSocketContext)
        {
            IHttpListenerWebSocketContext _webSocketContext = new SystemHttpListenerWebSocketContext(webSocketContext);

            switch (_webSocketContext.WebSocket.State)
            {
                case WebSocketState.Open:
                    OnConnected(_webSocketContext);
                    break;
            }

            if(_webSocketContext.WebSocket.State == WebSocketState.Open)
            {
                activeClients.TryAdd(_webSocketContext.SecWebSocketKey, _webSocketContext);
            }            
        }

        public virtual void OnConnected(IHttpListenerWebSocketContext context)
        {

        }

        private void PeriodicTaskDoWork()
        {
            List<string> removedClientKeys = activeClients
                .Where(f => f.Value.WebSocket.State != WebSocketState.Connecting && f.Value.WebSocket.State != WebSocketState.Open)
                .Select(f => f.Key)
                .ToList();

            foreach (var key in removedClientKeys)
                activeClients.TryRemove(key, out IHttpListenerWebSocketContext _ctx);
        }

        public void Dispose()
        {
        }
    }
}
