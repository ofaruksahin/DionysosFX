using DionysosFX.Swan.Net;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace DionysosFX.Module.WebApi
{
    [ApiController]
    public abstract class WebApiController : IDisposable
    {
        bool _disposed;

        public IHttpContext Context;

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void SetHttpContext(IHttpContext _context)
        {
            Context = _context;
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!_disposed)
                {
                    _disposed = true;
                }
            }
        }

        private void ExecuteJson(HttpStatusCode code,object responseItem)
        {
            Context.Response.StatusCode = (int)code;
            Context.Response.ContentType = "application/json";
            using (var writer = new StreamWriter(Context.Response.OutputStream))
                writer.WriteLine(JsonConvert.SerializeObject(responseItem));
            Context.SetHandled();
        }

        public void Ok(ResponseType responseType, object response = default)
        {
            switch (responseType)
            {
                case ResponseType.Json:
                    if (response == null)
                        ExecuteJson(HttpStatusCode.OK, new { });
                    else
                        ExecuteJson(HttpStatusCode.OK, response);
                    break;
                case ResponseType.XML:
                    throw new Exception("XML Response Type Not Supported");
            }  
        }
    }
}
