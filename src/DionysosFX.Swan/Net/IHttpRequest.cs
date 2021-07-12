using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DionysosFX.Swan.Net
{
    public interface IHttpRequest
    {
        /// <summary>
        /// 
        /// </summary>
        bool IsSecureConnection
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        string UserHostAddress
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        string UserAgent
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        Uri UrlReferrer
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        Uri Url
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        TransportContext TransportContext
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        string ServiceName
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        Guid RequestTraceIdentifier
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        IPEndPoint RemoteEndPoint
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        string RawUrl
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        NameValueCollection QueryString
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        Version ProtocolVersion
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        IPEndPoint LocalEndPoint
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        bool KeepAlive
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        bool IsWebSocketRequest
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        string UserHostName
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        string[] UserLanguages
        {
            get;
        }

        //bool IsAuthenticated
        //{
        //    get;
        //}

        /// <summary>
        /// 
        /// </summary>
        Stream InputStream
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        string HttpMethod
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        NameValueCollection Headers
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        bool HasEntityBody
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        CookieCollection Cookies
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        string ContentType
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        long ContentLength64
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        Encoding ContentEncoding
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        int ClientCertificateError
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        string[] AcceptTypes
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        bool IsLocal
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestCallback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        IAsyncResult BeginGetClientCertificate(AsyncCallback requestCallback, object state);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="asyncResult"></param>
        /// <returns></returns>
        X509Certificate2 EndGetClientCertificate(IAsyncResult asyncResult);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        X509Certificate2 GetClientCertificate();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<X509Certificate2> GetClientCertificateAsync();

    }
}
