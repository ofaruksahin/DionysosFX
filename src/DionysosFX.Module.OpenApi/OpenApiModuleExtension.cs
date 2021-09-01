using Autofac;
using DionysosFX.Swan;
using System;

namespace DionysosFX.Module.OpenApi
{
    public static class OpenApiModuleExtension
    {
        public static IHostBuilder AddOpenApiModule(this IHostBuilder @this, OpenApiModuleOptions options)
        {
            @this.ContainerBuilder.Register(r => options).As<OpenApiModuleOptions>().SingleInstance();
            @this
                .ContainerBuilder
                .RegisterType<OpenApiModule>()
                .SingleInstance();
            return @this;
        }

        public static IHostBuilder UseOpenApiModule(this IHostBuilder @this)
        {
            if(!@this.Container.TryResolve<OpenApiModule>(out OpenApiModule module))            
                throw new Exception($"{nameof(module)} Module not found");
            @this.ModuleCollection.Add(module.GetType().Name, module);
            return @this;
        }
    }
}
