using System;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.Host
{
    /// <summary>
    /// 
    /// </summary>
    public interface IWebServer : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        event EventHandler<WebServerStateChangeEventArgs> StateChanged;
        /// <summary>
        /// 
        /// </summary>
        event EventHandler<OnFatalExceptionEventArgs> OnFatalException;
        /// <summary>
        /// 
        /// </summary>
        WebServerState State 
        { 
            get;
            set; 
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task RunAsync(CancellationToken cancellationToken = default);
    }
}
