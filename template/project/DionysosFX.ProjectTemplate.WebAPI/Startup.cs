using DionysosFX.Host;
using DionysosFX.Module.WebApi;
using DionysosFX.Swan;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.ProjectTemplate.WebAPI
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
            hostBuilder.AddWebApiModule();
        }

        public void Build()
        {
            hostBuilder.BuildContainer();
            hostBuilder.UseWebApiModule();

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

