using DionysosFX.Swan.Modules;
using DionysosFX.Swan.Net;
using DionysosFX.Swan.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.Module.WebApi
{
    public class WebApiModule : IWebModule
    {
        List<RouteResolveResponse> routes = new List<RouteResolveResponse>();
        Assembly assembly = null;
        public void Start(CancellationToken cancellationToken)
        {
            try
            {
                assembly = Assembly.GetEntryAssembly();
                var types = assembly.GetTypes();
                foreach (var ty in types)
                {
                    var isWebApiController = ty.IsWebApiController();
                    if (isWebApiController)
                    {
                        var routeResolveRepsonse = ty.RouteResolve();
                        foreach (var resolveResponse in routeResolveRepsonse)
                            routes.Add(resolveResponse);
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        public async Task HandleRequestAsync(IHttpContext context)
        {
            if (context.IsHandled)
                return;
            object _verb;
            if (Enum.TryParse(typeof(HttpVerb), context.Request.HttpMethod.ToUpper(), out _verb))
            {
                HttpVerb verb = (HttpVerb)_verb;
                var routeItems = routes.Where(f => f.Route == context.Request.Url.LocalPath && f.Verb == verb);
                WebApiController instance = null;
                foreach (var routeItem in routeItems)
                {
                    if (instance == null)
                    {
                        var constructors = routeItem.EndpointType.GetConstructors();
                        foreach (var constructor in constructors)
                        {
                            List<object> _ctorParameters = new List<object>();
                            var ctorParameters = constructor.GetParameters();
                            foreach (var ctorParameter in ctorParameters)
                            {
                                
                            }
                            //instance = (WebApiController)Activator.CreateInstance(routeItem.EndpointType.GetTypeInfo());
                        }
                    }
                }
            }
        }

        public void Dispose()
        {
        }
    }
}
