using Autofac;
using System.Collections.Generic;
using System.Linq;

namespace DionysosFX.Host
{
    /// <summary>
    /// 
    /// </summary>
    public class HostBuilder : IHostBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        List<string> _prefixes = null;

        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyList<string> Prefixes => _prefixes;

        /// <summary>
        /// 
        /// </summary>        
        private ContainerBuilder _containerBuilder = null;
        /// <summary>
        /// 
        /// </summary>
        public ContainerBuilder ContainerBuilder => _containerBuilder;

        /// <summary>
        /// 
        /// </summary>
        private IContainer _container = null;

        /// <summary>
        /// 
        /// </summary>
        public IContainer Container => _container;


        public HostBuilder()
        {
            _prefixes = new();
            _containerBuilder = new();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        public void AddPrefix(string prefix)
        {
            if (!_prefixes.Any(f => f == prefix))
                _prefixes.Add(prefix);
        }

        public void BuilderContainer()
        {
            _container = _containerBuilder?.Build();
        }
    }
}
