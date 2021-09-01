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
            StaticFileOptions options = new StaticFileOptions();
            options.AllowedMimeTypes.Add("*");
            @this.ContainerBuilder.RegisterType<StaticFileModule>().SingleInstance();
            @this.ContainerBuilder.Register(i => options).As<StaticFileOptions>().SingleInstance();
            return @this;
        }

        public static IHostBuilder AddStaticFileModule(this IHostBuilder @this,StaticFileOptions options)
        {
            @this.ContainerBuilder.RegisterType<StaticFileModule>().SingleInstance();
            @this.ContainerBuilder.Register(i => options).As<StaticFileOptions>().SingleInstance();
            return @this;
        }


        public static  IHostBuilder UseStaticFileModule(this IHostBuilder @this)
        {
            if(!@this.Container.TryResolve(out StaticFileModule module))            
                throw new Exception($"{nameof(module)} Module not found");
            @this.ModuleCollection.Add(module.GetType().Name, module);
            return @this;
        }
    }
}
