using Autofac;
using DionysosFX.Swan;

namespace DionysosFX.Module.FluentValidator
{
    public static class FluentValidatorModuleExtension
    {
        /// <summary>
        /// Add fluent validator module add container
        /// </summary>
        /// <param name="this"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IHostBuilder AddFluentValidatorModule(this IHostBuilder @this,FluentValidatonOptions options)
        {
            @this.ContainerBuilder.Register(r => options).As<FluentValidatonOptions>().SingleInstance();
            return @this;
        }

        /// <summary>
        /// Add fluent validator module add web module
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IHostBuilder UseFluentValidatorModule(this IHostBuilder @this) => @this;
    }
}
