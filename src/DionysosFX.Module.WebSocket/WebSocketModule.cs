using DionysosFX.Swan.Extensions;
using DionysosFX.Swan.Modules;
using DionysosFX.Swan.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.Module.WebSocket
{
    internal class WebSocketModule : IWebModule
    {
        List<WebSocketItem> webSockets = null;
        public void Start(CancellationToken cancellationToken)
        {
            webSockets = WebSocketExtension.GetWebSocketItems();
        }

        public async Task HandleRequestAsync(IHttpContext context)
        {
            if (context.IsHandled)
                return;

            if (!context.Request.IsWebSocketRequest)
                return;

            var absolutePath = context.Request.Url.AbsolutePath;
            var socket = webSockets.FirstOrDefault(f => f.Route == absolutePath);
            if (socket == null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Close();
                return;
            }

            if (!socket.IsInstanceGenerated)
            {
                socket.Instance = socket.ConstructorParameters.CreateInstance(context, socket.SocketType);
                socket.IsInstanceGenerated = socket.Instance != null;
            }

            if (socket.Instance != null)
            {
                var httpListenerWebSocketContext = await context.AcceptWebSocketRequest();
                if (socket.OnBeforeConnected != null)
                {
                    socket.OnBeforeConnected.Invoke(socket.Instance, new[] { httpListenerWebSocketContext });
                }
            }

        }

        public void Dispose()
        {
            foreach (var webSocket in webSockets)
            {
                if (webSocket.Instance is WebSocketHub wch)
                    wch.Dispose();
            }
            webSockets.Clear();
            webSockets = null;
        }
    }
}
