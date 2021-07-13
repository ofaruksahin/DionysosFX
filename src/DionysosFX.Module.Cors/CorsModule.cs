using DionysosFX.Swan.Modules;
using DionysosFX.Swan.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.Module.Cors
{
    public class CorsModule : IWebModule
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task HandleRequestAsync(IHttpContext context)
        {
            throw new NotImplementedException();
        }

        public void Start(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
