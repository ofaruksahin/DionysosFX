using DionysosFX.Module.WebApi;
using DionysosFX.Swan.Routing;
using DionysosFX.Template.WebAPI.Entities;
using DionysosFX.Template.WebAPI.IService;

namespace DionysosFX.Template.WebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("/user")]
    public class UserController : WebApiController, IController<User>
    {
        IUserService _userService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userService"></param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get all user
        /// </summary>
        [Route(HttpVerb.GET,"/list")]
        public void List()
        {
            var list = _userService.GetAll();
        }

        /// <summary>
        /// Get user from id
        /// </summary>
        /// <param name="id"></param>
        [Route(HttpVerb.GET,"/get")]
        public void Get([QueryData] int id)
        {
            var user = _userService.Get(id);
            if (user != null)
            {

            }
        }

        /// <summary>
        /// Insert a new user
        /// </summary>
        /// <param name="entity"></param>
        [Route(HttpVerb.POST,"/insert")]
        public void Insert([JsonData] User entity)
        {
            _userService.Insert(entity);
        }

        /// <summary>
        /// Update a user with id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        [Route(HttpVerb.PUT,"/update")]
        public void Update([QueryData] int id, [JsonData] User entity)
        {
            _userService.Update(id, entity);
        }

        /// <summary>
        /// Delete user with id
        /// </summary>
        /// <param name="id"></param>
        [Route(HttpVerb.DELETE,"/delete")]
        public void Delete([QueryData] int id)
        {
            var user = _userService.Get(id);
            if (user != null)
            {
                _userService.Delete(user);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        [Route(HttpVerb.PUT,"/upload/{id}")]
        public void UploadFile([QueryData]int id,[FormData]User entity)
        {

        }
    }
}
