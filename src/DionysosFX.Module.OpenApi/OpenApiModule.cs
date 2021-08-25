using DionysosFX.Module.OpenApi.Attributes;
using DionysosFX.Module.OpenApi.Entities;
using DionysosFX.Module.WebApi;
using DionysosFX.Swan.Modules;
using DionysosFX.Swan.Net;
using DionysosFX.Swan.Routing;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.Module.OpenApi
{
    public class OpenApiModule : IWebModule
    {
        DocumentationResponse DocumentationResponse = new DocumentationResponse();
        public void Start(CancellationToken cancellationToken)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes().Where(f => f.IsWebApiController()).ToList();
                if (types.Any())
                {
                    foreach (var controller in types)
                    {
                        var controllerItem = new ControllerItem();
                        controllerItem.Name = controller.Name;

                        var controllerDescriptionAttribute = controller.GetCustomAttributes(true).Where(f => f.GetType() == typeof(ControllerDescriptionAttribute)).FirstOrDefault();
                        if (controllerDescriptionAttribute != null)
                            controllerItem.Description = ((ControllerDescriptionAttribute)controllerDescriptionAttribute).Description;

                        var endpoints = controller.GetMethods(BindingFlags.Instance | BindingFlags.Public).Where(f=>f.IsRoute()).ToList();
                        foreach (var endpoint in endpoints)
                        {
                            EndpointItem endpointItem = new EndpointItem();
                            controllerItem.Endpoints.Add(endpointItem);
                        }
                    }
                }
            }
        }

        public async Task HandleRequestAsync(IHttpContext context)
        {
            if (context.IsHandled)
                return;
            if (context.Request.Url.LocalPath == "/openapi")
            {
                context.SetHandled();
            }
            else if (context.Request.Url.LocalPath == "/openapi-ui" || context.Request.Url.LocalPath == "/openapi-ui.html")
            {

            }
        }

        public void Dispose()
        {
            DocumentationResponse = null;
        }
    }
}
