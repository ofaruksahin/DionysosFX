using DionysosFX.Module.FluentValidator.Lab.Entities;
using DionysosFX.Module.IWebApi;
using DionysosFX.Module.WebApi;
using DionysosFX.Module.WebApi.JSON;
using DionysosFX.Swan.Routing;

namespace DionysosFX.Module.FluentValidator.Lab.Controllers
{
    [Route("/user")]
    [FluentValidator]
    public class UserController : WebApiController
    {
        [Route(HttpVerb.POST,"/insert")]
        public IEndpointResult Insert([JsonData] User user)
        {
            return new Ok(new { });
        }
    }
}
