using DionysosFX.Module.IWebApi;
using DionysosFX.Module.WebApi;
using DionysosFX.Module.WebApi.JSON;
using DionysosFX.Swan.Routing;

namespace DionysosFX.ProjectTemplate.WebAPI.Controllers
{
    [Route("/users")]
    public class UserController : WebApiController
    {
        [Route(HttpVerb.GET,"/")]
        public IEndpointResult ListUser()
        {
            return new Ok(UserData.Instance.Users);
        }
    }
}
