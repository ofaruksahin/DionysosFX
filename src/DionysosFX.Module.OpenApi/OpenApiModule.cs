using DionysosFX.Module.OpenApi.Entities;
using DionysosFX.Swan.Modules;
using DionysosFX.Swan.Net;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.Module.OpenApi
{
    public class OpenApiModule : IWebModule
    {
        DocumentationResponse DocumentationResponse = new DocumentationResponse();
        public void Start(CancellationToken cancellationToken)
        {

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
