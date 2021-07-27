using DionysosFX.Module.WebApi;
using DionysosFX.Template.WebAPI.Entities;
using DionysosFX.Template.WebAPI.IService;

namespace DionysosFX.Template.WebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
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
        public void List()
        {
            var list = _userService.GetAll();
        }

        /// <summary>
        /// Get user from id
        /// </summary>
        /// <param name="id"></param>
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
        public void Insert([JsonData] User entity)
        {
            _userService.Insert(entity);
        }

        /// <summary>
        /// Update a user with id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        public void Update([QueryData] int id, [JsonData] User entity)
        {
            _userService.Update(id, entity);
        }

        /// <summary>
        /// Delete user with id
        /// </summary>
        /// <param name="id"></param>
        public void Delete([QueryData] int id)
        {
            var user = _userService.Get(id);
            if (user != null)
            {
                _userService.Delete(user);
            }
        }
    }
}
