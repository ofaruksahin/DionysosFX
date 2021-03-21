using System.Collections.Specialized;
using System.IO;

namespace DionysosFX.Net.Internal
{
    public class SystemHttpResponse : IHttpResponse
    {
        private readonly System.Net.HttpListenerResponse _response;

        public SystemHttpResponse(System.Net.HttpListenerResponse response)
        {
            _response = response;
        }

        public int StatusCode
        {
            get => _response.StatusCode;
            set => _response.StatusCode = value;
        }

        public NameValueCollection Headers => _response.Headers;

        public string ContentType => _response.ContentType;

        public Stream Body => _response.OutputStream;

        public long ContentLength => _response.ContentLength64;

        public void Close()
        {
            _response.Close();
        }
    }
}
