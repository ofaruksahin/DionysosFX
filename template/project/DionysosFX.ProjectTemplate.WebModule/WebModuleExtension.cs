using Autofac;
using DionysosFX.Swan;
using DionysosFX.Swan.Exceptions;

namespace DionysosFX.ProjectTemplate.WebModule
{
    public static class WebModuleExtension
    {
        public static IHostBuilder AddModule(this IHostBuilder @this)
        {
            @this
                .ContainerBuilder
                .RegisterType<WebModule>()
                .SingleInstance();
            return @this;
        }

        public static IHostBuilder UseModule(this IHostBuilder @this)
        {
            if (!@this.Container.TryResolve(out WebModule module))
                throw new ModuleNotFoundException(typeof(WebModule).Name);
            module.SetIContainer(@this.Container);
            @this.ModuleCollection.Add(module.GetType().Name, module);
            return @this;
        }

    }
}
