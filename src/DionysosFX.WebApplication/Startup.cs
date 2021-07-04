using DionysosFX.Host;
using DionysosFX.Swan;

namespace DionysosFX.WebApplication
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
            System.Console.WriteLine("On Configure Method");
        }

        public void Build()
        {
            System.Console.WriteLine("On Build Method");
        }
    }
}
