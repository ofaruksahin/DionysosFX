using DionysosFX.Module.IWebApi;
using DionysosFX.Module.WebApi;
using DionysosFX.Module.WebApi.JSON;
using DionysosFX.Swan.Routing;

namespace DionysosFX.Module.WebApiVersioning.Lab.Controllers
{
    [Route("/user")]
    public class UserController : WebApiController
    {
        [Route("/list")]
        [ApiVersion("0.0.0.1", true)]
        [ApiVersion("0.0.0.2", true)]
        [ApiVersion("0.0.0.3", true)]
        [ApiVersion("0.0.0.4")]
        [ApiVersion("0.0.0.5")]
        public IEndpointResult List()
        {
            return new Ok(new { });
        }
    }
}
