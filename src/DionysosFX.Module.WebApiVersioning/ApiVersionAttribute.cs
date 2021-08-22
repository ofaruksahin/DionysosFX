using Autofac;
using DionysosFX.Swan.Net;
using DionysosFX.Swan.Routing;
using System;
using System.Linq;
using System.Reflection;

namespace DionysosFX.Module.WebApiVersioning
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class ApiVersionAttribute : Attribute, IEndpointFilter
    {
        /// <summary>
        /// 
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Deprecated { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="version"></param>
        /// <param name="deprecated"></param>
        public ApiVersionAttribute(string version, bool deprecated = false)
        {
            Version = version;
            Deprecated = deprecated;
        }

        public void OnBefore(object sender, IHttpContext httpContext)
        {
            if (sender is RouteResolveResponse rsv)
            {
                var options = httpContext.Container.Resolve<WebApiVersioningModuleOptions>();
                if (options == null)
                    throw new ArgumentNullException(nameof(WebApiVersioningModuleOptions), $"{nameof(WebApiVersioningModuleOptions)} is null, you should use AddWebApiVersion method");
                var versions = rsv.Attributes
                    .Where(f => f.GetType() == typeof(ApiVersionAttribute))
                    .Select(f => ((ApiVersionAttribute)f).Version)
                    .ToList();
                var requestApiVersion = httpContext.Request.Headers["X-Api-Version"];
                if (string.IsNullOrEmpty(requestApiVersion))
                    httpContext.Request.Headers.Add("X-Api-Version", options.DefaultVersion);
                requestApiVersion = httpContext.Request.Headers["X-Api-Version"];

                var isInvalidVersion = false;

                if (!versions.Any(f => f == requestApiVersion))
                    isInvalidVersion = true;
                else if (requestApiVersion == Version && Deprecated)
                    isInvalidVersion = true;

                if (isInvalidVersion)
                {
                    var triggerOnValidEvent = options.GetType().GetMethod(WebApiVersioningConstant.TriggerOnVersionException, BindingFlags.Instance | BindingFlags.NonPublic);
                    if (triggerOnValidEvent != null)
                    {
                        triggerOnValidEvent.Invoke(options, new object[] { requestApiVersion, requestApiVersion == Version ? Deprecated : false, httpContext });
                    }
                }
            }

        }

        public void OnAfter(object sender, IHttpContext httpContext)
        {

        }
    }
}
