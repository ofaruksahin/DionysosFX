using Autofac;
using DionysosFX.Swan.Modules;
using DionysosFX.Swan.Net;
using DionysosFX.Swan.Routing;
using System;
using System.Collections.Generic;
using System.IO;
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
                string body = string.Empty;
                foreach (var routeItem in routeItems)
                {
                    var invokeParameters = routeItem.Invoke.GetParameters().ToList();
                    if (!invokeParameters.Any() && (context.Request.QueryString.Count > 0 || context.Request.Form.Count > 0 || context.Request.Files.Count > 0))
                    {
                        if (context.Request.ContentType != "application/x-www-form-urlencoded")
                            continue;
                        if (string.IsNullOrEmpty(body))
                        {
                            using (StreamReader reader = new StreamReader(context.Request.InputStream))
                            {
                                body = reader.ReadToEnd();
                            }
                        }

                        if (string.IsNullOrEmpty(body))
                            continue;
                    }
                    if (instance == null)
                    {
                        var constructors = routeItem.EndpointType.GetConstructors();
                        foreach (var constructor in constructors)
                        {
                            List<object> _ctorParameters = new List<object>();
                            var ctorParameters = constructor.GetParameters();
                            foreach (var ctorParameter in ctorParameters)
                            {
                                try
                                {
                                    var ctParam = context.Container.Resolve(ctorParameter.ParameterType);
                                    _ctorParameters.Add(ctParam);
                                }
                                catch (Exception)
                                {
                                    _ctorParameters.Add(null);
                                }
                            }
                            instance = (WebApiController)Activator.CreateInstance(routeItem.EndpointType.GetTypeInfo(), _ctorParameters.ToArray());
                            break;
                        }

                        if (instance == null)
                            instance = (WebApiController)Activator.CreateInstance(routeItem.EndpointType.GetTypeInfo());
                    }


                    if (!invokeParameters.Any())
                    {
                        routeItem.Invoke.Invoke(instance, null);
                    }
                }
            }
        }

        public void Dispose()
        {
        }
    }
}
