using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.Host
{
    public interface IHttpContext
    {
        string Id { get; }
        CancellationToken CancellationToken { get; set; }
        IHttpRequest Request { get; }
        IHttpResponse Response { get; }
        bool IsHandled { get; }
        void Close();
        void SetHandled();
    }
}
