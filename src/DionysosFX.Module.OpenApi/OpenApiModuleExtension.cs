using Autofac;
using DionysosFX.Swan;
using System;

namespace DionysosFX.Module.OpenApi
{
    public static class OpenApiModuleExtension
    {
        public static IHostBuilder AddOpenApiModule(this IHostBuilder @this, OpenApiOptions options)
        {
            @this.ContainerBuilder.Register(r => options).As<OpenApiOptions>().SingleInstance();
            @this
                .ContainerBuilder
                .RegisterType<OpenApiModule>()
                .SingleInstance();
            return @this;
        }

        public static IHostBuilder UseOpenApiModule(this IHostBuilder @this)
        {
            var module = @this.Container.Resolve<OpenApiModule>();
            if (module == null)
                throw new Exception($"{nameof(module)} Module not found");
            @this.ModuleCollection.Add(module.GetType().Name, module);
            return @this;
        }
    }
}
