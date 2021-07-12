using System;
using System.IO;
using System.Net;
using System.Text;

namespace DionysosFX.Swan.Net
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHttpResponse
    {
        long ContentLength64
        {
            get;
            set;
        }

        bool SendChunked
        {
            get;
            set;
        }

        string RedirectLocation
        {
            get;
            set;
        }

        Version ProtocolVersion
        {
            get;
            set;
        }

        Stream OutputStream
        {
            get;
        }

        bool KeepAlive
        {
            get;
            set;
        }

        WebHeaderCollection Headers
        {
            get;
            set;
        }

        CookieCollection Cookies
        {
            get;
            set;
        }

        string ContentType
        {
            get;
            set;
        }

        int StatusCode
        {
            get;
            set;
        }

        string StatusDescription
        {
            get;
            set;
        }

        Encoding ContentEncoding
        {
            get;
            set;
        }

        void Abort();

        void AddHeader(string name, string value);

        void AppendCookie(Cookie cookie);

        void AppendHeader(string name, string value);

        void Close(byte[] responseEntity, bool willBlock);

        void CopyFrom(HttpListenerResponse templateResponse);

        void Redirect(string url);

        void SetCookie(Cookie cookie);

        void Close();
    }
}
