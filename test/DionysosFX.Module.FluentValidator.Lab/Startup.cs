using DionysosFX.Host;
using DionysosFX.Module.WebApi;
using DionysosFX.Swan;
using DionysosFX.Swan.Modules;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.Module.FluentValidator.Lab
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
            _hostBuilder.AddWebApiModule(new WebApiModuleOptions(ResponseType.Json));
            FluentValidatonOptions options = new FluentValidatonOptions();
            options.OnValidationFail += Options_OnValidationFail;
            _hostBuilder.AddFluentValidatorModule(options);
        }

        private void Options_OnValidationFail(object sender, OnValidationFailEventArgs e)
        {
            e.Context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            e.Context.Response.ContentType = "application/json";
            using (var writer = new StreamWriter(e.Context.Response.OutputStream))
            {
                writer.WriteLine(JsonConvert.SerializeObject(e.ValidationResult.Errors));
            }
            e.Context.SetHandled();
        }

        public void Build()
        {
            _hostBuilder.BuildContainer();
            _hostBuilder.UseWebApiModule();
            _hostBuilder.UseFluentValidatorModule();
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
