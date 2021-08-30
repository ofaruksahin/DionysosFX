using Autofac;
using DionysosFX.Swan.Net;
using DionysosFX.Swan.Routing;
using System;
using System.Linq;

namespace DionysosFX.Module.Cors
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class EnableCorsAttribute : Attribute, IEndpointFilter
    {
        public string PolicyName { get; set; }

        public EnableCorsAttribute(string policyName)
        {
            this.PolicyName = policyName;
        }

        public void OnBefore(object sender, IHttpContext httpContext)
        {
            var options = httpContext.Container.Resolve<CorsModuleOptions>();
            if (options == null)
                throw new Exception("Cors Module Options is not defined");
            var policyItem = options.CorsPolicies.FirstOrDefault(f => f.Name == PolicyName);
            if (policyItem == null)
                throw new Exception($"{PolicyName} policy not found");
            httpContext.Response.Headers[CorsModuleConstants.AccessControlAllowCredentials] = "true";
            httpContext.Response.Headers[CorsModuleConstants.AccessControlAllowHeaders] = string.Join(',', policyItem.AllowedHeaders);
            httpContext.Response.Headers[CorsModuleConstants.AccessControlAllowMethods] = string.Join(',', policyItem.AllowedMethods);
            httpContext.Response.Headers[CorsModuleConstants.AccessControlAllowOrigin] = string.Join(',', policyItem.AllowedOrigins);
            httpContext.Response.Headers[CorsModuleConstants.AccessControlMaxAge] = policyItem.MaxAge.ToString();
            if (!policyItem.AllowedHeaders.Contains("*") && !policyItem.AllowedHeaders.Any(f => httpContext.Request.Headers.AllKeys.Any(y => f == y)))
                httpContext.SetHandled();
            if (!policyItem.AllowedMethods.Contains("*") && !policyItem.AllowedMethods.Any(f => f == httpContext.Request.HttpMethod))
                httpContext.SetHandled();
            if (!policyItem.AllowedOrigins.Contains("*") && !policyItem.AllowedOrigins.Any(f => f == httpContext.Request.Headers["Origin"]))
                httpContext.SetHandled();
        }

        public void OnAfter(object sender, IHttpContext httpContext)
        {
        }
    }
}
