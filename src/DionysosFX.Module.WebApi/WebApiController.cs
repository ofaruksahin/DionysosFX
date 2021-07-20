using System;

namespace DionysosFX.Module.WebApi
{
    [ApiController]
    public abstract class WebApiController : IDisposable
    {
        bool _disposed;
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
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
    }
}
