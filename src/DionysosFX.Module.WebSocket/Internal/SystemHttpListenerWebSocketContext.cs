using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.WebSockets;

namespace DionysosFX.Module.WebSocket.Internal
{
    public class SystemHttpListenerWebSocketContext : IHttpListenerWebSocketContext
    {
        private HttpListenerWebSocketContext _context;        
        public SystemHttpListenerWebSocketContext(HttpListenerWebSocketContext context)
        {
            _context = context;                 
        }

        public CookieCollection CookieCollection => _context.CookieCollection;

        public NameValueCollection Headers => _context.Headers;

        public bool IsSecureConnection => _context.IsSecureConnection;

        public string Origin => _context.Origin;

        public Uri RequestUri => _context.RequestUri;

        public string SecWebSocketKey => _context.SecWebSocketKey;

        public IEnumerable<string> SecWebSocketProtocols => _context.SecWebSocketProtocols;

        public string SecWebSocketVersion => _context.SecWebSocketVersion;

        private IWebSocket _webSocket;
        public IWebSocket WebSocket => _webSocket ?? (_webSocket = new SystemWebSocket(_context.WebSocket));

    }
}
