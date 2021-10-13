using Autofac;
using DionysosFX.Module.IWebApi;
using DionysosFX.Module.WebApi;
using DionysosFX.Module.WebApi.JSON;
using DionysosFX.Swan.Routing;
using System.Collections.Generic;

namespace DionysosFX.Module.HealthCheck
{
    [Route("/api/healthcheck")]
    public class HealthCheckController : WebApiController
    {
        [Route(HttpVerb.GET, "")]
        public IEndpointResult HealthCheck()
        {
            if (!Container.TryResolve(out HealthCheckModule module))
                return new NotFound(new { });
            List<HealthCheckResponse> responses = new List<HealthCheckResponse>();
            foreach (var healthCheck in module.HealthChecks)
                responses.Add(healthCheck.IsHealthily());
            return new Ok(responses);
        }
    }
}
