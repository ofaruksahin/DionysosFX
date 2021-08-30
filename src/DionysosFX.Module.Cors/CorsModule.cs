using Autofac;
using DionysosFX.Swan.Modules;
using DionysosFX.Swan.Net;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.Module.Cors
{
    public class CorsModule : IWebModule
    {
        public void Start(CancellationToken cancellationToken)
        {
        }

        public async Task HandleRequestAsync(IHttpContext context)
        {
            if (context.Request.HttpMethod == "OPTIONS")
            {
                var options = context.Container.Resolve<CorsModuleOptions>();
                if (options == null)
                    throw new Exception("Cors Module Options is not defined");
                var defaultPolicy = options.CorsPolicies.FirstOrDefault(f => f.IsDefault);
                if (defaultPolicy != null)
                {
                    context.Response.Headers[CorsModuleConstants.AccessControlAllowCredentials] = "true";
                    context.Response.Headers[CorsModuleConstants.AccessControlAllowHeaders] = string.Join(',', defaultPolicy.AllowedHeaders);
                    context.Response.Headers[CorsModuleConstants.AccessControlAllowMethods] = string.Join(',', defaultPolicy.AllowedMethods);
                    context.Response.Headers[CorsModuleConstants.AccessControlAllowOrigin] = string.Join(',', defaultPolicy.AllowedOrigins);
                    context.Response.Headers[CorsModuleConstants.AccessControlMaxAge] = defaultPolicy.MaxAge.ToString();
                    context.SetHandled();
                }
            }
        }
        public void Dispose()
        {
        }
    }
}
