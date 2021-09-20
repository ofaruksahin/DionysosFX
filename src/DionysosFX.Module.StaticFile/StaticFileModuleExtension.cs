using Autofac;
using DionysosFX.Swan;
using DionysosFX.Swan.Constants;
using DionysosFX.Swan.Exceptions;
using DionysosFX.Swan.Extensions;
using System.Reflection;

namespace DionysosFX.Module.StaticFile
{
    public static class StaticFileModuleExtension
    {
        /// <summary>
        /// Static File module add container
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IHostBuilder AddStaticFileModule(this IHostBuilder @this)
        {
            StaticFileOptions options = new StaticFileOptions();
            options.AllowedMimeTypes.Add("*");
            @this.ContainerBuilder.RegisterType<StaticFileModule>().SingleInstance();
            @this.ContainerBuilder.Register(i => options).As<StaticFileOptions>().SingleInstance();
            return @this;
        }

        /// <summary>
        /// Static File module add container
        /// </summary>
        /// <param name="this"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IHostBuilder AddStaticFileModule(this IHostBuilder @this,StaticFileOptions options)
        {
            @this.ContainerBuilder.RegisterType<StaticFileModule>().SingleInstance();
            @this.ContainerBuilder.Register(i => options).As<StaticFileOptions>().SingleInstance();
            return @this;
        }

        /// <summary>
        /// Static file module add module container
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static  IHostBuilder UseStaticFileModule(this IHostBuilder @this)
        {
            if (!@this.Container.TryResolve(out StaticFileModule module))
                throw new ModuleNotFoundException(typeof(StaticFileModule).Name);
            module.SetIContainer(@this.Container);
            @this.ModuleCollection.Add(module.GetType().Name, module);
            return @this;
        }
    }
}
