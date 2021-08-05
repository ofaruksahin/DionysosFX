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

                if (string.IsNullOrWhiteSpace(body))
                    body = "{}";

                foreach (var routeItem in routeItems)
                {
                    try
                    {
                        if (routeItem.QueryString.Count != context.Request.QueryString.Count)
                            continue;
                        var invokeParameters = routeItem.Invoke.GetParameters().ToList();
                        if (instance == null)
                        {
                            List<object> _ctorParameters = new List<object>();
                            foreach (var item in routeItem.ConstructorParameters)
                            {
                                try
                                {
                                    var ctParam = context.Container.Resolve(item.ParameterType);
                                    _ctorParameters.Add(ctParam);
                                }
                                catch (Exception)
                                {
                                    _ctorParameters.Add(null);
                                }
                            }
                            instance = Activator.CreateInstance(routeItem.EndpointType.GetTypeInfo(), _ctorParameters.ToArray());

                            if (instance == null)
                                instance = Activator.CreateInstance(routeItem.EndpointType.GetTypeInfo());
                        }

                        IEndpointFilter webApiFilter = (IEndpointFilter?)routeItem.Attributes.FirstOrDefault(f => f is IEndpointFilter);
                        webApiFilter?.OnBefore(context);
                        if (context.IsHandled)
                            break;
                        object invokeResult = null;
                        if (!invokeParameters.Any())
                        {
                            routeItem.SetHttpContext?.Invoke(instance, new[] { context });
                            invokeResult = routeItem.Invoke.Invoke(instance, null);
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
                            routeItem.SetHttpContext?.Invoke(instance, new[] { context });
                            invokeResult = routeItem.Invoke.Invoke(instance, _invokeParameters.ToArray());
                        }
                        if (invokeResult is IEndpointResult invkResult)
                            invkResult.ExecuteResponse(context);
                        if (context.IsHandled)
                            webApiFilter?.OnAfter(context);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        public void Dispose()
        {
        }
    }
}
