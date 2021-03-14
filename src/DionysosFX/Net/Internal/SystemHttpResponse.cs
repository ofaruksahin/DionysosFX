using System;
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

        public int StatusCode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public NameValueCollection Headers => throw new NotImplementedException();

        public string ContentType => throw new NotImplementedException();

        public Stream Body => throw new NotImplementedException();

        public long ContentLength => throw new NotImplementedException();

        public void Close()
        {
            throw new NotImplementedException();
        }
    }
}
