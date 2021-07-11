using DionysosFX.Host;
using DionysosFX.Swan.Threading;
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
            _hostBuilder.AddPrefix("http://*:1923");
            System.Console.WriteLine("On Configure Method");
        }

        /// <summary>
        /// 
        /// </summary>
        public void Build()
        {
            System.Console.WriteLine("On Build Method");

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
