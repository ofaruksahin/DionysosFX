using DionysosFX.Module.OpenApi.Attributes;
using DionysosFX.Module.WebApi;
using DionysosFX.Module.WebApi.EnpointResults;
using DionysosFX.Swan.Routing;
using DionysosFX.Template.WebAPI.Constants;
using DionysosFX.Template.WebAPI.Entities;
using DionysosFX.Template.WebAPI.IService;
using DionysosFX.Template.WebAPI.WebApiFilters;
using System.Net;

namespace DionysosFX.Template.WebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("/user")]
    [AuthorizeFilter]
    [ControllerDescription("Controller Description")]
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
        [Route(HttpVerb.GET, "/list")]
        [EndpointDescription("Get User List")]
        public IEndpointResult List()
        {
            var list = _userService.GetAll();
            return new Ok(list);
        }

        /// <summary>
        /// Get user from id
        /// </summary>
        /// <param name="id"></param>
        [Route(HttpVerb.GET, "/get/{id}")]
        public IEndpointResult Get([QueryData] int id)
        {
            var user = _userService.Get(id);
            if (user != null)
                return new Ok(new BaseResult<User>(user, Messages.Success, HttpStatusCode.OK));
            else
                return new NotFound(new BaseResult<User>(null, Messages.Error, HttpStatusCode.NotFound));
        }

        /// <summary>
        /// Insert a new user
        /// </summary>
        /// <param name="entity"></param>
        [Route(HttpVerb.POST, "/insert")]
        public void Insert([JsonData] User entity)
        {
            _userService.Insert(entity);
        }

        /// <summary>
        /// Update a user with id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        [Route(HttpVerb.PUT, "/update")]
        public void Update([QueryData] int id, [JsonData] User entity)
        {
            _userService.Update(id, entity);
        }

        /// <summary>
        /// Delete user with id
        /// </summary>
        /// <param name="id"></param>
        [Route(HttpVerb.DELETE, "/delete")]
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
