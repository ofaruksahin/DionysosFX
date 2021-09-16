using DionysosFX.Swan.Net;
using System;

namespace DionysosFX.Module.WebApiVersioning
{
    /// <summary>
    /// OnVersionException Event Args
    /// </summary>
    public class OnVersionExceptionEventArgs : EventArgs
    {
        /// <summary>
        /// Version
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Version is deprecated ?
        /// </summary>
        public bool Deprecated { get; set; }

        /// <summary>
        /// HttpContext
        /// </summary>
        public IHttpContext Context { get; set; }

        public OnVersionExceptionEventArgs(string version,bool deprecated, IHttpContext context)
        {
            Version = version;
            Deprecated = deprecated;
            Context = context;
        }
    }
}
