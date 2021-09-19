using System;

namespace DionysosFX.Module.WebSocket
{
    [AttributeUsage(AttributeTargets.Class,AllowMultiple = false)]
    public class WebSocketAttribute : Attribute
    {
        public string Route
        {
            get;
            set;
        }

        public WebSocketAttribute(string route)
        {
            Route = route;
        }
    }
}
