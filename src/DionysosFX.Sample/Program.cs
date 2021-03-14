using System;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.Sample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var url = args.Length > 0 ? args[0] : "http://*:1923";

            using (var cts = new CancellationTokenSource())
            {
                Task.WaitAll(RunWebServer(url, cts.Token));
            }

            Console.ReadKey();
        }

        private static WebServer CreateWebServer(string url)
        {
            var server = new WebServer(
                new WebServerOptions().WithUrlPrefix(url));

            server.StateChanged += (s, e) => Console.WriteLine($"Server New State {e.NewState}");

            return server;
        }

        private static async Task RunWebServer(string url,CancellationToken cancellationToken)
        {
            using var server = CreateWebServer(url);
            await server.RunAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
