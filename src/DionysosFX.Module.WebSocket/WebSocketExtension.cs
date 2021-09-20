using Autofac;
using DionysosFX.Swan;
using DionysosFX.Swan.Constants;
using DionysosFX.Swan.Exceptions;
using DionysosFX.Swan.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DionysosFX.Module.WebSocket
{
    public static class WebSocketExtension
    {
        internal static string OnBeforeConnectedMethod => "OnBeforeConnected";

        public static IHostBuilder AddWebSocket(this IHostBuilder @this)
        {
            @this
                .ContainerBuilder
                .RegisterType<WebSocketModule>()
                .SingleInstance();
            return @this;
        }

        public static IHostBuilder UseWebSocket(this IHostBuilder @this)
        {
            WebSocketModule module = null;
            if (!@this.Container.TryResolve<WebSocketModule>(out module))
                throw new ModuleNotFoundException(typeof(WebSocketModule).Name);
            module.SetIContainer(@this.Container);
            @this.ModuleCollection.Add(module.GetType().Name, module);
            return @this;
        }

        internal static List<WebSocketItem> GetWebSocketItems()
        {
            var result = new List<WebSocketItem>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var hubs = assembly.GetTypes()
                    .Where(f => f.IsHub())
                    .ToList();
                if (hubs.Any())
                {
                    foreach (var hub in hubs)
                    {
                        WebSocketItem socketItem = new WebSocketItem();
                        socketItem.SocketType = hub;
                        socketItem.Name = hub.Name;
                        var webSocketAttribute = hub.GetAttributes<WebSocketAttribute>().FirstOrDefault();
                        if (webSocketAttribute == null)
                            throw new AttributeNotFoundException(typeof(WebSocketAttribute).Name);
                        socketItem.Route = webSocketAttribute.Route;
                        if (socketItem.Route.EndsWith("/"))
                            socketItem.Route = socketItem.Route.TrimEnd('/');
                        socketItem.Instance = null;
                        socketItem.IsInstanceGenerated = false;
                        var constructor = hub.GetConstructors().FirstOrDefault();
                        if (constructor != null)
                        {
                            socketItem.ConstructorParameters = constructor.GetParameters().ToList();
                        }
                        socketItem.OnBeforeConnected = hub.GetCustomMethod(OnBeforeConnectedMethod, BindingFlags.Instance | BindingFlags.NonPublic);
                        result.Add(socketItem);
                    }
                }
            }
            return result;
        }

        internal static bool IsHub(this Type type)
        {
            if (type.BaseType != null && type.BaseType == typeof(WebSocketHub))
                return true;
            else if (type.BaseType != null)
                return type.BaseType.IsHub();
            return false;
        }
    }
}
