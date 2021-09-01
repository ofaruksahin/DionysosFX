using Autofac;
using DionysosFX.Module.WebApi;
using DionysosFX.Module.WebApi.EnpointResults;
using DionysosFX.Swan.DataAnnotations;
using DionysosFX.Swan.Extensions;
using DionysosFX.Swan.Modules;
using DionysosFX.Swan.Routing;
using System;

namespace DionysosFX.Module.OpenApi
{
    [Route("/open-api")]
    [NotMapped]
    internal class OpenApiController :WebApiController
    {
        [Route(HttpVerb.GET,"")]
        public IEndpointResult Get()
        {
            if (!Container.TryResolve(out WebApiModuleOptions apiModuleOptions))
                return new InternalServerError();
            if (apiModuleOptions.ResponseType != ResponseType.Json)
                return new InternalServerError();
            if(!Container.TryResolve(out OpenApiModuleOptions openApiOptions))
                return new InternalServerError();
            if (!Container.TryResolve(out OpenApiModule openApiModule))
                return new InternalServerError();
            openApiModule.DocumentationResponse.ApplicationName = openApiOptions.ApplicationName;
            Context.AddCacheExpire(TimeSpan.FromHours(1).TotalSeconds);
            return new Ok(openApiModule.DocumentationResponse);
        }
    }
}
