using DionysosFX.Module.WebSocket.Internal;
using System;
using System.Linq;
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
            var queries = message.Split('?', '&').ToList();
            queries.RemoveAll(f => string.IsNullOrEmpty(f));
            var firstQueryValue = queries[0].Split('=')[1];
            var secondQueryValue = queries[1].Split('=')[1];
            var clientMessage = string.Empty;
            if(queries.Count == 3)
                clientMessage = queries[2].Split('=')[1];
            secondQueryValue = System.Web.HttpUtility.UrlDecode(secondQueryValue);
            switch (firstQueryValue)
            {
                case "AddGroup":
                    AddGroup(secondQueryValue, context);
                    break;
                case "RemoveGroup":
                    RemoveGroup(secondQueryValue, context);
                    break;
                case "SendGroup":
                    SendToGroup(secondQueryValue, clientMessage, true);
                    break;
                case "SendOther":
                    SendToOther(context, clientMessage, true);
                    break;
                case "Send":
                    Send(context, clientMessage, true);
                    break;
                default:
                    break;
            }
            Console.WriteLine(context.SecWebSocketKey + ":" + message);
        }
    }
}
