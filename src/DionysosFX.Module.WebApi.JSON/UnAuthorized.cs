using DionysosFX.Module.IWebApi;
using DionysosFX.Swan.Net;
using DionysosFX.Swan.Routing;
using System.Net;

namespace DionysosFX.Module.WebApi.JSON
{
    public class UnAuthorized : EndpointResult
    {
        public UnAuthorized(object result = null) : base(result)
        {
            StatusCode = (int)HttpStatusCode.Unauthorized;
        }

        public override void ExecuteResponse(IHttpContext Context)
        {
            base.ExecuteResponse(Context);
        }
    }
}
