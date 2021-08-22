using DionysosFX.Swan.Modules;
using DionysosFX.Swan.Net;
using Newtonsoft.Json;
using System.IO;
using System.Net;

namespace DionysosFX.Module.WebApi.EnpointResults
{
    /// <summary>
    /// 
    /// </summary>
    public class Ok : IEndpointResult
    {
        /// <summary>
        /// 
        /// </summary>
        object Result = null;
        /// <summary>
        /// 
        /// </summary>
        int StatusCode = (int)HttpStatusCode.OK;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        public Ok(object result = null)
        {
            Result = result;
        }

        public void ExecuteResponse(IHttpContext Context)
        {
            switch (Context.GetWebApiModuleOptions()?.ResponseType)
            {
                case ResponseType.Json:
                    Context.Response.StatusCode = StatusCode;
                    Context.Response.ContentType = "application/json";
                    using (var writer = new StreamWriter(Context.Response.OutputStream))
                        writer.WriteLine(JsonConvert.SerializeObject(Result));
                    Context.SetHandled();
                    break;
                case ResponseType.XML:
                    break;
                default:
                    break;
            }
        }
    }
}
