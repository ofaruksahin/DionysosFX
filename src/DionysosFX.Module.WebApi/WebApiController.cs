using Autofac;
using DionysosFX.Module.IWebApi;
using DionysosFX.Swan.Net;
using System;

namespace DionysosFX.Module.WebApi
{
    /// <summary>
    /// Abstract web api controller
    /// </summary>
    [ApiController]
    public abstract class WebApiController : IDisposable
    {
        /// <summary>
        /// Controller is disposed ?
        /// </summary>
        bool _disposed;

        /// <summary>
        /// HttpContext
        /// </summary>
        public IHttpContext Context;

        /// <summary>
        /// Autofac dependency injection container
        /// </summary>
        public IContainer Container;

        public virtual void Dispose()
        {
            Dispose(true);                      
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// SetHttpContext
        /// </summary>
        /// <param name="_context"></param>
        private void SetHttpContext(IHttpContext _context)
        {
            Context = _context;
        }

        /// <summary>
        /// SetContainer
        /// </summary>
        /// <param name="_container"></param>
        private void SetContainer(IContainer _container)
        {
            Container = _container;
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!_disposed)
                {
                    _disposed = true;
                }
            }
        }   

        public void Redirect(string url)
        {
            Context.Response.Redirect(url);
        }
    }
}
