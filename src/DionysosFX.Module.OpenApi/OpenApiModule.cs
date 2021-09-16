using Autofac;
using DionysosFX.Module.OpenApi.Entities;
using DionysosFX.Module.WebApi;
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

namespace DionysosFX.Module.OpenApi
{
    /// <summary>
    /// OpenAPI Module
    /// </summary>
    internal class OpenApiModule : IWebModule
    {
        /// <summary>
        /// Documentation response
        /// </summary>
        internal DocumentationResponse DocumentationResponse = new DocumentationResponse();
        
        /// <summary>
        /// OpenAPI module started was  and trigged this method
        /// </summary>
        /// <param name="cancellationToken"></param>
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
                        var controllerItem = controller.ToController();

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
                            var endpointItem = endpoint.ToEndpoint(routePrefix,schemaTypes);

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
                            controllerItem.Endpoints.Add(endpointItem);
                        }
                        DocumentationResponse.Controllers.Add(controllerItem);
                    }
                }
            }

            foreach (var schemaType in schemaTypes)
            {
                var schemaItem = schemaType.ToSchemaItem();
                DocumentationResponse.Schemas.Add(schemaItem);
            }
        }

        /// <summary>
        /// OpenAPI module handle request and trigged this method
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
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
