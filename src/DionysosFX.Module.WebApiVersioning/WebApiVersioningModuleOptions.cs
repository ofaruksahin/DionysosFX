using DionysosFX.Swan.Net;
using System;

namespace DionysosFX.Module.WebApiVersioning
{
    public class WebApiVersioningModuleOptions
    {
        /// <summary>
        /// Default API Version
        /// </summary>
        public string DefaultVersion { get; set; }

        /// <summary>
        /// OnVersionException
        /// </summary>
        public event EventHandler<OnVersionExceptionEventArgs> OnVersionException;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="defaultVersion"></param>
        public WebApiVersioningModuleOptions(string defaultVersion)
        {
            DefaultVersion = defaultVersion;
        }

        /// <summary>
        /// Trigger OnVersionException method
        /// </summary>
        /// <param name="version"></param>
        /// <param name="context"></param>
        private void TriggerOnVersionException(string version, bool deprecated, IHttpContext context)
        {
            OnVersionException?.Invoke(this, new OnVersionExceptionEventArgs(version, deprecated, context));
        }
    }
}
