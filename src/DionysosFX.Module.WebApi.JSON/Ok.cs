using DionysosFX.Module.IWebApi;
using DionysosFX.Swan.Net;
using Newtonsoft.Json;
using System.IO;
using System.Net;

namespace DionysosFX.Module.WebApi.JSON
{
    /// <summary>
    /// Json Ok
    /// </summary>
    public class Ok : EndpointResult
    {
        public Ok(object result = null) : base(result)
        {
            StatusCode = (int)HttpStatusCode.OK;
        }

        public override void ExecuteResponse(IHttpContext Context)
        {
            Context.Response.StatusCode = StatusCode;
            Context.Response.ContentType = "application/json";
            using (var writer = new StreamWriter(Context.Response.OutputStream))
                writer.WriteLine(JsonConvert.SerializeObject(Result));
            Context.SetHandled();
        }
    }
}
