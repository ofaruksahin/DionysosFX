using DionysosFX.Swan.Routing;
using System.Threading.Tasks;

namespace DionysosFX.WebApplication.Controllers
{
    public class PersonController : BaseController
    {
        public PersonController(string qwe)
        {

        }

        [Route(HttpVerb.GET,"list")]
        public async Task<bool> GetUsers()
        {
            return true;
        }

        [Route(HttpVerb.GET,"get/{id}/{age}")]
        public async Task<bool> GetUser()
        {
            return true;
        }
    }
}
