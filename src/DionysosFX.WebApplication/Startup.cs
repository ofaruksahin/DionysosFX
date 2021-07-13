using DionysosFX.Host;
using DionysosFX.Module.StaticFile;
using DionysosFX.Module.WebApi;
using DionysosFX.Swan;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.WebApplication
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup : IStartup
    {
        /// <summary>
        /// 
        /// </summary>
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
        public void Configure()
        {
            System.Console.WriteLine("On Configure Method");
            _hostBuilder.AddPrefix("http://*:1923");
            _hostBuilder.AddStaticFileModule();
            _hostBuilder.AddWebApiModule();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Build()
        {            
            System.Console.WriteLine("On Build Method");
            _hostBuilder.BuilderContainer();
            _hostBuilder.UseStaticFileModule();
            _hostBuilder.UseWebApiModule();

            using (var cts = new CancellationTokenSource())
            {
                Task.WaitAll(RunWebServer(cts.Token));
            }            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IWebServer CreateWebServer()
        {
            IWebServer webServer = new WebServer(_hostBuilder);
            webServer.StateChanged += (sender, e) => Console.WriteLine($"Server New State {e.NewState}");            
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
