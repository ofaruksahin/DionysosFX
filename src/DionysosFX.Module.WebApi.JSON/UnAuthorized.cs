using DionysosFX.Module.IWebApi;
using DionysosFX.Swan.Net;
using System.Net;

namespace DionysosFX.Module.WebApi.JSON
{
    /// <summary>
    /// Json UnAuthorized
    /// </summary>
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
