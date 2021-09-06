using DionysosFX.Swan.Net;
using System;

namespace DionysosFX.Host
{
    public class OnFatalExceptionEventArgs : EventArgs
    {
        /// <summary>
        /// Web Request Content
        /// </summary>
        public IHttpContext Context { get; private set; }

        /// <summary>
        /// Handled exception
        /// </summary>
        public Exception Exception { get; private set; }

        public OnFatalExceptionEventArgs(IHttpContext Context, Exception Exception)
        {
            this.Context = Context;
            this.Exception = Exception;
        }
    }
}
