using DionysosFX.Module.Test;
using DionysosFX.Module.WebApi.EnpointResults;
using DionysosFX.Swan.Routing;
using System.Collections.Generic;
using System.Net;

namespace DionysosFX.Module.WebApi.Lab
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
            return new Ok(new BaseResult<List<User>>()
            {
                Data = users
            });
        }

        [Route(HttpVerb.GET, "/list/{id}")]
        public IEndpointResult Get([QueryData] int id)
        {
            var user = _userService.Get(id);
            if (user != null)
                return new Ok(new BaseResult<User>()
                {
                    Data = user
                });
            else
                return new NotFound(new BaseResult<User>()
                {
                    Data = null,
                    Message = Messages.Error,
                    StatusCode = HttpStatusCode.NotFound
                });
        }

        [Route(HttpVerb.POST, "/insert")]
        public IEndpointResult Insert([JsonData] User user)
        {
            var id = _userService.Insert(user);
            if (id > 0)
                return new Ok(new BaseResult<int>()
                {
                    Data = id
                });
            else
                return new NotFound(new BaseResult<object>()
                {
                    Message = Messages.Error,
                    StatusCode = HttpStatusCode.NotFound
                });
        }

        [Route(HttpVerb.PATCH, "/update/{id}")]
        public IEndpointResult Update([QueryData] int id, [JsonData] User user)
        {
            var isUpdated = _userService.Update(id, user);
            if (isUpdated)
                return new Ok(new BaseResult<bool>()
                {
                    Data = isUpdated
                });
            else
                return new NotFound(new BaseResult<bool>()
                {
                    Data = isUpdated,
                    Message = Messages.Error,
                    StatusCode = HttpStatusCode.NotFound
                });
        }

        [Route(HttpVerb.DELETE, "/delete/{id}")]
        public IEndpointResult Delete([QueryData] int id)
        {
            bool isDeleted = _userService.Delete(id);
            if (isDeleted)
                return new Ok(new BaseResult<bool>()
                {
                    Data = isDeleted
                });
            else
                return new NotFound(new BaseResult<bool>()
                {
                    Data = isDeleted,
                    Message = Messages.Error,
                    StatusCode = HttpStatusCode.NotFound
                });
        }
    }
}
