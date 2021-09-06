using DionysosFX.Swan;
using System;

namespace DionysosFX.Host
{
    /// <summary>
    /// DionysosFX App Instance Configuration Object
    /// </summary>
    public class Host
    {
        /// <summary>
        /// DionysosFX Configuration Object
        /// </summary>
        IHostBuilder _hostBuilder;

        /// <summary>
        /// 
        /// </summary>
        public Host()
        {
            _hostBuilder = new HostBuilder();
        }

        /// <summary>
        /// DionysosFX App Instance startup object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IStartup CreateStartup<T>() where T : IStartup
        {
            return (T)Activator.CreateInstance(typeof(T),_hostBuilder);
        }
    }
}
