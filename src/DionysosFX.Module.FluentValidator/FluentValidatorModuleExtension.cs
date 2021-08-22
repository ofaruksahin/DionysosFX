using Autofac;
using DionysosFX.Swan;

namespace DionysosFX.Module.FluentValidator
{
    public static class FluentValidatorModuleExtension
    {
        public static IHostBuilder AddFluentValidatorModule(this IHostBuilder @this,FluentValidatonOptions options)
        {
            @this.ContainerBuilder.Register(r => options).As<FluentValidatonOptions>().SingleInstance();
            return @this;
        }

        public static IHostBuilder UseFluentValidatorModule(this IHostBuilder @this) => @this;
    }
}
