using Autofac;
using DionysosFX.Module.IWebApi;
using DionysosFX.Module.WebApi;
using DionysosFX.Module.WebApi.JSON;
using DionysosFX.Swan.DataAnnotations;
using DionysosFX.Swan.Extensions;
using DionysosFX.Swan.Routing;
using System;

namespace DionysosFX.Module.OpenApi
{
    /// <summary>
    /// OpenAPI controller
    /// </summary>
    [Route("/open-api")]
    [NotMapped]
    internal class OpenApiController :WebApiController
    {
        [Route(HttpVerb.GET,"")]
        public IEndpointResult Get()
        {
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
