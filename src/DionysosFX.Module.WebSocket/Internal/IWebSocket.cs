using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.Module.WebSocket.Internal
{
    public interface IWebSocket
    {
        string? CloseStatusDescription { get; }
        WebSocketCloseStatus? CloseStatus { get; }
        WebSocketState State { get; }
        void Abort();
        Task CloseAsync(WebSocketCloseStatus closeStatus, string? statusDescription, CancellationToken cancellationToken);
        Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken);
        Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken);
        void Dispose();
    }
}
