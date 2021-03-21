using System;
using System.Threading;

namespace DionysosFX.Net.Internal
{
    internal sealed class SystemHttpContext : IHttpContextImpl
    {
        private readonly System.Net.HttpListenerContext _context;

        public SystemHttpContext(System.Net.HttpListenerContext context)
        {
            _context = context;
            _request = new SystemHttpRequest(context.Request);
            _response = new SystemHttpResponse(context.Response);
        }

        private string _id = Guid.NewGuid().ToString();
        public string Id => _id;

        private CancellationToken _cancellationToken;
        public CancellationToken CancellationToken
        {
            get => _cancellationToken;
            set => _cancellationToken = value;
        }

        private IHttpRequest _request;
        public IHttpRequest Request => _request;

        private IHttpResponse _response;
        public IHttpResponse Response => _response;

        private bool _isHandled;
        public bool IsHandled => _isHandled;

        public void SetHandled()
        {
            if (IsHandled)
                return;
            _isHandled = true;
        }

        public void Close()
        {
            Response.Close();
        }
    }
}
