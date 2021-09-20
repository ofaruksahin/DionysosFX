using DionysosFX.Module.WebSocket.Internal;
using System;
using System.Threading.Tasks;

namespace DionysosFX.Module.WebSocket.Lab
{
    [WebSocket("/chat")]
    public class ChatSocket : WebSocketHub
    {
        public override async Task OnConnected(IHttpListenerWebSocketContext context)
        {
            Console.WriteLine(context.SecWebSocketKey + " Connected");
        }

        public override async Task OnDisconnected(IHttpListenerWebSocketContext context)
        {
            Console.WriteLine(context.SecWebSocketKey + " Disconnected");
        }

        public override async Task OnMessage(IHttpListenerWebSocketContext context, string message)
        {
            Console.WriteLine(context.SecWebSocketKey + ":" + message);
        }
    }
}
