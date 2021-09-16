using DionysosFX.Swan.Net;
using System;

namespace DionysosFX.Module.IWebApi
{
    /// <summary>
    /// Base endpoint result
    /// </summary>
    public abstract class EndpointResult : IEndpointResult
    {
        /// <summary>
        /// Response Result
        /// </summary>
        public object Result { get; set; }

        /// <summary>
        /// Response status code
        /// </summary>
        public int StatusCode { get; set; }

        public EndpointResult(object result = null)
        {
            Result = result;
        }

        public virtual void ExecuteResponse(IHttpContext Context)
        {

        }
    }
}
