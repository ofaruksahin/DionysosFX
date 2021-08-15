using DionysosFX.Host;
using DionysosFX.Swan;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.Module.StaticFile.Lab
{
    public class Startup : IStartup
    {
        IHostBuilder _hostBuilder;

        public Startup(IHostBuilder hostBuilder)
        {
            _hostBuilder = hostBuilder;
        }

        public void Configure()
        {
            _hostBuilder.AddPrefix("http://*:1923");
            StaticFileOptions staticFileOptions = new StaticFileOptions();
            staticFileOptions.AllowedMimeTypes.Add("text/html");
            staticFileOptions.AllowedMimeTypes.Add("application/javascript");
            _hostBuilder.AddStaticFileModule(staticFileOptions);
        }

        public void Build()
        {
            _hostBuilder.BuildContainer();
            _hostBuilder.UseStaticFileModule();
            using (var cts = new CancellationTokenSource())
            {
                Task.WaitAll(RunWebServer(cts.Token));
            }
        }

        private IWebServer CreateWebServer()
        {
            IWebServer webServer = new WebServer(_hostBuilder);
            webServer.StateChanged += (sender, e) => Console.WriteLine($"Server New State {e.NewState}");
            return webServer;
        }

        private async Task RunWebServer(CancellationToken token)
        {
            using var server = CreateWebServer();
            await server.RunAsync(token).ConfigureAwait(false);
        }
    }
}
