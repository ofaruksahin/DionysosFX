using Autofac;
using DionysosFX.Swan;
using System;

namespace DionysosFX.Module.WebApi
{
    public static class WebApiModuleExtension
    {
        public static IHostBuilder AddWebApiModule(this IHostBuilder @this)
        {
            @this.ContainerBuilder.RegisterType<WebApiModule>();
            return @this;
        }

        public static IHostBuilder UseWebApiModule(this IHostBuilder @this)
        {
            var module = @this.Container.Resolve<WebApiModule>();
            if (module == null)
                throw new Exception($"{nameof(module)} Module not found");
            @this.ModuleCollection.Add(module.GetType().Name, module);
            return @this;
        }
    }
}
