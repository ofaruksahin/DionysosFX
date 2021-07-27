using Autofac;
using DionysosFX.Swan;
using DionysosFX.Swan.Modules;
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

        /// <summary>
        /// 
        /// </summary>
        private IWebModuleCollection _moduleCollection = null;
        /// <summary>
        /// 
        /// </summary>
        public IWebModuleCollection ModuleCollection => _moduleCollection;

        public HostBuilder()
        {
            _prefixes = new();
            _containerBuilder = new();
            _moduleCollection = new WebModuleCollection();
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

        public void BuildContainer()
        {
            _container = _containerBuilder?.Build();
        }
    }
}
