using DionysosFX.Host;
using DionysosFX.Module.HealthCheck.SqlServer;
using DionysosFX.Module.WebApi;
using DionysosFX.Swan;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.Module.HealthCheck.Lab
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
            _hostBuilder.AddWebApiModule();
            _hostBuilder.AddHealthCheckModule();
        }


        public void Build()
        {
            _hostBuilder.BuildContainer();
            _hostBuilder.UseWebApiModule();
            _hostBuilder
                .AddHealthCheckModule()
                .AddHealthCheckItem(new SqlServerHealthCheck(new SqlServerHealthCheckOptions()
                {
                    ConnectionString = "Server=.;Database=test;User Id=sa;Password=123456;",
                    Sql = "SELECT 1;"
                }));
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
