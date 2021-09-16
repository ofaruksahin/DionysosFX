using Autofac;
using DionysosFX.Swan;
using DionysosFX.Swan.Net;
using System;

namespace DionysosFX.Module.WebApi
{
    public static class WebApiModuleExtension
    {
        /// <summary>
        /// Web Api module add container
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IHostBuilder AddWebApiModule(this IHostBuilder @this)
        {
            @this.ContainerBuilder.RegisterType<WebApiModule>().SingleInstance();
            return @this;
        }

        /// <summary>
        /// Web api module add module container 
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IHostBuilder UseWebApiModule(this IHostBuilder @this)
        {
            if(!@this.Container.TryResolve(out WebApiModule module))            
                throw new Exception($"{nameof(module)} Module not found");
            @this.ModuleCollection.Add(module.GetType().Name, module);
            return @this;
        }

        /// <summary>
        /// Type is WebApiController ?
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
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
