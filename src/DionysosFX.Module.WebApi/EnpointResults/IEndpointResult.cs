using DionysosFX.Swan.Net;

namespace DionysosFX.Module.WebApi.EnpointResults
{
    public interface IEndpointResult
    {
        void ExecuteResponse(IHttpContext Context);
    }
}
