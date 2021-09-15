using DionysosFX.Module.IWebApi;
using DionysosFX.Swan.Net;
using Newtonsoft.Json;
using System.IO;
using System.Net;

namespace DionysosFX.Module.WebApi.JSON
{
    public class InternalServerError : EndpointResult
    {
        public InternalServerError(object result = null) : base(result)
        {
            StatusCode = (int)HttpStatusCode.InternalServerError;
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
