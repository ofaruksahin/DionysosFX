using DionysosFX.Module.WebApi;
using DionysosFX.Swan.Routing;
using System.Threading.Tasks;

namespace DionysosFX.WebApplication.Controllers
{    
    [Route("/person")]
    public class PersonController : WebApiController
    {
        [Route(HttpVerb.GET,"list")]
        public async Task<bool> GetUsers()
        {
            return true;
        }
    }
}
