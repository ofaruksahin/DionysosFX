using DionysosFX.Swan.Net;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.Swan.Modules
{
    public interface IWebModuleCollection : IDisposable
    {
        /// <summary>
        /// DionysosFX Core add a anew module
        /// </summary>
        /// <param name="name"></param>
        /// <param name="module"></param>
        void Add(string name, IWebModule module);

        /// <summary>
        /// Starts the all module
        /// </summary>
        /// <param name="name"></param>
        void Start(string name,CancellationToken cancellationToken);

        /// <summary>
        /// Starts the all module
        /// </summary>
        void Start(CancellationToken cancellationToken);

        /// <summary>
        /// This method trigged when handle the web request
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task DispatchRequestAsync(IHttpContext context);
    }
}
