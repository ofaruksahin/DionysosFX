using Autofac;
using DionysosFX.Module.OpenApi.Attributes;
using DionysosFX.Module.OpenApi.Entities;
using DionysosFX.Module.WebApi;
using DionysosFX.Module.WebApi.Attributes;
using DionysosFX.Module.WebApi.EnpointResults;
using DionysosFX.Module.WebApiVersioning;
using DionysosFX.Swan.DataAnnotations;
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
    internal class OpenApiModule : IWebModule
    {
        internal DocumentationResponse DocumentationResponse = new DocumentationResponse();
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
                        var controllerNotMappedAttributes = controller.GetAttributes<NotMappedAttribute>();
                        if (controllerNotMappedAttributes.Any())
                            continue;
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
                            if (DocumentationResponse.Versions.Any(f => f.Version == controllerVersion.Version))
                                continue;
                            DocumentationResponse.Versions.Add(new VersionItem()
                            {
                                Version = controllerVersion.Version,
                                Deprecated = controllerVersion.Deprecated
                            });
                        }

                        var endpoints = controller.GetMethods(BindingFlags.Instance | BindingFlags.Public).Where(f => f.IsRoute()).ToList();
                        foreach (var endpoint in endpoints)
                        {
                            var endpointNotMappedAttributes = endpoint.GetAttributes<NotMappedAttribute>();
                            if (endpointNotMappedAttributes.Any())
                                continue;
                            EndpointItem endpointItem = new EndpointItem();
                            var routeAttribute = endpoint.GetCustomAttribute(typeof(RouteAttribute));
                            if (routeAttribute == null)
                                throw new Exception($"{endpoint.Name} Route Attribute not found.");

                            endpointItem.Name = string.Format("{0}{1}", routePrefix, ((RouteAttribute)routeAttribute).Route);
                            if (endpointItem.Name.Contains("{"))
                                endpointItem.Name = endpointItem.Name.Substring(0, endpointItem.Name.IndexOf("{"));
                            endpointItem.Verb = ((RouteAttribute)routeAttribute).Verb.ToString();

                            var endpointDescriptionAttribute = endpoint.GetCustomAttribute(typeof(DescriptionAttribute));
                            if (endpointDescriptionAttribute != null)
                                endpointItem.Description = ((DescriptionAttribute)endpointDescriptionAttribute).Description;

                            var versioningAttributes = endpoint.GetAttributes<ApiVersionAttribute>();
                            foreach (var version in versioningAttributes)
                            {
                                if (endpointItem.Versions.Any(f => f.Version == version.Version))
                                    continue;
                                endpointItem.Versions.Add(new VersionItem()
                                {
                                    Version = version.Version,
                                    Deprecated = version.Deprecated
                                });
                                if (!DocumentationResponse.Versions.Any(f => f.Version == version.Version))
                                    DocumentationResponse.Versions.Add(new VersionItem()
                                    {
                                        Version = version.Version,
                                        Deprecated = version.Deprecated
                                    });
                            }

                            endpointItem.Versions.AddRange(DocumentationResponse.Versions.Where(f => endpointItem.Versions.Any(y => f.Version != y.Version)).ToList());
                            endpointItem.Versions = endpointItem.Versions.OrderBy(f => f.Version).ToList();
                            
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
        }

        public void Dispose()
        {
            DocumentationResponse = null;
        }
    }
}
