using DionysosFX.Swan.Net;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace DionysosFX.Host.Net.Internal
{
    /// <summary>
    /// 
    /// </summary>
    internal sealed class SystemHttpContext : IHttpContextImpl
    {
        /// <summary>
        /// 
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
        /// 
        /// </summary>
        private string _id = Guid.NewGuid().ToString();
        /// <summary>
        /// 
        /// </summary>
        public string Id => _id;
        /// <summary>
        /// 
        /// </summary>
        private CancellationToken _cancellationToken;
        /// <summary>
        /// 
        /// </summary>
        public CancellationToken CancellationToken { get => _cancellationToken; }
        /// <summary>
        /// 
        /// </summary>
        private IHttpRequest _request;
        /// <summary>
        /// 
        /// </summary>
        public IHttpRequest Request => _request;
        /// <summary>
        /// 
        /// </summary>
        private IHttpResponse _response;
        /// <summary>
        /// 
        /// </summary>
        public IHttpResponse Response => _response;
        /// <summary>
        /// 
        /// </summary>
        private bool _isHandled;
        /// <summary>
        /// 
        /// </summary>
        public bool IsHandled => _isHandled;
        /// <summary>
        /// 
        /// </summary>
        public void Close()
        {
            Response.Close();
        }
        /// <summary>
        /// 
        /// </summary>
        public void SetHandled()
        {
            if (_isHandled)
                return;
            _isHandled = true;
        }
    }
}
