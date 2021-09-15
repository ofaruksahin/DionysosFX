using DionysosFX.Swan.Net;
using System;

namespace DionysosFX.Module.IWebApi
{
    public abstract class EndpointResult : IEndpointResult
    {
        public object Result { get; set; }

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
