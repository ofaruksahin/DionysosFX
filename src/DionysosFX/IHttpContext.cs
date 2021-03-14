using System.Net;
using System.Threading;

namespace DionysosFX
{
    public interface IHttpContext
    {
        string Id { get; }
        CancellationToken CancellationToken { get; }
        IHttpRequest Request { get; }
        IHttpResponse Response { get; }
        bool IsHandled { get; }
        void SetHandled();
        void Close();
    }
}
