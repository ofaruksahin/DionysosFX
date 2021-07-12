using DionysosFX.Swan.Net;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.Swan.Modules
{
    public interface IWebModuleCollection : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="module"></param>
        void Add(string name, IWebModule module);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        void Start(string name,CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        void Start(CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task DispatchRequestAsync(IHttpContext context);
    }
}
