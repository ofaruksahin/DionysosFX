using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX
{
    public partial class WebServer : WebServerBase<WebServerOptions>
    {
        HttpListener Listener;

        public WebServer()
        {
            Options.AddUrlPrefix("http://*:80");

            Listener = CreateHttpListener();
        }

        public WebServer([NotNull] WebServerOptions options) : base(options)
        {
            Listener = CreateHttpListener();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    Listener = null;
                }
                catch
                {

                }
            }

            base.Dispose(disposing);
        }

        protected override void Prepare(CancellationToken cancellationToken)
        {
            base.Prepare(cancellationToken);
        }       


        protected override Task ProcessRequestsAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        protected override void OnFatalException()
        {
            throw new System.NotImplementedException();
        }

        private HttpListener CreateHttpListener()
        {
            return new HttpListener();
        }
    }
}
