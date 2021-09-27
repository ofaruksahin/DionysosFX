using Autofac;
using DionysosFX.Module.IWebApi;
using DionysosFX.Module.OpenApi.Attributes;
using DionysosFX.Module.WebApi.JSON;
using DionysosFX.Module.WebApiVersioning;
using DionysosFX.Swan.Exceptions;
using DionysosFX.Swan.Extensions;
using DionysosFX.Swan.Modules;
using DionysosFX.Swan.Net;
using DionysosFX.Swan.Routing;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.Module.OpenApi
{
    /// <summary>
    /// OpenAPI Module
    /// </summary>
    internal class OpenApiModule : WebModuleBase
    {
        internal OpenApiModuleOptions options;

        private List<KeyValuePair<string, string>> _openApiVersions;
        internal List<KeyValuePair<string, string>> openApiVersions
        {
            get => _openApiVersions ?? (_openApiVersions = new List<KeyValuePair<string, string>>());
        }

        /// <summary>
        /// OpenAPI module started was  and trigged this method
        /// </summary>
        /// <param name="cancellationToken"></param>
        public override void Start(CancellationToken cancellationToken)
        {
            if (!Container.TryResolve<OpenApiModuleOptions>(out options))
                throw new OptionsNotFoundException(typeof(OpenApiModuleOptions).Name);

            List<string> versions = new List<string>();
            var controllers = OpenApiExtension.GetControllers();
            foreach (var controller in controllers)
            {
                if (controller.IsNotMapped())
                    continue;
                var controllerApiVersionAttr = controller.GetCustomAttribute<ApiVersionAttribute>();
                if (controllerApiVersionAttr != null)
                {
                    if (!versions.Any(f => f == controllerApiVersionAttr.Version))
                    {
                        string versionName = string.Format("{0}",
                            controllerApiVersionAttr.Version);                        
                        versions.Add(versionName);
                    }
                    var endpoints = OpenApiExtension.GetEndpoints(controller);
                    foreach (var endpoint in endpoints)
                    {
                        if (endpoint.IsNotMapped())
                            continue;
                        var endpointVersionAttr = endpoint.GetCustomAttribute<ApiVersionAttribute>();
                        if (endpointVersionAttr != null)
                        {
                            if (!versions.Any(f => f == endpointVersionAttr.Version))
                            {
                                string versionName = string.Format("{0}",
                                    endpointVersionAttr.Version);
                                versions.Add(versionName);
                            }
                        }
                    }
                }
            }

            if (!versions.Any())
                versions.Add("Main Version");

            List<OpenApiUrlItem> urlItems = new List<OpenApiUrlItem>();

            foreach (var version in versions)
            {
                var document = OpenApiExtension.GetDocument(options, version);
                var paths = new OpenApiPaths();

                foreach (var controller in controllers)
                {
                    if (controller.IsNotMapped())
                        continue;
                    bool isDeprecated = false;
                    var controllerName = controller.Name;
                    var controllerDescription = string.Empty;
                    var controllerAttrs = controller.GetCustomAttributes<ApiVersionAttribute>().ToList();
                    controllerAttrs.RemoveAll(f => f.Version != version);
                    var controllerApiVersionAttr = controllerAttrs.FirstOrDefault();

                    if (controllerApiVersionAttr != null)
                        isDeprecated = controllerApiVersionAttr.Deprecated;

                    var controllerDescriptionAttr = controller.GetCustomAttribute<DescriptionAttribute>();

                    if (controllerDescriptionAttr != null)
                        controllerDescription = controllerDescriptionAttr.Description;

                    var routePrefix = string.Empty;

                    var controllerRouteAttr = controller.GetCustomAttribute<RouteAttribute>();
                    if (controllerRouteAttr != null)
                        routePrefix = controllerRouteAttr.Route;

                    var endpoints = OpenApiExtension.GetEndpoints(controller);

                    if (controllerApiVersionAttr == null)
                        if (!endpoints.Any(f => f.GetCustomAttribute<ApiVersionAttribute>() != null))
                            continue;

                    foreach (var endpoint in endpoints)
                    {
                        if (endpoint.IsNotMapped())
                            continue;

                        var endpointApiVersionAttrs = endpoint.GetCustomAttributes<ApiVersionAttribute>().ToList();
                        endpointApiVersionAttrs.RemoveAll(f => f.Version != version);
                        var endpointApiVersionAttr = endpointApiVersionAttrs.FirstOrDefault();
                        if (controllerApiVersionAttr != null && controllerApiVersionAttr.Version != version && endpointApiVersionAttr == null)
                            continue;
                        if (endpointApiVersionAttr != null && endpointApiVersionAttr.Version != version && controllerApiVersionAttr == null)
                            continue;

                        var endpointRouteAttr = endpoint.GetCustomAttribute<RouteAttribute>();
                        if (endpointRouteAttr == null)
                            throw new AttributeNotFoundException(typeof(RouteAttribute).Name);

                        string endpointDescription = string.Empty;
                        var endpointDescriptionAttr = endpoint.GetCustomAttribute<DescriptionAttribute>();
                        if (endpointDescriptionAttr != null)
                            endpointDescription = endpointDescriptionAttr.Description;

                        string route = string.Format("{0}{1}", routePrefix, endpointRouteAttr.Route);
                        if (route.Contains("{"))
                        {
                            var indexOf = route.IndexOf("{");
                            route = route.Substring(0, indexOf);
                        }

                        if (route.EndsWith("/"))
                            route = route.TrimEnd('/');

                        var pathItem = new OpenApiPathItem();
                        pathItem.Summary = controllerDescription;

                        var operations = new Dictionary<OperationType, OpenApiOperation>();
                        OpenApiOperation operation = new OpenApiOperation();
                        operation.RequestBody = new OpenApiRequestBody();
                        operation.Deprecated = isDeprecated;
                        operation.Summary = endpointDescription;
                        operation.Tags.Add(new OpenApiTag() { Name = controllerName });

                        var responseTypeAttrs = endpoint.GetAttributes<ResponseTypeAttribute>();

                        foreach (var responseTypeAttr in responseTypeAttrs)
                            OpenApiExtension.AddResponse(document, operation, responseTypeAttr);

                        var methodParameters = endpoint.GetParameters();
                        var parameters = endpoint.GetAttributes<ParameterAttribute>();

                        foreach (var parameter in parameters)
                        {
                            var methodParameter = methodParameters.FirstOrDefault(f => f.Name == parameter.Name);
                            if (methodParameter == null)
                                continue;

                            var converter = methodParameter
                              .GetCustomAttributes()
                              .FirstOrDefault(f => f.GetType().BaseType != null && f.GetType().GetInterface(nameof(IParameterConverter)) != null);

                            if (converter != null)
                            {
                                switch (converter)
                                {
                                    case QueryDataAttribute:                                        
                                        operation.Parameters.Add(OpenApiExtension.GetOpenApiParameter(parameter));
                                        break;
                                    case JsonDataAttribute:
                                    case FormDataAttribute:
                                        OpenApiExtension.AddRequestBody(document, operation, methodParameter.ParameterType, converter is JsonDataAttribute ? "application/json" : "multipart/form-data");
                                        break;
                                }
                            }
                        }

                        switch (endpointRouteAttr.Verb)
                        {
                            case HttpVerb.GET:
                                operations.Add(OperationType.Get, operation);
                                break;
                            case HttpVerb.POST:
                                operations.Add(OperationType.Post, operation);
                                break;
                            case HttpVerb.PUT:
                                operations.Add(OperationType.Put, operation);
                                break;
                            case HttpVerb.DELETE:
                                operations.Add(OperationType.Delete, operation);
                                break;
                            case HttpVerb.PATCH:
                                operations.Add(OperationType.Patch, operation);
                                break;
                        }
                        
                        OpenApiExtension.AddHeaders(operation, options);
                        pathItem.Operations = operations;
                        paths.Add(route, pathItem);
                    }

                }


                document.Paths = paths;
                var openApiJson = OpenApiSerializableExtensions.SerializeAsJson(document, OpenApiSpecVersion.OpenApi3_0);
                openApiVersions.Add(new KeyValuePair<string, string>(version, openApiJson));
                urlItems.Add(new OpenApiUrlItem($"/swagger/{version}.json", version));
            }

            Assembly curr = Assembly.GetEntryAssembly();
            var location = Path.Combine(Path.GetDirectoryName(curr.Location), "swagger", "index.html");
            if (File.Exists(location))
            {
                var content = File.ReadAllText(location);
                content = content.Replace("$$0$$", JsonConvert.SerializeObject(urlItems));
                using (StreamWriter sw = new StreamWriter(location))
                {
                    sw.Write(content);
                    sw.Close();
                }
            }
        }

        /// <summary>
        /// OpenAPI module handle request and trigged this method
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task HandleRequestAsync(IHttpContext context)
        {
            if (context.IsHandled)
                return;
            if (context.Request.Url.AbsolutePath.StartsWith("/swagger", StringComparison.InvariantCulture))
            {
                var lastIndexOf = context.Request.Url.AbsolutePath.LastIndexOf("/")+1;
                var version = context.Request.Url.AbsolutePath.Substring(lastIndexOf);
                lastIndexOf = version.LastIndexOf('.');
                version = version.Substring(0,lastIndexOf);
                var json = openApiVersions.FirstOrDefault(f => f.Key == version);
                if (!string.IsNullOrEmpty(json.Value))
                {
                    context.Response.StatusCode = 200;
                    context.Response.ContentType = "application/json";
                    using (var writer = new StreamWriter(context.Response.OutputStream))
                        writer.WriteLine(json.Value);
                    context.SetHandled();
                }
            }
        }

        public override void Dispose()
        {
        }
    }
}
