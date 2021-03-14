using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.Net.Internal
{
    internal class SystemHttpListener : IHttpListener
    {
        private readonly System.Net.HttpListener _httpListener;

        public SystemHttpListener(System.Net.HttpListener httpListener)
        {
            _httpListener = httpListener;
        }

        public List<string> Prefixes => _httpListener.Prefixes.ToList();

        public bool IsListening => _httpListener.IsListening;

        public string Name => $"DionysosFX Web Server";

        public void AddPrefix(string urlPrefix)
        {
            _httpListener.Prefixes.Add(urlPrefix);
        }

        public void Dispose()
        {
            ((IDisposable)_httpListener)?.Dispose();
        }


        public void Start()
        {
            _httpListener.Start();
        }

        public void Stop()
        {
            _httpListener.Stop();
        }
        public Task<IHttpContextImpl> GetContextAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
