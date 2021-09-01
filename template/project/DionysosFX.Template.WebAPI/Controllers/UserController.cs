using DionysosFX.Module.Cors;
using DionysosFX.Module.OpenApi.Attributes;
using DionysosFX.Module.WebApi;
using DionysosFX.Module.WebApi.EnpointResults;
using DionysosFX.Module.WebApiVersioning;
using DionysosFX.Swan.Routing;
using DionysosFX.Template.WebAPI.Constants;
using DionysosFX.Template.WebAPI.Entities;
using DionysosFX.Template.WebAPI.IService;
using DionysosFX.Template.WebAPI.WebApiFilters;
using System.Collections.Generic;
using System.Net;

namespace DionysosFX.Template.WebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("/user")]
    [AuthorizeFilter]
    [Description("Controller Description")]
    [ApiVersion("1.0.0.0")]
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
        [Description("Get User List")]
        [ResponseType(HttpStatusCode.OK, typeof(List<User>), "User List")]
        [ResponseType(HttpStatusCode.NotFound, typeof(List<User>), "User List")]
        [ApiVersion("1.0.0.2")]
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
        [Description("Get User From Id")]
        [Parameter("id",  "User Id")]
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
        [Parameter("entity","User Item")]
        public IEndpointResult Insert([JsonData] User entity)
        {
            _userService.Insert(entity);
            return new Ok(entity);
        }

        /// <summary>
        /// Update a user with id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        [Route(HttpVerb.PUT, "/update")]
        [Parameter("id","User Id")]
        [Parameter("entity","User entity")]
        public IEndpointResult Update([QueryData] int id, [FormData] User entity)
        {
            _userService.Update(id, entity);
            return new Ok(entity);
        }

        /// <summary>
        /// Delete user with id
        /// </summary>
        /// <param name="id"></param>
        [Route(HttpVerb.DELETE, "/delete")]
        public IEndpointResult Delete([QueryData] int id)
        {
            var user = _userService.Get(id);
            if (user != null)
            {
                _userService.Delete(user);
                return new Ok(user);
            }
            else
            {
                return new NotFound(new { });
            }
        }
    }
}
