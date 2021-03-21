using System.Net;
using System.Threading;

namespace DionysosFX
{
    public interface IHttpContext
    {
        string Id { get; }
        CancellationToken CancellationToken { get; set; }
        IHttpRequest Request { get; }
        IHttpResponse Response { get; }
        bool IsHandled { get; }
        void Close();
        void SetHandled();        
    }
}
