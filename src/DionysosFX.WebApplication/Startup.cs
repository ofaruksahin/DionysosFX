using DionysosFX.Host;

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
        }
    }
}
