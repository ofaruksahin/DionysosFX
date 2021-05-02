using DionysosFX.Host.Net.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.Host
{
    public partial class WebServer : WebServerBase<WebServerOptions>
    {
        IHttpListener Listener;

        public WebServer() : base(new WebServerOptions())
        {
            Options.AddUrlPrefix("http://*:80");

            Listener = CreateHttpListener();
        }

        public WebServer([NotNull] string prefix) : base(new WebServerOptions())
        {
            Options.AddUrlPrefix(prefix);
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
            Listener.Start();
        }


        protected override async Task ProcessRequestsAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested && (Listener?.IsListening ?? false))
            {
                var context = await Listener.GetContextAsync(cancellationToken);
                context.CancellationToken = cancellationToken;

                Task.Run(() => DoHandleContextAsync(context), cancellationToken);
            }
        }

        protected override void OnFatalException()
        {
        }

        private IHttpListener CreateHttpListener()
        {
            IHttpListener DoCreate() => new SystemHttpListener(new System.Net.HttpListener()) as IHttpListener;

            var listener = DoCreate();
            System.Console.WriteLine($"Running HttpListener: {listener.Name}");

            foreach (var prefix in Options.UrlPrefixes)
            {
                var urlPrefix = new string(prefix?.ToCharArray());

                if (!urlPrefix.EndsWith("/", StringComparison.Ordinal))
                    urlPrefix += "/";

                urlPrefix = urlPrefix.ToLowerInvariant();

                listener.AddPrefix(urlPrefix);

                Console.WriteLine($"Web server prefix '{prefix}' added.");
            }

            //TODO:
            //return new HttpListener();
            return listener;
        }
    }
}
