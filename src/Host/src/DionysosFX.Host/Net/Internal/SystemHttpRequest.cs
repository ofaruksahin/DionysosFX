using HttpMultipartParser;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DionysosFX.Host.Net.Internal
{
    public class SystemHttpRequest : IHttpRequest
    {
        private readonly System.Net.HttpListenerRequest _request;

        public SystemHttpRequest(System.Net.HttpListenerRequest request)
        {
            _request = request;

            string text;
            using (var reader = new StreamReader(_request.InputStream,
                                                 _request.ContentEncoding))
            {
                text = reader.ReadToEnd();
            }

            if (!String.IsNullOrEmpty(text))
                multipartParser = MultipartFormDataParser.Parse(_request.InputStream, _request.ContentEncoding);
        }

        public NameValueCollection Query => _request.QueryString;

        public NameValueCollection Headers => _request.Headers;

        public Stream Body => _request.InputStream;

        public Uri Path => _request.Url;

        public long ContentLength => _request.ContentLength64;

        public string ContentType => _request.ContentType;

        private MultipartFormDataParser multipartParser = default;
        public IReadOnlyList<ParameterPart> Form
        {
            get
            {
                return multipartParser?.Parameters;
            }
        }
        public IReadOnlyList<FilePart> Files
        {
            get
            {
                return multipartParser?.Files;
            }
        }

        public string Host => _request.Url.Host;

        public bool IsHttps => _request.IsSecureConnection;

        public string Method => _request.HttpMethod;

    }
}
