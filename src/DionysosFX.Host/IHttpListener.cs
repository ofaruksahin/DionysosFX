using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.Host
{
    /// <summary>
    /// 
    /// </summary>
    internal interface IHttpListener : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        IReadOnlyList<string> Prefixes 
        { 
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        bool IsListening 
        { 
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name 
        { 
            get;
            set; 
        }
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
        /// <param name="prefix"></param>
        void AddPrefix(string prefix);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IHttpContextImpl> GetContextAsync(CancellationToken cancellationToken);
    }
}
