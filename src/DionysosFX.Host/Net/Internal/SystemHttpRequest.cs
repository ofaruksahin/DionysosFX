using DionysosFX.Swan.Net;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DionysosFX.Host.Net.Internal
{
    /// <summary>
    /// 
    /// </summary>
    internal class SystemHttpRequest : IHttpRequest
    {
        /// <summary>
        /// 
        /// </summary>
        private System.Net.HttpListenerRequest _request;

        public SystemHttpRequest(System.Net.HttpListenerRequest request)
        {
            _request = request;
        }

        public bool IsSecureConnection => _request.IsSecureConnection;

        public string UserHostAddress => _request.UserHostAddress;

        public string UserAgent => _request.UserAgent;

        public Uri UrlReferrer => _request.UrlReferrer;

        public Uri Url => _request.Url;

        public TransportContext TransportContext => _request.TransportContext;

        public string ServiceName => _request.ServiceName;

        public Guid RequestTraceIdentifier => _request.RequestTraceIdentifier;

        public IPEndPoint RemoteEndPoint => _request.RemoteEndPoint;

        public string RawUrl => _request.RawUrl;

        public NameValueCollection QueryString => _request.QueryString;

        public Version ProtocolVersion => _request.ProtocolVersion;

        public IPEndPoint LocalEndPoint => _request.LocalEndPoint;

        public bool KeepAlive => _request.KeepAlive;

        public bool IsWebSocketRequest => _request.IsWebSocketRequest;

        public string UserHostName => _request.UserHostName;

        public string[] UserLanguages => _request.UserLanguages;

        public Stream InputStream => _request.InputStream;

        public string HttpMethod => _request.HttpMethod;

        public NameValueCollection Headers => _request.Headers;

        public bool HasEntityBody => _request.HasEntityBody;

        public CookieCollection Cookies => _request.Cookies;

        public string ContentType => _request.ContentType;

        public long ContentLength64 => _request.ContentLength64;

        public Encoding ContentEncoding => _request.ContentEncoding;

        public int ClientCertificateError => _request.ClientCertificateError;

        public string[] AcceptTypes => _request.AcceptTypes;

        public bool IsLocal => _request.IsLocal;

        public IAsyncResult BeginGetClientCertificate(AsyncCallback requestCallback, object state) => _request.BeginGetClientCertificate(requestCallback, state);

        public X509Certificate2 EndGetClientCertificate(IAsyncResult asyncResult) => _request.EndGetClientCertificate(asyncResult);

        public X509Certificate2 GetClientCertificate() => _request.GetClientCertificate();

        public Task<X509Certificate2> GetClientCertificateAsync() => _request.GetClientCertificateAsync();        
    }
}
