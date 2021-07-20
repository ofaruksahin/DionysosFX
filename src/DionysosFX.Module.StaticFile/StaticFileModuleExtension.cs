using Autofac;
using DionysosFX.Swan;
using DionysosFX.Swan.Modules;
using System;

namespace DionysosFX.Module.StaticFile
{
    public static class StaticFileModuleExtension
    {
        public static IHostBuilder AddStaticFileModule(this IHostBuilder @this)
        {
            @this.ContainerBuilder.RegisterType<StaticFileModule>().SingleInstance();
            return @this;
        }

        public static  IHostBuilder UseStaticFileModule(this IHostBuilder @this)
        {
            var module = @this.Container.Resolve<StaticFileModule>();
            if (module == null)
                throw new Exception($"{nameof(module)} Module not found");
            @this.ModuleCollection.Add(module.GetType().Name, module);
            return @this;
        }
    }
}
