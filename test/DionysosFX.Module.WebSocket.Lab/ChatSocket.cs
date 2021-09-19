using DionysosFX.Module.WebSocket.Internal;

namespace DionysosFX.Module.WebSocket.Lab
{
    [WebSocket("/chat")]
    public class ChatSocket : WebSocketHub
    {
        public override void OnConnected(IHttpListenerWebSocketContext context)
        {
        }
    }
}
