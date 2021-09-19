using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;

namespace DionysosFX.Module.WebSocket.Internal
{
    public interface IHttpListenerWebSocketContext
    {
        CookieCollection CookieCollection { get; }
        NameValueCollection Headers { get; }
        bool IsSecureConnection { get; }
        string Origin { get; }
        Uri RequestUri { get; }
        string SecWebSocketKey { get; }
        IEnumerable<string> SecWebSocketProtocols { get; }
        string SecWebSocketVersion { get; }
        IWebSocket WebSocket { get; }
    }
}
