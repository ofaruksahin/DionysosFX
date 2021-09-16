using Autofac;
using DionysosFX.Swan;

namespace DionysosFX.Module.WebApiVersioning
{
    public static class WebApiVersioningModuleExtension
    {
        /// <summary>
        /// Add web api version module add container
        /// </summary>
        /// <param name="this"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IHostBuilder AddWebApiVersionModule(this IHostBuilder @this,WebApiVersioningModuleOptions options)
        {
            @this.ContainerBuilder.Register(r => options).As<WebApiVersioningModuleOptions>().SingleInstance();
            return @this;
        }

        /// <summary>
        /// Add web api version module add module container
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IHostBuilder UseWebApiVersionModule(this IHostBuilder @this) => @this;
    }
}
