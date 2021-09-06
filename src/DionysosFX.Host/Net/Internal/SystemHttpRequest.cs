using DionysosFX.Swan.Net;
using HttpMultipartParser;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DionysosFX.Host.Net.Internal
{
    /// <summary>
    /// Web Request object
    /// </summary>
    internal class SystemHttpRequest : IHttpRequest
    {
        /// <summary>
        /// .Net default web request object
        /// </summary>
        private System.Net.HttpListenerRequest _request;

        public SystemHttpRequest(System.Net.HttpListenerRequest request)
        {
            _request = request;

            if (!String.IsNullOrEmpty(ContentType) && ContentType.Contains("multipart/form-data"))
            {
                multipartFormDataParser = MultipartFormDataParser.Parse(InputStream);
                _form = multipartFormDataParser.Parameters;
                _files = multipartFormDataParser.Files;
            }
            else
            {
                _form = new List<ParameterPart>();
                _files = new List<FilePart>();
            }
        }

        /// <summary>
        /// Request is with came ssl certificate
        /// </summary>
        public bool IsSecureConnection => _request.IsSecureConnection;

        /// <summary>
        /// User host address info
        /// </summary>
        public string UserHostAddress => _request.UserHostAddress;

        /// <summary>
        /// User browser info
        /// </summary>
        public string UserAgent => _request.UserAgent;

        /// <summary>
        /// User referrerUrl
        /// </summary>
        public Uri UrlReferrer => _request.UrlReferrer;

        /// <summary>
        /// User send to request which one url ?
        /// </summary>
        public Uri Url => _request.Url;

        /// <summary>
        /// The System.Net.TransportContext class provides additional context about the underlying
        /// </summary>
        public TransportContext TransportContext => _request.TransportContext;

        /// <summary>
        /// Gets the Service Provider Name (SPN) that the client sent on the request.
        /// </summary>
        public string ServiceName => _request.ServiceName;

        /// <summary>
        /// Gets the request identifier of the incoming HTTP request.
        /// </summary>
        public Guid RequestTraceIdentifier => _request.RequestTraceIdentifier;

        /// <summary>
        /// Gets the client IP address and port number from which the request originated.
        /// </summary>
        public IPEndPoint RemoteEndPoint => _request.RemoteEndPoint;

        /// <summary>
        /// Gets the URL information (without the host and port) requested by the client.
        /// </summary>
        public string RawUrl => _request.RawUrl;

        /// <summary>
        /// Gets the query string included in the request.
        /// </summary>
        public NameValueCollection QueryString => _request.QueryString;

        /// <summary>
        /// Gets the HTTP version used by the requesting client.
        /// </summary>
        public Version ProtocolVersion => _request.ProtocolVersion;

        /// <summary>
        /// Gets the server IP address and port number to which the request is directed.
        /// </summary>
        public IPEndPoint LocalEndPoint => _request.LocalEndPoint;

        /// <summary>
        /// Gets a System.Boolean value that indicates whether the client requests a persistent connection.
        /// </summary>
        public bool KeepAlive => _request.KeepAlive;

        /// <summary>
        /// Gets a System.Boolean value that indicates whether the TCP connection was a WebSocket
        /// request.
        /// </summary>
        public bool IsWebSocketRequest => _request.IsWebSocketRequest;

        /// <summary>
        /// Gets the DNS name and, if provided, the port number specified by the client.
        /// </summary>
        public string UserHostName => _request.UserHostName;

        /// <summary>
        ///  Gets the natural languages that are preferred for the response.
        /// </summary>
        public string[] UserLanguages => _request.UserLanguages;

        /// <summary>
        /// Request Content Type is multipart form data ? is yeah, body in store multipartformdataparser object
        /// </summary>
        private MultipartFormDataParser multipartFormDataParser = null;

        /// <summary>
        ///  Gets a stream that contains the body data sent by the client.
        /// </summary>
        public Stream InputStream => _request.InputStream;

        /// <summary>
        /// Gets the HTTP method specified by the client.
        /// </summary>
        public string HttpMethod => _request.HttpMethod;

        /// <summary>
        /// Gets the collection of header name/value pairs sent in the request.
        /// </summary>
        public NameValueCollection Headers => _request.Headers;

        /// <summary>
        /// Gets a System.Boolean value that indicates whether the request has associated body data.
        /// </summary>
        public bool HasEntityBody => _request.HasEntityBody;

        /// <summary>
        /// Gets the cookies sent with the request.
        /// </summary>
        public CookieCollection Cookies => _request.Cookies;

        /// <summary>
        /// Gets the MIME type of the body data included in the request.
        /// </summary>
        public string ContentType => _request.ContentType;

        /// <summary>
        /// Gets the length of the body data included in the request.
        /// </summary>
        public long ContentLength64 => _request.ContentLength64;

        /// <summary>
        /// Gets the content encoding that can be used with data sent with the request.
        /// </summary>
        public Encoding ContentEncoding => _request.ContentEncoding;

        /// <summary>
        /// Gets an error code that identifies a problem with the System.Security.Cryptography.X509Certificates.X509Certificate provided by the client.
        /// </summary>
        public int ClientCertificateError => _request.ClientCertificateError;

        /// <summary>
        /// Gets the MIME types accepted by the client.
        /// </summary>
        public string[] AcceptTypes => _request.AcceptTypes;

        /// <summary>
        /// Gets a System.Boolean value that indicates whether the request is sent from the local computer.
        /// </summary>
        public bool IsLocal => _request.IsLocal;

        /// <summary>
        /// WebRequest content type is multipart ? is yeah, store parameter in object
        /// </summary>
        private IReadOnlyList<ParameterPart> _form = null;
        public IReadOnlyList<ParameterPart> Form => _form;

        /// <summary>
        /// WebRequest content type is multipart ? is yeah, store files in object
        /// </summary>
        private IReadOnlyList<FilePart> _files = null;
        public IReadOnlyList<FilePart> Files => _files;

        /// <summary>
        /// Begins an asynchronous request for the client's X.509 v.3 certificate.
        /// </summary>
        /// <param name="requestCallback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public IAsyncResult BeginGetClientCertificate(AsyncCallback requestCallback, object state) => _request.BeginGetClientCertificate(requestCallback, state);

        /// <summary>
        ///  Ends an asynchronous request for the client's X.509 v.3 certificate.
        /// </summary>
        /// <param name="asyncResult"></param>
        /// <returns></returns>
        public X509Certificate2 EndGetClientCertificate(IAsyncResult asyncResult) => _request.EndGetClientCertificate(asyncResult);

        /// <summary>
        /// Retrieves the client's X.509 v.3 certificate.
        /// </summary>
        /// <returns></returns>
        public X509Certificate2 GetClientCertificate() => _request.GetClientCertificate();

        /// <summary>
        /// Retrieves the client's X.509 v.3 certificate as an asynchronous operation.
        /// </summary>
        /// <returns></returns>
        public Task<X509Certificate2> GetClientCertificateAsync() => _request.GetClientCertificateAsync();
    }
}
