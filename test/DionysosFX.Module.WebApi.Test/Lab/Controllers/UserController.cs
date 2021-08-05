using DionysosFX.Module.Test;
using DionysosFX.Module.WebApi.EnpointResults;
using DionysosFX.Module.WebApi.Test.Lab.Entities;
using DionysosFX.Module.WebApi.Test.Lab.IService;
using DionysosFX.Swan.Routing;
using System.Collections.Generic;
using System.Net;

namespace DionysosFX.Module.WebApi.Test.Lab.Controllers
{
    [Route("/user")]
    public class UserController : WebApiController
    {
        IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
            _userService.Initialize();
        }

        [Route(HttpVerb.GET, "/list")]
        public IEndpointResult List()
        {
            var users = _userService.GetAll();
            return new Ok(new BaseResult<List<User>>(users, Messages.Success, HttpStatusCode.OK));
        }

        [Route(HttpVerb.GET, "/list/{id}")]
        public IEndpointResult Get([QueryData] int id)
        {
            var user = _userService.Get(id);
            if (user != null)
                return new Ok(new BaseResult<User>(user, Messages.Success, HttpStatusCode.OK));
            else
                return new NotFound(new BaseResult<User>(null, Messages.Error, HttpStatusCode.NotFound));
        }

        [Route(HttpVerb.POST, "/insert")]
        public IEndpointResult Insert([JsonData] User user)
        {
            var id = _userService.Insert(user);
            if (id > 0)
                return new Ok(new BaseResult<int>(id, Messages.Error, HttpStatusCode.OK));
            else
                return new NotFound(new BaseResult<object>(new { }, Messages.Error, HttpStatusCode.NotFound));
        }

        [Route(HttpVerb.PATCH, "/update/{id}")]
        public IEndpointResult Update([QueryData] int id, [JsonData] User user)
        {
            var isUpdated = _userService.Update(id, user);
            if (isUpdated)
                return new Ok(new BaseResult<bool>(isUpdated,Messages.Success,HttpStatusCode.OK));
            else
                return new NotFound(new BaseResult<bool>(isUpdated,Messages.Success,HttpStatusCode.NotFound));
        }

        [Route(HttpVerb.DELETE, "/delete/{id}")]
        public IEndpointResult Delete([QueryData] int id)
        {
            bool isDeleted = _userService.Delete(id);
            if (isDeleted)
                return new Ok(new BaseResult<bool>(isDeleted, Messages.Success, HttpStatusCode.OK));
            else
                return new NotFound(new BaseResult<bool>(isDeleted, Messages.Success, HttpStatusCode.NotFound));
        }
    }
}
