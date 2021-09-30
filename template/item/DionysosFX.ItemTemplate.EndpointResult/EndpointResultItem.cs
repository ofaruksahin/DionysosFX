using DionysosFX.Module.IWebApi;
using DionysosFX.Swan.Net;

namespace $rootnamespace$.EndpointResult
{
    public class $safeitemname$ : IEndpointResult
    {
        private object Data
        {
            get;
            set;
        }

        public EndpointResultItem(object data = null)
        {
            Data = data;
        }

        public void ExecuteResponse(IHttpContext Context)
        {
        }
    }
}
