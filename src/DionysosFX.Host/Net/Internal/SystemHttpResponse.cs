using DionysosFX.Swan.Associations;
using DionysosFX.Swan.Net;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace DionysosFX.Host.Net.Internal
{
    /// <summary>
    /// Web Response object
    /// </summary>
    internal class SystemHttpResponse : IHttpResponse
    {
        /// <summary>
        /// Use it to store the content of request
        /// </summary>
        private SystemHttpContext _context;
        /// <summary>
        /// Represents a response to a request being handled by an System.Net.HttpListener object
        /// </summary>
        private System.Net.HttpListenerResponse _response;

        public SystemHttpResponse(SystemHttpContext context,System.Net.HttpListenerResponse response)
        {
            _context = context;
            _response = response;
        }

        /// <summary>
        /// Gets or sets the number of bytes in the body data included in the response.
        /// </summary>
        public long ContentLength64
        {
            get => _response.ContentLength64;
            set => _response.ContentLength64 = value;
        }

        /// <summary>
        /// Gets or sets whether the response uses chunked transfer encoding.
        /// </summary>
        public bool SendChunked
        {
            get => _response.SendChunked;
            set => _response.SendChunked = value;
        }

        /// <summary>
        /// Gets or sets the value of the HTTP Location header in this response.
        /// </summary>
        public string RedirectLocation
        {
            get => _response.RedirectLocation;
            set => _response.RedirectLocation = value;
        }

        /// <summary>
        /// Gets or sets the HTTP version used for the response.
        /// </summary>
        public Version ProtocolVersion
        {
            get => _response.ProtocolVersion;
            set => _response.ProtocolVersion = value;
        }

        /// <summary>
        ///  Gets a System.IO.Stream object to which a response can be written.
        /// </summary>
        public Stream OutputStream => _response.OutputStream;

        /// <summary>
        /// Gets or sets a value indicating whether the server requests a persistent connection.
        /// </summary>
        public bool KeepAlive
        {
            get => _response.KeepAlive;
            set => _response.KeepAlive = value;
        }

        /// <summary>
        /// Gets or sets the collection of header name/value pairs returned by the server.
        /// </summary>
        public WebHeaderCollection Headers
        {
            get => _response.Headers;
        }

        /// <summary>
        /// Gets or sets the collection of cookies returned with the response.
        /// </summary>
        public CookieCollection Cookies
        {
            get => _response.Cookies;
            set => _response.Cookies = value;
        }

        /// <summary>
        /// Gets or sets the collection of cookies returned with the response.
        /// </summary>
        public string ContentType
        {
            get => _response.ContentType;
            set => _response.ContentType = value;
        }

        /// <summary>
        ///  Gets or sets the HTTP status code to be returned to the client.
        /// </summary>
        public int StatusCode
        {
            get => _response.StatusCode;
            set
            {
                _response.StatusCode = value;
                StatusDescription = HttpStatusDescription.Get(value);
            }
        }

        /// <summary>
        /// Gets or sets a text description of the HTTP status code returned to the client.
        /// </summary>
        public string StatusDescription
        {
            get => _response.StatusDescription;
            set => _response.StatusDescription = value;
        }

        /// <summary>
        ///  Gets or sets the System.Text.Encoding for this response's System.Net.HttpListenerResponse.OutputStream.
        /// </summary>
        public Encoding ContentEncoding
        {
            get => _response.ContentEncoding;
            set => _response.ContentEncoding = value;
        }

        /// <summary>
        ///  Gets or sets the collection of header name/value pairs returned by the server.
        /// </summary>
        WebHeaderCollection IHttpResponse.Headers
        {
            get => _response.Headers;
            set => _response.Headers = value;
        }

        /// <summary>
        /// Closes the connection to the client without sending a response.
        /// </summary>
        public void Abort()
        {
            _response.Abort();
        }

        /// <summary>
        /// Adds the specified header and value to the HTTP headers for this response.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddHeader(string name, string value)
        {
            _response.AddHeader(name, value);
        }

        /// <summary>
        /// Adds the specified System.Net.Cookie to the collection of cookies for this response.
        /// </summary>
        /// <param name="cookie"></param>
        public void AppendCookie(Cookie cookie)
        {
            _response.AppendCookie(cookie);
        }

        /// <summary>
        /// Appends a value to the specified HTTP header to be sent with this response.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AppendHeader(string name, string value)
        {
            _response.AppendHeader(name, value);
        }

        /// <summary>
        /// Sends the response to the client and releases the resources held by this System.Net.HttpListenerResponse instance.
        /// </summary>
        public void Close()
        {
            _response.Close();
            _context.SetHandled();
        }

        /// <summary>
        /// Returns the specified byte array to the client and releases the resources held
        /// by this System.Net.HttpListenerResponse instance.</summary>
        /// <param name="responseEntity"></param>
        /// <param name="willBlock"></param>
        public void Close(byte[] responseEntity, bool willBlock)
        {
            _response.Close(responseEntity, willBlock);
            _context.SetHandled();
        }

        /// <summary>
        /// Copies properties from the specified System.Net.HttpListenerResponse to this
        /// response.</summary>
        /// <param name="templateResponse"></param>
        public void CopyFrom(HttpListenerResponse templateResponse)
        {
            _response.CopyFrom(templateResponse);
        }

        /// <summary>
        /// Configures the response to redirect the client to the specified URL.
        /// </summary>
        /// <param name="url"></param>
        public void Redirect(string url)
        {
            _response.Redirect(url);
            _context.SetHandled();
        }

        /// <summary>
        /// Adds or updates a System.Net.Cookie in the collection of cookies sent with this
        /// response.</summary>
        /// <param name="cookie"></param>
        public void SetCookie(Cookie cookie)
        {
            _response.SetCookie(cookie);
        }
    }
}
