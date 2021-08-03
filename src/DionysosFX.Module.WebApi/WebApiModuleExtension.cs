using Autofac;
using DionysosFX.Swan;
using DionysosFX.Swan.Net;
using System;

namespace DionysosFX.Module.WebApi
{
    public static class WebApiModuleExtension
    {
        public static IHostBuilder AddWebApiModule(this IHostBuilder @this,WebApiModuleOptions options = null)
        {
            @this.ContainerBuilder.RegisterType<WebApiModule>().SingleInstance();
            if (options == null)
                options = new WebApiModuleOptions();
            @this.ContainerBuilder.Register(r => options).As<WebApiModuleOptions>().SingleInstance();
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
        
        public static WebApiModuleOptions GetWebApiModuleOptions(this IHttpContext @this) => @this.Container.Resolve<WebApiModuleOptions>();   
    }
}
