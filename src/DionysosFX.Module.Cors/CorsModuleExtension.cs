using Autofac;
using DionysosFX.Swan;

namespace DionysosFX.Module.Cors
{
    public static class CorsModuleExtension
    {
        public static IHostBuilder AddCors(this IHostBuilder @this,CorsModuleOptions options)
        {
            @this.ContainerBuilder.Register(i => options).As<CorsModuleOptions>().SingleInstance();
            return @this;
        }

        public static IHostBuilder UseCors(this IHostBuilder @this) => @this;
    }
}
