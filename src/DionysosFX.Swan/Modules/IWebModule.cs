using DionysosFX.Swan.Net;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.Swan.Modules
{
    /// <summary>
    /// DionysosFX Base Module
    /// </summary>
    public interface IWebModule : IDisposable
    {
        /// <summary>
        /// This method triggering when module was starting
        /// </summary>
        void Start(CancellationToken cancellationToken);

        /// <summary>
        /// This method triggering when handle web request
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task HandleRequestAsync(IHttpContext context);
    }
}
