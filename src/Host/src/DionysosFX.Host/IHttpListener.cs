using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.Host
{
    public interface IHttpListener : IDisposable
    {
        List<string> Prefixes { get; }
        bool IsListening { get; }
        string Name { get; }
        void Start();
        void Stop();
        void AddPrefix(string urlPrefix);
        Task<IHttpContextImpl> GetContextAsync(CancellationToken cancellationToken);
    }
}
