using DionysosFX.Swan.Net;

namespace DionysosFX.Module.IWebApi
{
    public interface IEndpointResult
    {
        void ExecuteResponse(IHttpContext Context);
    }
}
