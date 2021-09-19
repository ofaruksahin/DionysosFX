using Autofac;
using DionysosFX.Module.IWebApi;
using DionysosFX.Swan.Extensions;
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
    /// <summary>
    /// Web Api Module 
    /// </summary>
    internal class WebApiModule : IWebModule
    {
        /// <summary>
        /// Web api routes
        /// </summary>
        List<RouteResolveResponse> routes = new List<RouteResolveResponse>();

        /// <summary>
        /// Static file module started was  and trigged this method
        /// </summary>
        /// <param name="cancellationToken"></param>
        public void Start(CancellationToken cancellationToken)
        {
            try
            {
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                foreach (var assembly in assemblies)
                {
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
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// Static file module handle request and trigged this method
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
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

                IDictionary<RouteResolveResponse, List<object>> routeItemsDictionary = new Dictionary<RouteResolveResponse, List<object>>();
                foreach (var routeItem in routeItems)
                {
                    if (routeItems.Count() > 1 && routeItem.QueryString.Count != context.Request.QueryString.Count)
                        continue;
                    if (instance == null || instance.GetType() != routeItem.EndpointType)
                    {
                        routeItem.ConstructorParameters = routeItem.ConstructorParameters ?? (new List<ParameterInfo>());
                        instance = routeItem.ConstructorParameters.CreateInstance(context, routeItem.EndpointType.GetTypeInfo());
                        //instance = CreateInstance(routeItem, context);
                    }

                    routeItem.SetHttpContext?.Invoke(instance, new[] { context });
                    routeItem.SetContainer?.Invoke(instance, new[] { context.Container });
                    List<object> _invokeParameters = new List<object>();
                    foreach (var invokeParameter in routeItem.InvokeParameters)
                    {
                        var customAttributes = invokeParameter.GetCustomAttributes();
                        var attribute = customAttributes.FirstOrDefault(f => f.GetType().GetInterface(typeof(IParameterConverter).Name) != null);
                        if (attribute is IParameterConverter converter)
                            _invokeParameters.Add(converter.Convert(context, routeItem, invokeParameter));
                    }

                    if (_invokeParameters.Count == routeItem.InvokeParameters.Count)
                    {
                        routeItemsDictionary.Add(routeItem, _invokeParameters);
                    }
                }

                if (instance != null)
                {
                    var endpointItem = routeItemsDictionary.Where(f => f.Value.Count == routeItemsDictionary.Values.Max(f => f.Count)).FirstOrDefault();
                    if (endpointItem.Key != null)
                    {
                        endpointItem.Key.Parameters = endpointItem.Value;
                        var endpointFilters = endpointItem.Key.Attributes.Where(f => f is IEndpointFilter).ToList();
                        foreach (IEndpointFilter item in endpointFilters)
                        {
                            if (context.IsHandled)
                                break;
                            item?.OnBefore(endpointItem.Key, context);
                        }
                        object invokeResult = null;
                        if (!context.IsHandled)
                            invokeResult = endpointItem.Key.Invoke.Invoke(instance, endpointItem.Value.ToArray());
                        if (invokeResult is IEndpointResult invkResult)
                            invkResult.ExecuteResponse(context);
                        else if (invokeResult is Task<IEndpointResult> invkTask)
                            (await invkTask).ExecuteResponse(context);
                        foreach (IEndpointFilter item in endpointFilters)
                        {
                            if (context.IsHandled)
                                item?.OnAfter(endpointItem.Key, context);
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
