using DionysosFX.Swan.Net;

namespace DionysosFX.Module.WebApi
{
    public interface IWebApiFilter
    {
        void OnBefore(IHttpContext httpContext);
        void OnAfter(IHttpContext httpContext);
    }
}
