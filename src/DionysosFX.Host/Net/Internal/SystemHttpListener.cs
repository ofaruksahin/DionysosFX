using DionysosFX.Swan.Net;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.Host.Net.Internal
{
    /// <summary>
    /// This class listening web requests
    /// </summary>
    internal class SystemHttpListener : IHttpListener
    {
        /// <summary>
        /// .Net Default Request Listener object
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
        /// Listener Status
        /// </summary>
        public bool IsListening => _listener == null ? false : _listener.IsListening;

        /// <summary>
        /// Listener name
        /// </summary>
        public string Name { get => "DionysosFX Web Server"; }

        /// <summary>
        /// web prefixes
        /// </summary>
        private List<string> _prefixes;

        /// <summary>
        /// web prefixes
        /// </summary>
        public IReadOnlyList<string> Prefixes => _prefixes;

        /// <summary>
        /// Add a new prefix method
        /// </summary>
        /// <param name="prefix"></param>
        public void AddPrefix(string prefix)
        {
            _prefixes.Add(prefix);            
        }
       
        /// <summary>
        /// wait a new web request
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
        /// Start listener object and set isListening state true
        /// </summary>
        public void Start()
        {
            _listener.Prefixes.Clear();
            foreach(var prefix in _prefixes)
                _listener.Prefixes.Add(prefix);
            _listener.Start();
        }

        /// <summary>
        /// Stop listener
        /// </summary>
        public void Stop()
        {
            _listener.Stop();
        }

        /// <summary>
        /// Dispose listener
        /// </summary>
        public void Dispose()
        {
            _listener = null;
        }
    }
}
