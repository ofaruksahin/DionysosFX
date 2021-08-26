using Autofac;
using DionysosFX.Host;
using DionysosFX.Module.OpenApi;
using DionysosFX.Module.WebApi;
using DionysosFX.Module.WebApiVersioning;
using DionysosFX.Swan;
using DionysosFX.Swan.Modules;
using DionysosFX.Template.WebAPI.IService;
using DionysosFX.Template.WebAPI.Service;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.Template.WebAPI
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup : IStartup
    {
        IHostBuilder _hostBuilder;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hostBuilder"></param>
        public Startup(IHostBuilder hostBuilder)
        {
            _hostBuilder = hostBuilder;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Build()
        {
            _hostBuilder.BuildContainer();
            _hostBuilder.UseWebApiModule();
            _hostBuilder.UseOpenApiModule();
            _hostBuilder.AddWebApiVersionModule(new WebApiVersioningModuleOptions("1.0.0.0"));

            using (var cts = new CancellationTokenSource())
            {
                Task.WaitAll(RunWebServer(cts.Token));
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public void Configure()
        {
            _hostBuilder.AddPrefix("http://*:1923");
            _hostBuilder.AddWebApiModule(new WebApiModuleOptions(ResponseType.Json));
            _hostBuilder.AddOpenApiModule();

            _hostBuilder
                .ContainerBuilder
                .RegisterType<UserService>()
                .As<IUserService>()
                .SingleInstance();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IWebServer CreateWebServer()
        {
            IWebServer webServer = new WebServer(_hostBuilder);
            webServer.StateChanged += (sender, e) => Console.WriteLine($"Server New State {e.NewState}");
            webServer.OnFatalException += (sender, e) => { Console.WriteLine(e.Exception.Message); };
            return webServer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task RunWebServer(CancellationToken cancellationToken)
        {
            using var server = CreateWebServer();
            await server.RunAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
