using System;
using System.Collections.Generic;
using System.Reflection;

namespace DionysosFX.Swan.Routing
{
    public class RouteResolveResponse
    {
        public string Route
        {
            get;
            set;
        }

        public HttpVerb Verb
        {
            get;
            set;
        }

        public Type EndpointType
        {
            get;
            set;
        }

        public MethodInfo Invoke
        {
            get;
            set;
        }

        public MethodInfo SetHttpContext
        {
            get;
            set;
        }

        public List<string> QueryString
        {
            get;
            set;
        } = new List<string>();
    }
}
