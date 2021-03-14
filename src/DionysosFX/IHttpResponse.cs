using System.Collections.Specialized;
using System.IO;

namespace DionysosFX
{
    public interface IHttpResponse
    {
        int StatusCode { get; set; }
        NameValueCollection Headers { get; }
        string ContentType { get; }
        Stream Body { get; }
        long ContentLength { get; }
        void Close();
    }
}
