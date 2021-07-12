using DionysosFX.Swan.Net;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.Swan.Modules
{
    /// <summary>
    /// 
    /// </summary>
    public interface IWebModule : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        void Start(CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task HandleRequestAsync(IHttpContext context);
    }
}
