using Autofac;
using DionysosFX.Swan;
using DionysosFX.Swan.Modules;
using System.Collections.Generic;
using System.Linq;

namespace DionysosFX.Host
{
    /// <summary>
    /// DionysosFX App Configuration Builder
    /// </summary>
    public class HostBuilder : IHostBuilder
    {
        /// <summary>
        /// This list storage is dionysosFX access prefixes
        /// </summary>
        List<string> _prefixes = null;

        /// <summary>
        /// This list storage is dionysosFX access prefixes
        /// </summary>
        public IReadOnlyList<string> Prefixes => _prefixes;

        /// <summary>
        /// This object storage is dionysosFX module, options and user defined object dependency injection container
        /// </summary>        
        private ContainerBuilder _containerBuilder = null;
        /// <summary>
        /// This object storage is dionysosFX module, options and user defined object dependency injection container
        /// </summary>
        public ContainerBuilder ContainerBuilder => _containerBuilder;

        /// <summary>
        /// This object storage is dionysosFX module, options and user defined object dependency injection container
        /// </summary>
        private IContainer _container = null;

        /// <summary>
        /// This object storage is dionysosFX module, options and user defined object dependency injection container
        /// </summary>
        public IContainer Container => _container;

        /// <summary>
        /// This object storage is dionysosFX Module
        /// </summary>
        private IWebModuleCollection _moduleCollection = null;

        /// <summary>
        /// This object storage is dionysosFX Module
        /// </summary>
        public IWebModuleCollection ModuleCollection => _moduleCollection;

        public HostBuilder()
        {
            _prefixes = new();
            _containerBuilder = new();
            _moduleCollection = new WebModuleCollection();
        }

        /// <summary>
        /// Add a new prefix on dionysosFX app
        /// </summary>
        /// <param name="prefix"></param>
        public void AddPrefix(string prefix)
        {
            if (!_prefixes.Any(f => f == prefix))
                _prefixes.Add(prefix);
        }

        /// <summary>
        /// Build a dependency injection container
        /// </summary>
        public void BuildContainer()
        {
            _container = _containerBuilder?.Build();
        }
    }
}
