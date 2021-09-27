using DionysosFX.Module.OpenApi.Attributes;
using DionysosFX.Module.WebApi;
using DionysosFX.Swan.Extensions;
using DionysosFX.Swan.Routing;
using HttpMultipartParser;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DionysosFX.Module.OpenApi
{
    internal static class OpenApiExtension
    {
        public static OpenApiDocument GetDocument(OpenApiModuleOptions options, string versionName)
        {
            var document = new OpenApiDocument();
            document.Components = new OpenApiComponents();
            document.Components.Schemas = new Dictionary<string, OpenApiSchema>();

            var info = new OpenApiInfo();
            info.Version = versionName;
            info.Title = options.Title;

            OpenApiContact contact = new OpenApiContact();
            contact.Name = options.ContactName;
            contact.Email = options.ContactEmail;
            info.Contact = contact;

            document.Info = info;

            document.Components.SecuritySchemes = new Dictionary<string, OpenApiSecurityScheme>();

            if (options.EnableBearerToken)
            {
                OpenApiSecurityScheme openApiSecurityScheme = new OpenApiSecurityScheme();
                openApiSecurityScheme.Type = SecuritySchemeType.Http;
                openApiSecurityScheme.Scheme = "bearer";
                openApiSecurityScheme.BearerFormat = "JWT";
                document.Components.SecuritySchemes.Add(new KeyValuePair<string, OpenApiSecurityScheme>("bearerAuth", openApiSecurityScheme));
            }

            return document;
        }

        public static List<Type> GetControllers()
        {
            List<Type> types = new List<Type>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var controllers = assembly
                    .GetTypes()
                    .Where(f => f.IsWebApiController())
                    .ToList();
                types.AddRange(controllers);
            }
            return types;
        }

        public static List<MethodInfo> GetEndpoints(Type controller)
        {
            List<MethodInfo> endpoints = new List<MethodInfo>();
            var methods = controller
                .GetMethods(BindingFlags.Instance | BindingFlags.Public)
                .Where(f => f.IsRoute())
                .ToList();
            endpoints.AddRange(methods);
            return endpoints;
        }

        public static IDictionary<string, OpenApiSchema> GetOpenApiProperties(Type type)
        {
            IDictionary<string, OpenApiSchema> schema = new Dictionary<string, OpenApiSchema>();

            if (type.IsArray())
                type = type.GetGenericArguments()[0];

            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                var openApiSchema = GetOpenApiPropertyTypes(property.PropertyType);

                schema.Add(property.Name, openApiSchema);
            }

            return schema;
        }

        public static OpenApiSchema GetOpenApiPropertyTypes(Type type)
        {
            OpenApiSchema openApiSchema = new OpenApiSchema();

            if (type == typeof(string) || type == typeof(DateTime) || type == typeof(DateTime?))
            {
                openApiSchema.Type = "string";
                if (type == typeof(DateTime) || type == typeof(DateTime?))
                {
                    openApiSchema.Items = new OpenApiSchema();
                    openApiSchema.Items.Type = "string";
                    openApiSchema.Items.Format = "date-time";
                }
            }
            else if (type == typeof(List<FilePart>) || type == typeof(FilePart))
            {
                openApiSchema.Items = new OpenApiSchema();
                openApiSchema.Type = "array";
                openApiSchema.Items.Type = "string";
                openApiSchema.Items.Format = "binary";
            }
            else if (type == typeof(int) || type == typeof(int?))
            {
                openApiSchema.Type = "integer";
                openApiSchema.Items = new OpenApiSchema();
                openApiSchema.Items.Type = "integer";
                openApiSchema.Items.Format = "int64";
            }
            else if (type == typeof(float) || type == typeof(float?) || type == typeof(double) || type == typeof(double?))
            {
                openApiSchema.Type = "number";
                openApiSchema.Items = new OpenApiSchema();
                openApiSchema.Items.Type = "number";
                if (type == typeof(float) || type == typeof(float?))
                    openApiSchema.Items.Format = "float";
                else
                    openApiSchema.Items.Format = "double";
            }
            else if (type == typeof(bool) || type == typeof(bool?))
            {
                openApiSchema.Type = "boolean";
            }
            else if (type.IsArray())
            {
                openApiSchema.Type = "array";
            }
            else
            {
                openApiSchema.Type = "object";
            }
            return openApiSchema;
        }

        public static OpenApiParameter GetOpenApiParameter(ParameterAttribute parameter)
        {
            var openApiParameter = new OpenApiParameter();
            openApiParameter.Name = parameter.Name;
            openApiParameter.Description = parameter.Description;
            openApiParameter.In = ParameterLocation.Query;
            openApiParameter.Required = true;
            return openApiParameter;
        }

        public static void AddHeaders(OpenApiOperation operation, OpenApiModuleOptions options)
        {
            foreach (var header in options.Headers)
            {
                OpenApiParameter openApiHeader = new OpenApiParameter();
                openApiHeader.Name = header;
                openApiHeader.In = ParameterLocation.Header;
                openApiHeader.Required = false;
                operation.Parameters.Add(openApiHeader);
            }
        }

        public static OpenApiMediaType GetOpenApiMediaType(Type type)
        {
            OpenApiMediaType mediaType = new OpenApiMediaType();
            mediaType.Schema = new OpenApiSchema();
            mediaType.Schema.Items = new OpenApiSchema();
            mediaType.Schema.Items.Reference = new OpenApiReference();
            mediaType.Schema.Items.Reference.Type = ReferenceType.Schema;
            mediaType.Schema.Items.Reference.Id = type.GetName();
            return mediaType;
        }

        public static void AddRequestBody(OpenApiDocument document, OpenApiOperation operation, Type type, string contentType)
        {
            var name = type.GetName();
            operation.RequestBody.Content = new Dictionary<string, OpenApiMediaType>();

            OpenApiSchema schmea = new OpenApiSchema();
            schmea.Properties = new Dictionary<string, OpenApiSchema>();
            schmea.Type = type.IsArray() ? "array" : "object";
            schmea.Properties = GetOpenApiProperties(type);

            var mediaType = GetOpenApiMediaType(type);

            if (!document.Components.Schemas.ContainsKey(name))
                document.Components.Schemas.Add(new KeyValuePair<string, OpenApiSchema>(name, schmea));

            operation.RequestBody.Content.Add(new KeyValuePair<string, OpenApiMediaType>(contentType, mediaType));
        }

        public static void AddResponse(OpenApiDocument document,OpenApiOperation operation,ResponseTypeAttribute responseTypeAttr)
        {
            OpenApiResponse response = new OpenApiResponse();
            response.Description = responseTypeAttr.Description;

            OpenApiSchema schema = new OpenApiSchema();
            schema.Properties = new Dictionary<string, OpenApiSchema>();
            schema.Type = "object";
            schema.Properties = GetOpenApiProperties(responseTypeAttr.Type);

            var name = responseTypeAttr.Type.GetName();

            if (!document.Components.Schemas.ContainsKey(name))
                document.Components.Schemas.Add(new KeyValuePair<string, OpenApiSchema>(name, schema));

            var mediaType = GetOpenApiMediaType(responseTypeAttr.Type);

            response.Content.Add(new KeyValuePair<string, OpenApiMediaType>(name, mediaType));
            string statusCode = ((int)responseTypeAttr.StatusCode).ToString();
            operation.Responses.Add(statusCode, response);
        }        
    }
}
