using DionysosFX.Swan.Modules;
using DionysosFX.Swan.Net;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.Module.OpenApi
{
    public class OpenApiModule : IWebModule
    {
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public Task HandleRequestAsync(IHttpContext context)
        {
            throw new System.NotImplementedException();
        }

        public void Start(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
