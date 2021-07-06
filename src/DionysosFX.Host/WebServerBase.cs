using DionysosFX.Swan;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.Host
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    public abstract class WebServerBase<TOptions> : ConfiguredObject, IWebServer, IHttpContextHandler
        where TOptions : WebServerOptionsBase, new()
    {
        /// <summary>
        /// 
        /// </summary>
        private WebServerState _state = WebServerState.Created;

        /// <summary>
        /// 
        /// </summary>
        private TOptions _options = default;

        /// <summary>
        /// 
        /// </summary>
        public TOptions options => _options;

        /// <summary>
        /// 
        /// </summary>
        public WebServerState State
        {
            get => _state;
            set
            {
                if (value == _state)
                    return;

                var oldState = _state;
                _state = value;

                if (_state != WebServerState.Created)
                    LockConfiguration();

                StateChanged?.Invoke(this, new WebServerStateChangeEventArgs(oldState, value));

            }
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<WebServerStateChangeEventArgs> StateChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public WebServerBase([NotNull]TOptions options)
        {
            _options = options;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        protected virtual void Prepare(CancellationToken cancellationToken)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected abstract Task ProcessRequestAsync(CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        protected abstract void OnFatalException();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected async Task DoHandleContextAsync(IHttpContextImpl context)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task HandleContextAsync(IHttpContextImpl context)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task RunAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
                return;
        }

        /// <summary>
        /// 
        /// </summary>
        ~WebServerBase()
        {
            Dispose(false);
        }
    }
}
