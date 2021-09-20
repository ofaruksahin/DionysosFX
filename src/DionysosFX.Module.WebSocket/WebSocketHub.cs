using Autofac;
using DionysosFX.Module.WebSocket.Internal;
using DionysosFX.Swan.Threading;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.Module.WebSocket
{
    public abstract class WebSocketHub : IDisposable
    {
        WebSocketModuleOptions options = null;

        ConcurrentDictionary<string, IHttpListenerWebSocketContext> clients = new ConcurrentDictionary<string, IHttpListenerWebSocketContext>();

        ConcurrentDictionary<string, List<IHttpListenerWebSocketContext>> groups = new ConcurrentDictionary<string, List<IHttpListenerWebSocketContext>>();

        public IContainer Container;

        public WebSocketHub()
        {
            PeriodicTask.Create(PeriodicTaskDoWork, 5);
        }

        private void SetOptions(WebSocketModuleOptions options)
        {
            this.options = options;
        }

        private void SetIContainer(IContainer container)
        {
            Container = container;
        }

        private async Task OnBeforeConnected(HttpListenerWebSocketContext webSocketContext)
        {
            IHttpListenerWebSocketContext _webSocketContext = new SystemHttpListenerWebSocketContext(webSocketContext);

            if (_webSocketContext.WebSocket.State == WebSocketState.Open)
            {
                clients.TryAdd(_webSocketContext.SecWebSocketKey, _webSocketContext);
                await OnConnected(_webSocketContext);
                try
                {
                    while (_webSocketContext.WebSocket.State == WebSocketState.Open)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            WebSocketReceiveResult result;
                            do
                            {
                                byte[] buffer = new byte[options.BufferSize];
                                result = await _webSocketContext.WebSocket.ReceiveAsync(buffer, CancellationToken.None);
                                ms.Write(buffer, 0, result.Count);
                            } while (!result.EndOfMessage);

                            if(result.MessageType == WebSocketMessageType.Text)
                            {
                                var message = Encoding.UTF8.GetString(ms.ToArray());
                                OnMessage(_webSocketContext, message);
                            }
                        }                                              
                    }
                    OnBeforeDisconnected(_webSocketContext);
                }
                catch (WebSocketException)
                {
                    OnBeforeDisconnected(_webSocketContext);
                }
            }
        }

        public virtual async Task OnConnected(IHttpListenerWebSocketContext context)
        {

        }

        private async Task OnBeforeDisconnected(IHttpListenerWebSocketContext context)
        {
            if (context.WebSocket.State == WebSocketState.Connecting || context.WebSocket.State == WebSocketState.Open)
                context.WebSocket.Abort();
            await OnDisconnected(context);
        }

        public virtual async Task OnDisconnected(IHttpListenerWebSocketContext context)
        {

        }

        public void AddGroup(string groupName,IHttpListenerWebSocketContext context)
        {
            if (groups.ContainsKey(groupName))
            {
                if (groups.TryGetValue(groupName,out List<IHttpListenerWebSocketContext> _ctxs))
                {
                    _ctxs.Add(context);
                }
            }
            else
            {
                var ctxs = new List<IHttpListenerWebSocketContext>() { context };
                groups.TryAdd(groupName, ctxs);
            }
        }

        public void RemoveGroup(string groupName,IHttpListenerWebSocketContext context)
        {
            if(groups.TryGetValue(groupName,out List<IHttpListenerWebSocketContext> _ctxs))
            {
                _ctxs.RemoveAll(f => f.SecWebSocketKey == context.SecWebSocketKey);
            }
        }

        public async Task Send(IHttpListenerWebSocketContext context,string message,bool endOfMessage = true)
        {
            try
            {
                byte[] buffer = new byte[options.BufferSize];
                buffer = Encoding.UTF8.GetBytes(message);
                await context.WebSocket.SendAsync(buffer, WebSocketMessageType.Text, endOfMessage, new CancellationToken());                
            }
            catch (WebSocketException)
            {
                OnBeforeDisconnected(context);
            }
        }

        public async Task SendToOther(IHttpListenerWebSocketContext context,string message, bool endOfMessage = true)
        {
            var otherClients = clients
                .Where(f => f.Key != context.SecWebSocketKey)
                .Select(f => f.Value)
                .ToList();
            foreach (var otherContext in otherClients)
                await Send(otherContext, message, endOfMessage);
        }

        public async Task SendToGroup(string groupName,string message, bool endOfMessage = true)
        {
            if(groups.TryGetValue(groupName,out List<IHttpListenerWebSocketContext> _ctxs))
            {
                foreach (var context in _ctxs)
                    await Send(context, message, endOfMessage);
            }
        }

        public virtual async Task OnMessage(IHttpListenerWebSocketContext context,string message)
        {

        }

        private void PeriodicTaskDoWork()
        {
            List<string> removedClientKeys = clients
                .Where(f => f.Value.WebSocket.State != WebSocketState.Connecting && f.Value.WebSocket.State != WebSocketState.Open)
                .Select(f => f.Key)
                .ToList();

            foreach (var key in removedClientKeys)
            {
                if (clients.TryRemove(key, out IHttpListenerWebSocketContext _ctx))
                {
                    List<string> groupNames = groups
                                    .Where(f => f.Value.Any(y => y.SecWebSocketKey == _ctx.SecWebSocketKey))
                                    .Select(f => f.Key)
                                    .ToList();

                    foreach (var groupName in groupNames)
                    {
                        if (groups.TryGetValue(groupName,out List<IHttpListenerWebSocketContext> _ctxs))
                        {
                            _ctxs.RemoveAll(f => f.SecWebSocketKey == _ctx.SecWebSocketKey);
                        }                            
                    }
                }           
            }

            List<string> removedGroups = groups
                .Where(f => !f.Value.Any())
                .Select(f => f.Key)
                .ToList();

            foreach (var key in removedGroups)
                groups.TryRemove(key, out List<IHttpListenerWebSocketContext> _ctxs);
        }

        public void Dispose()
        {
        }
    }
}
