using Autofac;
using DionysosFX.Swan;
using DionysosFX.Swan.Constants;
using DionysosFX.Swan.Exceptions;
using DionysosFX.Swan.Extensions;
using System.Reflection;

namespace DionysosFX.Module.Cors
{
    public static class CorsModuleExtension
    {
        /// <summary>
        /// Add cors module in container
        /// </summary>
        /// <param name="this"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IHostBuilder AddCors(this IHostBuilder @this,CorsModuleOptions options)
        {            
            @this.ContainerBuilder.RegisterType<CorsModule>().SingleInstance();
            @this.ContainerBuilder.Register(i => options).As<CorsModuleOptions>().SingleInstance();
            return @this;
        }

        /// <summary>
        /// Add cors module in module collection
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IHostBuilder UseCors(this IHostBuilder @this)
        {
            if (!@this.Container.TryResolve(out CorsModule module))
                throw new ModuleNotFoundException(typeof(CorsModule).Name);
            module.SetIContainer(@this.Container);
            @this.ModuleCollection.Add(module.GetType().Name, module);            
            return @this; 
        }
    }
}
