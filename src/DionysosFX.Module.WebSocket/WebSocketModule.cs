using DionysosFX.Swan.Extensions;
using DionysosFX.Swan.Modules;
using DionysosFX.Swan.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using System.Reflection;

namespace DionysosFX.Module.WebSocket
{
    internal class WebSocketModule : WebModuleBase
    {
        WebSocketModuleOptions options = null;

        List<WebSocketItem> webSockets = null;

        public override void Start(CancellationToken cancellationToken)
        {
            Container.TryResolve<WebSocketModuleOptions>(out options);
            webSockets = WebSocketExtension.GetWebSocketItems();
            foreach (var webSocket in webSockets)
            {
                if (webSocket.IsInstanceGenerated)
                    continue;
                webSocket.Instance = webSocket.ConstructorParameters.CreateInstance(Container, webSocket.SocketType);
                webSocket.Instance.Invoke(WebSocketConstants.SetOptions, BindingFlags.Instance | BindingFlags.NonPublic, new[] { options });
                webSocket.IsInstanceGenerated = webSocket.Instance != null;
            }
        }

        public override async Task HandleRequestAsync(IHttpContext context)
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
                throw new NullReferenceException($"{socket.SocketType.Name} is null reference");
            }

            if (socket.Instance != null)
            {
                var httpListenerWebSocketContext = await context.AcceptWebSocketRequest();
                socket.Instance.Invoke(WebSocketConstants.OnBeforeConnectedMethod,BindingFlags.Instance | BindingFlags.NonPublic, new[] { httpListenerWebSocketContext });                
            }          
        }

        public override void Dispose()
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
