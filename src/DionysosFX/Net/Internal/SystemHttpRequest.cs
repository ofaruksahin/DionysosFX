using System;
using System.Collections.Specialized;
using System.IO;

namespace DionysosFX.Net.Internal
{
    public class SystemHttpRequest : IHttpRequest
    {
        private readonly System.Net.HttpListenerRequest _request;

        public SystemHttpRequest(System.Net.HttpListenerRequest request)
        {
            _request = request;
        }

        public NameValueCollection Query => throw new NotImplementedException();

        public NameValueCollection Headers => throw new NotImplementedException();

        public Stream Body => throw new NotImplementedException();

        public string Path => throw new NotImplementedException();

        public long ContentLength => throw new NotImplementedException();

        public string ContentType => throw new NotImplementedException();

        public NameValueCollection Form => throw new NotImplementedException();

        public string Host => throw new NotImplementedException();

        public bool IsHttps => throw new NotImplementedException();

        public string Method => throw new NotImplementedException();
    }
}
