using DionysosFX.Host;
using DionysosFX.Module.WebApi;
using DionysosFX.Swan;
using DionysosFX.Swan.Modules;
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.Module.WebApiVersioning.Lab
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
            var webApiVersioningModuleOptions = new WebApiVersioningModuleOptions("0.0.0.5");
            webApiVersioningModuleOptions.OnVersionException += WebApiVersioningModuleOptions_OnInvalidEvent;
            _hostBuilder.AddWebApiVersionModule(webApiVersioningModuleOptions);
        }

        private void WebApiVersioningModuleOptions_OnInvalidEvent(object sender, OnVersionExceptionEventArgs e)
        {
            e.Context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            e.Context.Response.ContentType = "application/json";
            using (var writer = new StreamWriter(e.Context.Response.OutputStream))
            {
                if (e.Deprecated)
                {
                    Console.WriteLine($"{e.Version} is deprecated");
                    writer.WriteLine($"{e.Version} is deprecated");
                }
                else
                {
                    Console.WriteLine($"{e.Version} is not defined");
                    writer.WriteLine($"{e.Version} is not defined");
                }
            }
            e.Context.SetHandled();
        }

        public void Build()
        {
            _hostBuilder.BuildContainer();
            _hostBuilder.UseWebApiModule();
            _hostBuilder.UseWebApiVersionModule();
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