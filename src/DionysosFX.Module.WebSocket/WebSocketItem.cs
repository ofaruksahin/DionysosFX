using System;
using System.Collections.Generic;
using System.Reflection;

namespace DionysosFX.Module.WebSocket
{
    internal class WebSocketItem
    {
        public Type SocketType { get; set; }
        public string Name { get; set; }
        public string Route { get; set; }
        public object Instance { get; set; }
        public bool IsInstanceGenerated { get; set; }
        //public ConstructorInfo Constructor { get; set; }
        public List<ParameterInfo> ConstructorParameters { get; set; }
        public MethodInfo OnBeforeConnected { get; set; }
    }
}
