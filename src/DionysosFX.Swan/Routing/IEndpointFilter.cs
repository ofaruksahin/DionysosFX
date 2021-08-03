using DionysosFX.Swan.Net;

namespace DionysosFX.Swan.Routing
{
    public interface IEndpointFilter
    {
        void OnBefore(IHttpContext httpContext);
        void OnAfter(IHttpContext httpContext);
    }
}
