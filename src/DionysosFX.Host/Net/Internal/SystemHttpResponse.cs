﻿using DionysosFX.Swan.Associations;
using DionysosFX.Swan.Net;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace DionysosFX.Host.Net.Internal
{
    /// <summary>
    /// 
    /// </summary>
    internal class SystemHttpResponse : IHttpResponse
    {
        private SystemHttpContext _context;
        private System.Net.HttpListenerResponse _response;

        public SystemHttpResponse(SystemHttpContext context,System.Net.HttpListenerResponse response)
        {
            _context = context;
            _response = response;
        }

        public long ContentLength64
        {
            get => _response.ContentLength64;
            set => _response.ContentLength64 = value;
        }
        public bool SendChunked
        {
            get => _response.SendChunked;
            set => _response.SendChunked = value;
        }
        public string RedirectLocation
        {
            get => _response.RedirectLocation;
            set => _response.RedirectLocation = value;
        }

        public Version ProtocolVersion
        {
            get => _response.ProtocolVersion;
            set => _response.ProtocolVersion = value;
        }

        public Stream OutputStream => _response.OutputStream;

        public bool KeepAlive
        {
            get => _response.KeepAlive;
            set => _response.KeepAlive = value;
        }
        public WebHeaderCollection Headers
        {
            get => _response.Headers;
        }

        public CookieCollection Cookies
        {
            get => _response.Cookies;
            set => _response.Cookies = value;
        }

        public string ContentType
        {
            get => _response.ContentType;
            set => _response.ContentType = value;
        }

        public int StatusCode
        {
            get => _response.StatusCode;
            set
            {
                _response.StatusCode = value;
                StatusDescription = HttpStatusDescription.Get(value);
            }
        }

        public string StatusDescription
        {
            get => _response.StatusDescription;
            set => _response.StatusDescription = value;
        }

        public Encoding ContentEncoding
        {
            get => _response.ContentEncoding;
            set => _response.ContentEncoding = value;
        }
        WebHeaderCollection IHttpResponse.Headers
        {
            get => _response.Headers;
            set => _response.Headers = value;
        }

        public void Abort()
        {
            _response.Abort();
        }

        public void AddHeader(string name, string value)
        {
            _response.AddHeader(name, value);
        }

        public void AppendCookie(Cookie cookie)
        {
            _response.AppendCookie(cookie);
        }

        public void AppendHeader(string name, string value)
        {
            _response.AppendHeader(name, value);
        }

        public void Close()
        {
            _response.Close();
            _context.SetHandled();
        }

        public void Close(byte[] responseEntity, bool willBlock)
        {
            _response.Close(responseEntity, willBlock);
            _context.SetHandled();
        }

        public void CopyFrom(HttpListenerResponse templateResponse)
        {
            _response.CopyFrom(templateResponse);
        }

        public void Redirect(string url)
        {
            _response.Redirect(url);
            _context.SetHandled();
        }

        public void SetCookie(Cookie cookie)
        {
            _response.SetCookie(cookie);
        }
    }
}
