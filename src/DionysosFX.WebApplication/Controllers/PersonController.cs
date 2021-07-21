using DionysosFX.Swan.Routing;
using DionysosFX.WebApplication.IRepository;
using System.Threading.Tasks;

namespace DionysosFX.WebApplication.Controllers
{
    public class PersonController : BaseController
    {
        IUserRepository _userService;

        public PersonController(IUserRepository userService)
        {
            _userService = userService;
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
