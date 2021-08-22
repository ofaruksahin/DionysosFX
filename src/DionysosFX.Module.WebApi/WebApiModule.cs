using Autofac;
using DionysosFX.Module.WebApi.EnpointResults;
using DionysosFX.Swan.Extensions;
using DionysosFX.Swan.HttpMultipart;
using DionysosFX.Swan.Modules;
using DionysosFX.Swan.Net;
using DionysosFX.Swan.Routing;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.Module.WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public class WebApiModule : IWebModule
    {
        List<RouteResolveResponse> routes = new List<RouteResolveResponse>();
        Assembly assembly = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
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

        /// <summary>
        /// 
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
                string body = string.Empty;
                using (StreamReader reader = new StreamReader(context.Request.InputStream))
                {
                    body = reader.ReadToEnd();
                }

                bool bodyIsNull = false;
                if (string.IsNullOrWhiteSpace(body))
                {
                    bodyIsNull = true;
                    body = "{}";
                }

                IDictionary<RouteResolveResponse, List<object>> routeItemsDictionary = new Dictionary<RouteResolveResponse, List<object>>();
                foreach (var routeItem in routeItems)
                {
                    if (routeItem.QueryString.Count != context.Request.QueryString.Count)
                        continue;
                    if (instance == null || instance.GetType() != routeItem.EndpointType)
                    {
                        instance = CreateInstance(routeItem, context);
                    }

                    routeItem.SetHttpContext?.Invoke(instance, new[] { context });
                    List<object> _invokeParameters = new List<object>();
                    foreach (var invokeParameter in routeItem.InvokeParameters)
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
                            if (bodyIsNull)
                                continue;
                            _invokeParameters.Add(JsonConvert.DeserializeObject(body, invokeParameter.ParameterType));
                        }
                        if (attribute is QueryDataAttribute)
                        {
                            if (!routeItem.QueryString.Any(f => f == invokeParameter.Name))
                                continue;
                            bool isArray = invokeParameter.IsArray();
                            if (isArray)
                            {
                                if (invokeParameter.ParameterType.GenericTypeArguments.Any())
                                    _invokeParameters.Add(context.Request.QueryString[invokeParameter.Name].Split(',').Select(f => f).ToList());
                                else
                                    _invokeParameters.Add(context.Request.QueryString[invokeParameter.Name].Split(',').Select(f => f).ToArray());
                            }
                            else
                            {
                                _invokeParameters.Add(Convert.ChangeType(context.Request.QueryString[invokeParameter.Name], invokeParameter.ParameterType));
                            }
                        }
                    }

                    if (_invokeParameters.Count == routeItem.InvokeParameters.Count)
                    {
                        _invokeParameters.RemoveAll(f => f == null);
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
                        foreach (IEndpointFilter item in endpointFilters)
                        {
                            if (context.IsHandled)
                                item?.OnAfter(endpointItem.Key, context);
                        }
                    }
                }
            }
        }

        private object CreateInstance(RouteResolveResponse routeItem, IHttpContext context)
        {
            object instance = null;

            List<object> constructorParameters = new List<object>();

            foreach (var item in routeItem.ConstructorParameters)
            {
                try
                {
                    var ctParam = context.Container.Resolve(item.ParameterType);
                    constructorParameters.Add(ctParam);
                }
                catch (Exception)
                {
                    constructorParameters.Add(null);
                }
            }

            instance = Activator.CreateInstance(routeItem.EndpointType.GetTypeInfo(), constructorParameters.ToArray());

            if (instance == null)
                instance = Activator.CreateInstance(routeItem.EndpointType.GetTypeInfo());

            return instance;
        }

        public void Dispose()
        {
        }
    }
}
