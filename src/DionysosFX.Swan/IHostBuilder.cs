using Autofac;
using DionysosFX.Swan.Modules;
using System.Collections.Generic;

namespace DionysosFX.Swan
{
    public interface IHostBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        IReadOnlyList<string> Prefixes
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        ContainerBuilder ContainerBuilder
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        IContainer Container
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        IWebModuleCollection ModuleCollection
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        void AddPrefix(string prefix);
        /// <summary>
        /// 
        /// </summary>
        void BuilderContainer();
    }
}
