using Autofac;
using DionysosFX.Swan.Modules;
using DionysosFX.Swan.Net;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.Module.Cors
{
    /// <summary>
    /// Cors module
    /// </summary>
    internal class CorsModule : IWebModule
    {
        /// <summary>
        /// When module was started than trigged start
        /// </summary>
        /// <param name="cancellationToken"></param>
        public void Start(CancellationToken cancellationToken)
        {
        }

        /// <summary>
        /// Handle web request after then execute method
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task HandleRequestAsync(IHttpContext context)
        {
            if (!context.Container.TryResolve(out CorsModuleOptions options))
                throw new Exception("Cors Module Options is not defined");
            context.Response.Headers[CorsModuleConstants.AccessControlAllowCredentials] = "true";
            context.Response.Headers[CorsModuleConstants.AccessControlAllowHeaders] = string.Join(',', options.AllowedHeaders);
            context.Response.Headers[CorsModuleConstants.AccessControlAllowMethods] = string.Join(',', options.AllowedMethods);
            context.Response.Headers[CorsModuleConstants.AccessControlAllowOrigin] = string.Join(',', options.AllowedOrigins);
            context.Response.Headers[CorsModuleConstants.AccessControlMaxAge] = options.MaxAge.ToString();
        }

        public void Dispose()
        {
        }
    }
}
