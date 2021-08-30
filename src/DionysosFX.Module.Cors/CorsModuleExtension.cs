using Autofac;
using DionysosFX.Swan;
using System;
using System.Linq;

namespace DionysosFX.Module.Cors
{
    public static class CorsModuleExtension
    {
        public static IHostBuilder AddCors(this IHostBuilder @this,CorsModuleOptions options)
        {
            @this.ContainerBuilder.RegisterType<CorsModule>().SingleInstance();
            @this.ContainerBuilder.Register(i => options).As<CorsModuleOptions>().SingleInstance();
            return @this;
        }

        public static IHostBuilder UseCors(this IHostBuilder @this,string policyName)
        {
            var module = @this.Container.Resolve<CorsModule>();
            if (module == null)
                throw new Exception($"{nameof(module)} Module not found");
            @this.ModuleCollection.Add(module.GetType().Name, module);
            var options = @this.Container.Resolve<CorsModuleOptions>();
            if (options == null)
                throw new Exception("Cors module options is not defined");
            var policy = options.CorsPolicies.FirstOrDefault(f => f.Name == policyName);
            if (policy == null)
                throw new Exception("Cors policy is not defined");
            policy.IsDefault = true;
            return @this; 
        }
    }
}
