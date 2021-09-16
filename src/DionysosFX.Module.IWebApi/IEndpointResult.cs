using DionysosFX.Swan.Net;

namespace DionysosFX.Module.IWebApi
{
    /// <summary>
    /// Endpoint result interface
    /// </summary>
    public interface IEndpointResult
    {
        void ExecuteResponse(IHttpContext Context);
    }
}
