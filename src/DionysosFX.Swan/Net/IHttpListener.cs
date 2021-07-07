using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.Swan.Net
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHttpListener : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        IReadOnlyList<string> Prefixes { get; }
        /// <summary>
        /// 
        /// </summary>
        bool IsListening { get; }
        /// <summary>
        /// 
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 
        /// </summary>
        void Start();
        /// <summary>
        /// 
        /// </summary>
        void Stop();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="urlPrefix"></param>
        void AddPrefix(string urlPrefix);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IHttpContextImpl> GetContextAsync(CancellationToken cancellationToken);
    }
}
