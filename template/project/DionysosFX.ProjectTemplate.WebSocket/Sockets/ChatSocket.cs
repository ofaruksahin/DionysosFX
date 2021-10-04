using DionysosFX.Module.WebSocket;
using DionysosFX.Module.WebSocket.Internal;
using System.Threading.Tasks;

namespace DionysosFX.ProjectTemplate.WebSocket.Sockets
{
    [WebSocket("/chat")]
    public class ChatSocket :WebSocketHub
    {
        public override Task OnConnected(IHttpListenerWebSocketContext context)
        {
            return base.OnConnected(context);
        }
        public override Task OnDisconnected(IHttpListenerWebSocketContext context)
        {
            return base.OnDisconnected(context);
        }

        public override Task OnMessage(IHttpListenerWebSocketContext context, string message)
        {
            return base.OnMessage(context, message);
        }
    }
}
