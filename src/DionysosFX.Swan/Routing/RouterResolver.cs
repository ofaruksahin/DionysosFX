using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DionysosFX.Swan.Routing
{
    public static class RouterResolver
    {
        
        public static List<RouteResolveResponse> RouteResolve(this Type @this)
        {
            List<RouteResolveResponse> resolveResponses = new List<RouteResolveResponse>();
            var mainAttr = @this.GetCustomAttribute(typeof(RouteAttribute));
            string prefix = string.Empty;
            if (mainAttr != null)
                prefix = ((RouteAttribute)mainAttr).Route;

            var endpoints = @this.GetMethods(BindingFlags.Instance | BindingFlags.Public).ToList();
            if (endpoints.Any())
            {
                foreach (var endpoint in endpoints)
                {
                    var isRouteMethod = endpoint.IsRoute();
                    if (isRouteMethod)
                    {
                        var attr = endpoint.GetCustomAttribute(typeof(RouteAttribute));
                        if (attr != null)
                        {
                            RouteResolveResponse routeResolveResponse = new RouteResolveResponse();
                            var routeAttr = (RouteAttribute)attr;
                            if (!routeAttr.Route.StartsWith("/"))
                                routeResolveResponse.Route = string.Format("/{0}", routeAttr.Route);
                            else
                                routeResolveResponse.Route = routeAttr.Route;
                            routeResolveResponse.Verb = routeAttr.Verb;
                            routeResolveResponse.Invoke = endpoint;
                            routeResolveResponse.Route = prefix + routeResolveResponse.Route;
                            if (routeResolveResponse.Route.Contains("{"))
                            {
                                var bracketIndex = routeResolveResponse.Route.IndexOf("{");
                                var queries = routeResolveResponse.Route.Substring(routeResolveResponse.Route.IndexOf("{"));
                                var parameters = queries.Split('{','}').ToList();
                                parameters.RemoveAll(f => string.IsNullOrEmpty(f) || f == "/");
                                routeResolveResponse.QueryString.AddRange(parameters);
                                routeResolveResponse.Route = routeResolveResponse.Route.Substring(0,bracketIndex);
                                routeResolveResponse.Route = routeResolveResponse.Route.TrimEnd('/');
                            }
                            var isUrl = Uri.IsWellFormedUriString(routeResolveResponse.Route, UriKind.RelativeOrAbsolute);
                            if (!isUrl)
                                throw new UrlFormatException(routeResolveResponse.Route);
                            routeResolveResponse.EndpointType = @this;
                            resolveResponses.Add(routeResolveResponse);
                        }
                    }
                }
            }
            return resolveResponses;
        }
    }
}
