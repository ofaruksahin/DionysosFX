using Autofac;
using DionysosFX.Swan.HttpMultipart;
using DionysosFX.Swan.Modules;
using DionysosFX.Swan.Net;
using DionysosFX.Swan.Routing;
using Newtonsoft.Json;
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
            catch (Exception)
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
                object instance = null;
                string body = string.Empty;
                using (StreamReader reader = new StreamReader(context.Request.InputStream))
                {
                    body = reader.ReadToEnd();
                }

                if (string.IsNullOrWhiteSpace(body))
                    body = "{}";

                foreach (var routeItem in routeItems)
                {
                    if (routeItem.QueryString.Count != context.Request.QueryString.Count)
                        continue;
                    var invokeParameters = routeItem.Invoke.GetParameters().ToList();
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
                            instance = Activator.CreateInstance(routeItem.EndpointType.GetTypeInfo(), _ctorParameters.ToArray());
                            break;
                        }

                        if (instance == null)
                            instance = Activator.CreateInstance(routeItem.EndpointType.GetTypeInfo());
                    }

                    IEndpointFilter webApiFilter = (IEndpointFilter?)routeItem.Attributes.FirstOrDefault(f=>f is IEndpointFilter);
                    webApiFilter?.OnBefore(context);
                    if (context.IsHandled)
                        break;
                    if (!invokeParameters.Any())
                    {
                        routeItem.SetHttpContext?.Invoke(instance, new[] { context});
                        routeItem.Invoke.Invoke(instance, null);
                    }
                    else
                    {
                        List<object> _invokeParameters = new List<object>();
                        foreach (var invokeParameter in invokeParameters)
                        {
                            var customAttributes = invokeParameter.GetCustomAttributes();
                            var attribute = customAttributes.FirstOrDefault(f =>
                                f.GetType() == typeof(FormDataAttribute) ||
                                f.GetType() == typeof(JsonDataAttribute) ||
                                f.GetType() == typeof(QueryDataAttribute)
                                );

                            if (attribute is FormDataAttribute)
                            {
                                _invokeParameters.Add(context.ToFormObject(invokeParameter.ParameterType));
                            }
                            if (attribute is JsonDataAttribute)
                            {
                                _invokeParameters.Add(JsonConvert.DeserializeObject(body, invokeParameter.ParameterType));
                            }
                            if (attribute is QueryDataAttribute)
                            {
                                _invokeParameters.Add(Convert.ChangeType(context.Request.QueryString[invokeParameter.Name], invokeParameter.ParameterType));
                            }
                        }
                        routeItem.SetHttpContext?.Invoke(instance, new[] { context });
                        routeItem.Invoke.Invoke(instance, _invokeParameters.ToArray());
                    }
                    if (context.IsHandled)
                        webApiFilter?.OnAfter(context);
                }
            }
        }

        public void Dispose()
        {
        }
    }
}
