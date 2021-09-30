using DionysosFX.Swan.Net;
using DionysosFX.Swan.Routing;
using System;

namespace $rootnamespace$.EnpointFilter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method,AllowMultiple = true)]
    public class $safeitemname$ :Attribute, IEndpointFilter
    {
        public void OnAfter(object sender, IHttpContext httpContext)
        {
            if(sender is RouteResolveResponse rsp)
            {

            }
        }

        public void OnBefore(object sender, IHttpContext httpContext)
        {
            if(sender is RouteResolveResponse rsp)
            {

            }
        }
    }
}
