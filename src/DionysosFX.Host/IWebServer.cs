using System;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.Host
{
    /// <summary>
    /// DionysosFX Server base interface
    /// </summary>
    public interface IWebServer : IDisposable
    {
        /// <summary>
        /// Server state change method
        /// </summary>
        event EventHandler<WebServerStateChangeEventArgs> StateChanged;
        /// <summary>
        /// Server handle the exception and trigger a 'OnFatalException' event
        /// </summary>
        event EventHandler<OnFatalExceptionEventArgs> OnFatalException;

        /// <summary>
        /// Storage server status
        /// </summary>
        WebServerState State 
        { 
            get;
            set; 
        }

        /// <summary>
        /// Start listening server and listen to requests
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task RunAsync(CancellationToken cancellationToken = default);
    }
}
