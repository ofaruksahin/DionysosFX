using System;

namespace DionysosFX.Host
{
    public class Host
    {
        IHostBuilder _hostBuilder;

        public Host()
        {
            _hostBuilder = new HostBuilder();
        }

        public IStartup CreateStartup<T>() where T : IStartup
        {
            return (T)Activator.CreateInstance(typeof(T),_hostBuilder);
        }
    }
}
