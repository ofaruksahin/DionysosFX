using DionysosFX.Swan.Net;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.Host.Net.Internal
{
    /// <summary>
    /// 
    /// </summary>
    internal class SystemHttpListener : IHttpListener
    {
        /// <summary>
        /// 
        /// </summary>
        private System.Net.HttpListener _listener;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listener"></param>
        public SystemHttpListener(System.Net.HttpListener listener)
        {
            _listener = listener;
            _prefixes = new();
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsListening => _listener == null ? false : _listener.IsListening;

        /// <summary>
        /// 
        /// </summary>
        public string Name { get => "DionysosFX Web Server"; }

        /// <summary>
        /// 
        /// </summary>
        private List<string> _prefixes;

        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyList<string> Prefixes => _prefixes;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        public void AddPrefix(string prefix)
        {
            _prefixes.Add(prefix);            
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IHttpContextImpl> GetContextAsync(CancellationToken cancellationToken)
        {
            System.Net.HttpListenerContext context = null;
            try
            {
                context = await _listener.GetContextAsync().ConfigureAwait(false);
            }
            catch (OperationCanceledException e) when(cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine(e.Message);
            }
            return new SystemHttpContext(context,cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Start()
        {
            _listener.Prefixes.Clear();
            foreach(var prefix in _prefixes)
                _listener.Prefixes.Add(prefix);
            _listener.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Stop()
        {
            _listener.Stop();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            _listener = null;
        }
    }
}
