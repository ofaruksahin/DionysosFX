using System;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX
{
    public interface IWebServer : IDisposable
    {
        event EventHandler<WebServerStateChangeEventArgs> StateChanged;
        WebServerState State { get; }
        Task RunAsync(CancellationToken cancellationToken = default);
    }
}
