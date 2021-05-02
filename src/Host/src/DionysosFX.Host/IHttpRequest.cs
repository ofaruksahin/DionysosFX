using HttpMultipartParser;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;

namespace DionysosFX.Host
{
    public interface IHttpRequest
    {
        NameValueCollection Query { get; }
        NameValueCollection Headers { get; }
        Stream Body { get; }
        Uri Path { get; }
        long ContentLength { get; }
        string ContentType { get; }
        IReadOnlyList<ParameterPart> Form { get; }
        IReadOnlyList<FilePart> Files { get; }
        string Host { get; }
        bool IsHttps { get; }
        string Method { get; }
    }
}
