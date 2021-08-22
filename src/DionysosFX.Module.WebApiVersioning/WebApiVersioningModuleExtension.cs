using Autofac;
using DionysosFX.Swan;

namespace DionysosFX.Module.WebApiVersioning
{
    public static class WebApiVersioningModuleExtension
    {
        public static IHostBuilder AddWebApiVersionModule(this IHostBuilder @this,WebApiVersioningModuleOptions options)
        {
            @this.ContainerBuilder.Register(r => options).As<WebApiVersioningModuleOptions>().SingleInstance();
            return @this;
        }

        public static IHostBuilder UseWebApiVersionModule(this IHostBuilder @this) => @this;
    }
}
