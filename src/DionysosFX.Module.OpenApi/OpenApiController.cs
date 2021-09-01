using Autofac;
using DionysosFX.Module.OpenApi.Attributes;
using DionysosFX.Module.WebApi;
using DionysosFX.Module.WebApi.EnpointResults;
using DionysosFX.Swan.Extensions;
using DionysosFX.Swan.Modules;
using DionysosFX.Swan.Routing;
using System;

namespace DionysosFX.Module.OpenApi
{
    [Route("/open-api")]
    [NotMapped]
    public class OpenApiController :WebApiController
    {
        [Route(HttpVerb.GET,"")]
        public IEndpointResult Get()
        {
            if (!Container.TryResolve<WebApiModuleOptions>(out WebApiModuleOptions apiModuleOptions))
                return new NotFound();
            if (apiModuleOptions.ResponseType != ResponseType.Json)
                return new NotFound();
            if(!Container.TryResolve<OpenApiModuleOptions>(out OpenApiModuleOptions openApiOptions))
                return new NotFound();
            if (!Container.TryResolve<OpenApiModule>(out OpenApiModule openApiModule))
                return new NotFound();
            openApiModule.DocumentationResponse.ApplicationName = openApiOptions.ApplicationName;
            Context.AddCacheExpire(TimeSpan.FromHours(1).TotalSeconds);
            return new Ok(openApiModule.DocumentationResponse);
        }
    }
}
