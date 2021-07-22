using DionysosFX.Module.WebApi;
using DionysosFX.Swan.Routing;
using DionysosFX.WebApplication.Dtos;
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
        public async Task<bool> GetUser([QueryData]int id,[QueryData]int age)
        {
            return true;
        }

        [Route(HttpVerb.GET,"/get/{id}/{age}/{name}")]
        public async Task<bool> GetUser([QueryData] int id,[QueryData]int age,[QueryData]string name)
        {
            return true;
        }

        [Route(HttpVerb.GET,"/insert")]
        public async Task<bool> Insert([JsonData]User user)
        {
            return true;
        }
    }
}
