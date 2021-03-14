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
        }

        private string _id = Guid.NewGuid().ToString();
        public string Id => _id;

        private CancellationToken _cancellationToken = new CancellationToken();
        public CancellationToken CancellationToken => _cancellationToken;

        private IHttpRequest _request;
        public IHttpRequest Request => _request;

        private IHttpResponse _response;
        public IHttpResponse Response => _response;

        private bool _isHandled;
        public bool IsHandled => _isHandled;

        public void Close()
        {
            //Response.Close();
        }

        public void SetHandled()
        {
            if (IsHandled)
                return;
            _isHandled = true;
        }
    }
}
