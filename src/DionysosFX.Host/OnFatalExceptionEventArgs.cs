using DionysosFX.Swan.Net;
using System;

namespace DionysosFX.Host
{
    public class OnFatalExceptionEventArgs : EventArgs
    {
        public IHttpContext Context { get; private set; }
        public Exception Exception { get; private set; }

        public OnFatalExceptionEventArgs(IHttpContext Context, Exception Exception)
        {
            this.Context = Context;
            this.Exception = Exception;
        }
    }
}
