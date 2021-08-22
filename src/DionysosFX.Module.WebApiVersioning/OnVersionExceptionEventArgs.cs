using DionysosFX.Swan.Net;
using System;

namespace DionysosFX.Module.WebApiVersioning
{
    public class OnVersionExceptionEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Deprecated { get; set; }

        /// <summary>
        /// 
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
