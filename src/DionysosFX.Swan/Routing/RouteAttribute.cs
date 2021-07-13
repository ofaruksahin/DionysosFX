using System;

namespace DionysosFX.Swan.Routing
{
    [AttributeUsage(
        AttributeTargets.Class | 
        AttributeTargets.Method, 
        AllowMultiple = false)]        
    public class RouteAttribute : Attribute
    {
        public HttpVerb Verb
        {
            get;
            private set;
        }
        public string Route
        {
            get;
            private set;
        }


        public RouteAttribute(HttpVerb verb,string route)
        {
            Verb = verb;
            Route = route;
        }

        public RouteAttribute(string route)
        {
            Route = route;
        }

    }
}
