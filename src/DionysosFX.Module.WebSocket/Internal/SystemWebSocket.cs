using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.Module.WebSocket.Internal
{
    public class SystemWebSocket : IWebSocket
    {
        private System.Net.WebSockets.WebSocket _webSocket;

        public SystemWebSocket(System.Net.WebSockets.WebSocket webSocket)
        {
            _webSocket = webSocket;
        }

        public string CloseStatusDescription => _webSocket.CloseStatusDescription;

        public WebSocketCloseStatus? CloseStatus => _webSocket.CloseStatus;

        public WebSocketState State => _webSocket.State;

        public void Abort()
        {
            _webSocket.Abort();
        }

        public Task CloseAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)=> _webSocket.CloseAsync(closeStatus, statusDescription, cancellationToken);

        public Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken) => _webSocket.ReceiveAsync(buffer, cancellationToken);

        public Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken) => _webSocket.SendAsync(buffer, messageType, endOfMessage, cancellationToken);

        public void Dispose()
        {
            _webSocket.Dispose();
        }
    }
}
