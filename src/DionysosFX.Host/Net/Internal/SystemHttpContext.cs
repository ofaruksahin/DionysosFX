using Autofac;
using DionysosFX.Swan.Net;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace DionysosFX.Host.Net.Internal
{
    /// <summary>
    /// Use it to store the content of request
    /// </summary>
    internal sealed class SystemHttpContext : IHttpContextImpl
    {
        /// <summary>
        /// Store the content of Default .net request
        /// </summary>
        private readonly System.Net.HttpListenerContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public SystemHttpContext([NotNull]System.Net.HttpListenerContext context,CancellationToken cancellationToken)
        {
            _context = context;
            _cancellationToken = cancellationToken;

            _response = new SystemHttpResponse(this,context.Response);
            _request = new SystemHttpRequest(context.Request);             
        }

        /// <summary>
        /// Unique key of the request
        /// </summary>
        private string _id = Guid.NewGuid().ToString();

        /// <summary>
        /// Unique key of the request
        /// </summary>
        public string Id => _id;

        /// <summary>
        /// Used to cancel request
        /// </summary>
        private CancellationToken _cancellationToken;

        /// <summary>
        /// Used to cancel request
        /// </summary>
        public CancellationToken CancellationToken { get => _cancellationToken; }

        /// <summary>
        /// Store the content of request
        /// </summary>
        private IHttpRequest _request;

        /// <summary>
        /// Store to content of request
        /// </summary>
        public IHttpRequest Request => _request;

        /// <summary>
        /// Store to content response
        /// </summary>
        private IHttpResponse _response;

        /// <summary>
        /// Store to content response
        /// </summary>
        public IHttpResponse Response => _response;

        /// <summary>
        /// Is the request processed ?
        /// </summary>
        private bool _isHandled;

        /// <summary>
        /// Is the request processed ?
        /// </summary>
        public bool IsHandled => _isHandled;

        /// <summary>
        /// Dependency injection container (AutoFac)
        /// </summary>
        private IContainer _container;
        public IContainer Container
        {
            get => _container;
            set => _container = value;
        }

        /// <summary>
        /// Request close method
        /// </summary>
        public void Close()
        {
            Response.Close();
        }

        /// <summary>
        /// Request set handled method
        /// </summary>
        public void SetHandled()
        {
            if (_isHandled)
                return;
            _isHandled = true;
        }
    }
}
