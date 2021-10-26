using DionysosFX.Host;
using DionysosFX.Swan;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.Module.ConfigurationManager.Lab
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
            ConfigurationManagerBuilder.ConfigurationFiles.Add(@"appsettings.development.json");
            ConfigurationManagerBuilder.Build();
            var a = Configuration.GetSection("Name").Get<string>();
        }


        public void Build()
        {
            _hostBuilder.BuildContainer();
            using (var cts = new CancellationTokenSource())
            {
                Task.WaitAll(RunWebServer(cts.Token));
            }
        }

        private IWebServer CreateWebServer()
        {
            IWebServer webServer = new WebServer(_hostBuilder);
            webServer.StateChanged += (sender, e) => Console.WriteLine($"Server New State {e.NewState}");
            webServer.OnFatalException += (sender, e) => { Console.WriteLine(e.Exception.Message); };
            return webServer;
        }

        private async Task RunWebServer(CancellationToken cancellationToken)
        {
            using var server = CreateWebServer();
            await server.RunAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}

