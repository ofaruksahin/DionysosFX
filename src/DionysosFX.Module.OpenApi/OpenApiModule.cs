using Autofac;
using DionysosFX.Module.OpenApi.Attributes;
using DionysosFX.Module.OpenApi.Entities;
using DionysosFX.Module.WebApi;
using DionysosFX.Module.WebApi.Attributes;
using DionysosFX.Module.WebApi.EnpointResults;
using DionysosFX.Module.WebApiVersioning;
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
using static DionysosFX.Module.OpenApi.Entities.SchemaItem;

namespace DionysosFX.Module.OpenApi
{
    public class OpenApiModule : IWebModule
    {
        DocumentationResponse DocumentationResponse = new DocumentationResponse();
        public void Start(CancellationToken cancellationToken)
        {            
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            List<Type> schemaTypes = new List<Type>();
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes().Where(f => f.IsWebApiController()).ToList();
                if (types.Any())
                {
                    foreach (var controller in types)
                    {
                        var controllerItem = new ControllerItem();
                        controllerItem.Name = controller.Name;

                        var controllerDescriptionAttribute = controller.GetCustomAttribute(typeof(DescriptionAttribute));
                        if (controllerDescriptionAttribute != null)
                            controllerItem.Description = ((DescriptionAttribute)controllerDescriptionAttribute).Description;

                        var controllerRouteAttribute = controller.GetCustomAttribute(typeof(RouteAttribute));

                        string routePrefix = string.Empty;
                        if (controllerRouteAttribute != null)
                            routePrefix = ((RouteAttribute)controllerRouteAttribute).Route;

                        if (!string.IsNullOrEmpty(routePrefix) && !routePrefix.StartsWith("/"))
                            routePrefix = string.Format("/{0}", routePrefix);

                        var controllerVersions = controller.GetAttributes<ApiVersionAttribute>();

                        foreach (var controllerVersion in controllerVersions)
                        {
                            if (DocumentationResponse.Versions.Any(f => f == controllerVersion.Version))
                                continue;
                            DocumentationResponse.Versions.Add(controllerVersion.Version);
                        }

                        var endpoints = controller.GetMethods(BindingFlags.Instance | BindingFlags.Public).Where(f => f.IsRoute()).ToList();
                        foreach (var endpoint in endpoints)
                        {
                            EndpointItem endpointItem = new EndpointItem();
                            var routeAttribute = endpoint.GetCustomAttribute(typeof(RouteAttribute));
                            if (routeAttribute == null)
                                throw new Exception($"{endpoint.Name} Route Attribute not found.");
                            else
                            {
                                endpointItem.Name = string.Format("{0}{1}", routePrefix, ((RouteAttribute)routeAttribute).Route);
                                if (endpointItem.Name.Contains("{"))
                                    endpointItem.Name = endpointItem.Name.Substring(0, endpointItem.Name.IndexOf("{"));
                                endpointItem.Verb = ((RouteAttribute)routeAttribute).Verb.ToString();
                            }
                            var endpointDescriptionAttribute = endpoint.GetCustomAttribute(typeof(DescriptionAttribute));
                            if (endpointDescriptionAttribute != null)
                                endpointItem.Description = ((DescriptionAttribute)endpointDescriptionAttribute).Description;

                            var versioningAttributes = endpoint.GetAttributes<ApiVersionAttribute>();
                            foreach (var version in versioningAttributes)
                            {
                                if (endpointItem.Versions.Any(f => f == version.Version))
                                    continue;
                                endpointItem.Versions.Add(version.Version);
                                if (!DocumentationResponse.Versions.Any(f => f == version.Version))
                                    DocumentationResponse.Versions.Add(version.Version);
                            }

                            var methodParameters = endpoint.GetParameters();

                            var parameterAttributes = endpoint.GetAttributes<ParameterAttribute>();
                            endpointItem.Parameters = parameterAttributes
                                .Select(f => new ParameterItem(f.Name, f.Description))
                                .ToList();

                            foreach (var item in endpointItem.Parameters)
                            {
                                var methodParameter = methodParameters.FirstOrDefault(f => f.Name == item.Name);
                                if (methodParameter == null)
                                    continue;
                                item.TypeName = methodParameter.ParameterType.GetName();
                                if (!methodParameter.ParameterType.IsSystemType() && !schemaTypes.Any(f => f.FullName == methodParameter.ParameterType.FullName))
                                    schemaTypes.Add(methodParameter.ParameterType);
                                var convertAttribute = methodParameter.GetCustomAttributes()
                                  .Where(f => f.GetType().GetInterface(nameof(IParameterConverter)) != null)
                                  .FirstOrDefault();
                                item.PrefixType = convertAttribute.GetType().FullName;
                            }

                            var responseTypeAttributes = endpoint.GetAttributes<ResponseTypeAttribute>();
                            endpointItem.ResponseTypes = responseTypeAttributes
                                .Select(f => new ResponseTypeItem(f.StatusCode, f.Type.GetName(), f.Description))
                                .ToList();

                            List<Type> responseGenericTypes = new List<Type>();
                            foreach (var responseType in responseTypeAttributes)
                                responseType.Type.GetGenericTypesRecursive(ref responseGenericTypes);

                            schemaTypes.AddRange(responseGenericTypes.Where(f => schemaTypes.Any(y => y.FullName != f.FullName)).Select(f => f));

                            controllerItem.Endpoints.Add(endpointItem);
                        }
                        DocumentationResponse.Controllers.Add(controllerItem);
                    }
                }
            }

            foreach (var schemaType in schemaTypes)
            {
                SchemaItem schemaItem = new SchemaItem(schemaType.FullName);
                var schemaDescriptionAttribute = schemaType.GetAttributes<DescriptionAttribute>().FirstOrDefault();
                if (schemaDescriptionAttribute != null)
                    schemaItem.Description = schemaDescriptionAttribute.Description;
                schemaItem.SchemaProperties = schemaType
                    .GetProperties()
                    .Select(f => new SchemaPropertyItem()
                    {
                        Name = f.Name,
                        Type = f.PropertyType.FullName,
                        Description = (f.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute)?.Description
                    }).ToList();
                DocumentationResponse.Schemas.Add(schemaItem);
            }
        }

        public async Task HandleRequestAsync(IHttpContext context)
        {
            if (context.IsHandled)
                return;
            var apiModuleOptions = context.Container.Resolve<WebApiModuleOptions>();
            if (apiModuleOptions == null || apiModuleOptions.ResponseType != ResponseType.Json)
                throw new Exception("OpenAPI supported the only json");
            var openApiOptions = context.Container.Resolve<OpenApiModuleOptions>();
            if (openApiOptions == null)
                throw new Exception("OpenAPI Options not found");
            DocumentationResponse.ApplicationName = openApiOptions.ApplicationName;
            if (context.Request.Url.LocalPath == "/open-api" && context.Request.HttpMethod == "GET")
            {
                context.Response.Headers["Access-Control-Allow-Origin"] = "*";
                var expireTime = TimeSpan.FromHours(1).TotalSeconds;
                context.AddCacheExpire(expireTime);
                new Ok(DocumentationResponse).ExecuteResponse(context);
            }
        }

        public void Dispose()
        {
            DocumentationResponse = null;
        }
    }
}
