using Autofac;
using DionysosFX.Module.IWebApi;
using DionysosFX.Swan.Net;
using System;

namespace DionysosFX.Module.WebApi
{
    [ApiController]
    public abstract class WebApiController : IDisposable
    {
        bool _disposed;

        public IHttpContext Context;

        public IContainer Container;

        public virtual void Dispose()
        {
            Dispose(true);                      
            GC.SuppressFinalize(this);
        }

        private void SetHttpContext(IHttpContext _context)
        {
            Context = _context;
        }

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
