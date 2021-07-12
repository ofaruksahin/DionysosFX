using DionysosFX.Swan;
using System;

namespace DionysosFX.Host
{
    /// <summary>
    /// 
    /// </summary>
    public class Host
    {
        /// <summary>
        /// 
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
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IStartup CreateStartup<T>() where T : IStartup
        {
            return (T)Activator.CreateInstance(typeof(T),_hostBuilder);
        }
    }
}
