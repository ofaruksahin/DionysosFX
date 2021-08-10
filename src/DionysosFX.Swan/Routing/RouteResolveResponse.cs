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

        public List<ParameterInfo> InvokeParameters
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

        public List<Attribute> Attributes
        {
            get;
            set;
        } = new List<Attribute>();

        public List<ParameterInfo> ConstructorParameters
        {
            get;
            set;
        } = new List<ParameterInfo>();
    }
}
