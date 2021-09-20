using Autofac;
using DionysosFX.Swan.Net;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.Swan.Modules
{
    public abstract class WebModuleBase : IWebModule
    {
        public IContainer Container
        {
            get;
            private set;
        }

        public void SetIContainer(IContainer container)
        {
            Container = container;
        }

        public virtual void Start(CancellationToken cancellationToken)
        {

        }

        public virtual async Task HandleRequestAsync(IHttpContext context)
        {
            throw new System.NotImplementedException();
        }

        public virtual void Dispose()
        {
        }      
    }
}
