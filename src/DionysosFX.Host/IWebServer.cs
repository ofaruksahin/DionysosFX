using System;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.Host
{
    public interface IWebServer : IDisposable
    {
        event EventHandler<WebServerStateChangeEventArgs> StateChanged;
        WebServerState State { get; set; }
        Task RunAsync(CancellationToken cancellationToken = default);
    }
}
