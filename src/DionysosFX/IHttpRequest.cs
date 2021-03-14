using System.Collections.Specialized;
using System.IO;

namespace DionysosFX
{
    public interface IHttpRequest
    {
        NameValueCollection Query { get; }
        NameValueCollection Headers { get; }
        Stream Body { get; }
        string Path { get; }
        long ContentLength { get; }
        string ContentType { get; }
        NameValueCollection Form { get; }
        string Host { get; }
        bool IsHttps { get; }
        string Method { get; }
    }
}
