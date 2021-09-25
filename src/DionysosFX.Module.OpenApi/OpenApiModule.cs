using DionysosFX.Module.IWebApi;
using DionysosFX.Module.OpenApi.Attributes;
using DionysosFX.Module.OpenApi.Entities;
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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.Module.OpenApi
{
    /// <summary>
    /// OpenAPI Module
    /// </summary>
    internal class OpenApiModule : WebModuleBase
    {
        /// <summary>
        /// Documentation response
        /// </summary>
        internal DocumentationResponse DocumentationResponse = new DocumentationResponse();

        /// <summary>
        /// OpenAPI module started was  and trigged this method
        /// </summary>
        /// <param name="cancellationToken"></param>
        public override void Start(CancellationToken cancellationToken)
        {
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
                        versions.Add(controllerApiVersionAttr.Version);
                    var endpoints = OpenApiExtension.GetEndpoints(controller);
                    foreach (var endpoint in endpoints)
                    {
                        if (endpoint.IsNotMapped())
                            continue;
                        var endpointVersionAttr = endpoint.GetCustomAttribute<ApiVersionAttribute>();
                        if (endpointVersionAttr != null)
                        {
                            if (!versions.Any(f => f == endpointVersionAttr.Version))
                                versions.Add(endpointVersionAttr.Version);
                        }
                    }
                }
            }

            if (!versions.Any())
                versions.Add("1.0.0.0");

            List<OpenApiDocument> documents = new List<OpenApiDocument>();
            foreach (var version in versions)
            {
                var document = new OpenApiDocument();
                document.Components = new OpenApiComponents();
                document.Components.Schemas = new Dictionary<string, OpenApiSchema>();
                var info = new OpenApiInfo();
                info.Version = version;
                info.Title = "";
                info.Description = "";

                OpenApiContact contact = new OpenApiContact();
                contact.Name = "";
                contact.Email = "";
                info.Contact = contact;

                OpenApiLicense license = new OpenApiLicense();
                license.Name = "";
                //license.Url = new Uri("http://*");
                document.Info = info;

                var servers = new List<OpenApiServer>();
                var server = new OpenApiServer();
                server.Url = "http://*:80";
                document.Servers = servers;

                var paths = new OpenApiPaths();

                foreach (var controller in controllers)
                {
                    if (controller.IsNotMapped())
                        continue;
                    bool isDeprecated = false;
                    var controllerName = controller.Name;
                    var controllerDescription = string.Empty;
                    var controllerApiVersionAttr = controller.GetCustomAttribute<ApiVersionAttribute>();
                    if (controllerApiVersionAttr != null && controllerApiVersionAttr.Version != version)
                        continue;
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
                        OpenApiOperation apiOperation = new OpenApiOperation();
                        apiOperation.RequestBody = new OpenApiRequestBody();
                        apiOperation.Deprecated = isDeprecated;
                        apiOperation.Summary = endpointDescription;
                        apiOperation.Tags.Add(new OpenApiTag() { Name = controllerName });

                        var responseTypeAttrs = endpoint.GetAttributes<ResponseTypeAttribute>();

                        foreach (var responseTypeAttr in responseTypeAttrs)
                        {
                            OpenApiResponse apiResponse = new OpenApiResponse();
                            apiResponse.Description = responseTypeAttr.Description;

                            OpenApiSchema schema = new OpenApiSchema();
                            schema.Properties = new Dictionary<string, OpenApiSchema>();
                            schema.Type = "object";
                            schema.Properties = OpenApiExtension.GetOpenApiSchema(responseTypeAttr.Type);

                            if (!document.Components.Schemas.ContainsKey(responseTypeAttr.Type.GetName()))
                                document.Components.Schemas.Add(new KeyValuePair<string, OpenApiSchema>(responseTypeAttr.Type.GetName(), schema));

                            OpenApiMediaType mediaType = new OpenApiMediaType();
                            mediaType.Schema = new OpenApiSchema();
                            mediaType.Schema.Items = new OpenApiSchema();
                            mediaType.Schema.Items.Reference = new OpenApiReference();
                            mediaType.Schema.Items.Reference.Type = ReferenceType.Schema;
                            mediaType.Schema.Items.Reference.Id = responseTypeAttr.Type.GetName();
                            apiResponse.Content.Add(new KeyValuePair<string, OpenApiMediaType>(responseTypeAttr.Type.GetName(), mediaType));

                            apiOperation.Responses.Add(((int)responseTypeAttr.StatusCode).ToString(), apiResponse);
                        }

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
                                        OpenApiParameter apiParameter = new OpenApiParameter();
                                        apiParameter.Name = parameter.Name;
                                        apiParameter.Description = parameter.Description;
                                        apiParameter.In = ParameterLocation.Query;
                                        apiParameter.Required = true;
                                        apiOperation.Parameters.Add(apiParameter);
                                        break;
                                    case JsonDataAttribute:
                                    case FormDataAttribute:
                                        apiOperation.RequestBody.Content = new Dictionary<string, OpenApiMediaType>();

                                        OpenApiSchema schema = new OpenApiSchema();
                                        schema.Properties = new Dictionary<string, OpenApiSchema>();
                                        schema.Type = methodParameter.ParameterType.IsArray() ? "array" : "object";
                                        schema.Properties = OpenApiExtension.GetOpenApiSchema(methodParameter.ParameterType);

                                        OpenApiMediaType jsonMediaType = new OpenApiMediaType();
                                        jsonMediaType.Schema = new OpenApiSchema();
                                        jsonMediaType.Schema.Items = new OpenApiSchema();
                                        jsonMediaType.Schema.Items.Reference = new OpenApiReference();
                                        jsonMediaType.Schema.Items.Reference.Type = ReferenceType.Schema;
                                        jsonMediaType.Schema.Items.Reference.Id = methodParameter.ParameterType.GetName();

                                        if (!document.Components.Schemas.ContainsKey(methodParameter.ParameterType.GetName()))
                                            document.Components.Schemas.Add(new KeyValuePair<string, OpenApiSchema>(methodParameter.ParameterType.GetName(), schema));
                                        if (converter is JsonDataAttribute)
                                            apiOperation.RequestBody.Content.Add(new KeyValuePair<string, OpenApiMediaType>("application/json", jsonMediaType));
                                        else
                                            apiOperation.RequestBody.Content.Add(new KeyValuePair<string, OpenApiMediaType>("multipart/form-data", jsonMediaType));
                                        break;
                                }
                            }
                        }

                        switch (endpointRouteAttr.Verb)
                        {
                            case HttpVerb.GET:
                                operations.Add(OperationType.Get, apiOperation);
                                break;
                            case HttpVerb.POST:
                                operations.Add(OperationType.Post, apiOperation);
                                break;
                            case HttpVerb.PUT:
                                operations.Add(OperationType.Put, apiOperation);
                                break;
                            case HttpVerb.DELETE:
                                operations.Add(OperationType.Delete, apiOperation);
                                break;
                            case HttpVerb.PATCH:
                                operations.Add(OperationType.Patch, apiOperation);
                                break;
                        }

                        pathItem.Operations = operations;
                        paths.Add(route, pathItem);
                    }

                }


                document.Paths = paths;

                var a = OpenApiSerializableExtensions.SerializeAsJson(document, OpenApiSpecVersion.OpenApi3_0);
                File.WriteAllText(@"C:\Users\Faruk\Desktop\test\swagger2.json", a);
                documents.Add(document);
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
        }

        public override void Dispose()
        {
            DocumentationResponse = null;
        }
    }
}
