using DionysosFX.Swan.Net;

namespace DionysosFX.Swan.Routing
{
    public interface IEndpointFilter
    {
        void OnBefore(object sender, IHttpContext httpContext);
        void OnAfter(object sender, IHttpContext httpContext);
    }
}
