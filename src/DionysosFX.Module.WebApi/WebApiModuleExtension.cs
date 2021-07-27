using Autofac;
using DionysosFX.Swan;
using System;

namespace DionysosFX.Module.WebApi
{
    public static class WebApiModuleExtension
    {
        public static IHostBuilder AddWebApiModule(this IHostBuilder @this)
        {
            @this.ContainerBuilder.RegisterType<WebApiModule>().SingleInstance();
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

        public static bool IsWebApiController(this Type @this)
        {
            if (@this.BaseType == null)
                return false;

            if (@this.BaseType == typeof(WebApiController))
                return true;
            else
                return @this.BaseType.IsWebApiController();

        }
    }
}
