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
                context.Response.Headers[CorsModuleConstants.AccessControlAllowCredentials] = "true";
                context.Response.Headers[CorsModuleConstants.AccessControlAllowHeaders] = string.Join(',', options.AllowedHeaders);
                context.Response.Headers[CorsModuleConstants.AccessControlAllowMethods] = string.Join(',', options.AllowedMethods);
                context.Response.Headers[CorsModuleConstants.AccessControlAllowOrigin] = string.Join(',', options.AllowedOrigins);
                context.Response.Headers[CorsModuleConstants.AccessControlMaxAge] = options.MaxAge.ToString();
                context.SetHandled();
            }
        }
        public void Dispose()
        {
        }
    }
}
