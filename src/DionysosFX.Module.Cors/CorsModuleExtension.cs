using Autofac;
using DionysosFX.Swan;
using System;
using System.Linq;

namespace DionysosFX.Module.Cors
{
    public static class CorsModuleExtension
    {
        public static IHostBuilder AddCors(this IHostBuilder @this,CorsModuleOptions options)
        {
            @this.ContainerBuilder.RegisterType<CorsModule>().SingleInstance();
            @this.ContainerBuilder.Register(i => options).As<CorsModuleOptions>().SingleInstance();
            return @this;
        }

        public static IHostBuilder UseCors(this IHostBuilder @this)
        {
            if(!@this.Container.TryResolve(out CorsModule module))
                throw new Exception($"{nameof(module)} Module not found");
            @this.ModuleCollection.Add(module.GetType().Name, module);            
            return @this; 
        }
    }
}
