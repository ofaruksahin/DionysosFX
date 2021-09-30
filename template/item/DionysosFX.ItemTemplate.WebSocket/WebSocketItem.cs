using DionysosFX.Module.WebSocket;
using DionysosFX.Module.WebSocket.Internal;
using System.Threading.Tasks;

namespace DionysosFX.ItemTemplate.WebSocket
{
    [WebSocket("/$safeitemname$")]
    public class $safeitemname$ : WebSocketHub
    {
        public override async Task OnConnected(IHttpListenerWebSocketContext context)
        {
        }

        public override async Task OnDisconnected(IHttpListenerWebSocketContext context)
        {
        }

        public override async Task OnMessage(IHttpListenerWebSocketContext context, string message)
        {
        }
    }
}
