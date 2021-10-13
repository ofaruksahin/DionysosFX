using Autofac;
using DionysosFX.Swan;
using DionysosFX.Swan.Exceptions;

namespace DionysosFX.Module.HealthCheck
{
    public static class HealthCheckModuleExtension
    {
        public static IHostBuilder AddHealthCheckModule(this IHostBuilder @this)
        {
            @this
                .ContainerBuilder
                .RegisterType<HealthCheckModule>()
                .SingleInstance();
            return @this;
        }

        public static IHostBuilder UseHealthCheckModule(this IHostBuilder @this)
        {
            if (!@this.Container.TryResolve(out HealthCheckModule module))
                throw new ModuleNotFoundException(typeof(HealthCheckModule).Name);
            module.SetIContainer(@this.Container);
            @this.ModuleCollection.Add(module.GetType().Name, module);
            return @this;
        }

        public static IHostBuilder AddHealthCheckItem(this IHostBuilder @this, IHealthCheck healthCheckItem)
        {
            if (!@this.Container.TryResolve(out HealthCheckModule module))
                throw new ModuleNotFoundException(typeof(HealthCheckModule).Name);
            module.HealthChecks.Add(healthCheckItem);
            return @this;
        }
    }
}
