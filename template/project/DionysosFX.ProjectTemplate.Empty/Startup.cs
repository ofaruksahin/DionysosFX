using DionysosFX.Host;
using DionysosFX.Swan;
using System.Threading;
using System.Threading.Tasks;

namespace App
{
    public class Startup : IStartup
    {
        IHostBuilder hostBuilder;

        public Startup(IHostBuilder hostBuilder)
        {
            this.hostBuilder = hostBuilder;
        }

        public void Configure()
        {
            hostBuilder.AddPrefix("http://*:1923");
        }

        public void Build()
        {
            hostBuilder.BuildContainer();

            using (var cts = new CancellationTokenSource())
                Task.WaitAll(RunWebServer(cts.Token));
        }

        private IWebServer CreateWebServer()
        {
            IWebServer webServer = new WebServer(hostBuilder);
            return webServer;
        }

        private async Task RunWebServer(CancellationToken cancellationToken)
        {
            using var server = CreateWebServer();
            await server.RunAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
