using DionysosFX.Shared;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX
{
    public abstract class WebServerBase<TOptions> : ConfiguredObject, IWebServer
        where TOptions :WebServerOptionsBase,new()
    {

        private WebServerState _state = WebServerState.Created;
        public TOptions Options { get; }

        protected WebServerBase() : this(new TOptions())
        {

        }

        protected WebServerBase([NotNull]TOptions options)
        {

        }

        ~WebServerBase()
        {
            Dispose(false);
        }

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

        public event EventHandler<WebServerStateChangeEventArgs> StateChanged;       

        public async Task RunAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                State = WebServerState.Loading;
                Prepare(cancellationToken);

                State = WebServerState.Listening;
                await ProcessRequestsAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)when(cancellationToken.IsCancellationRequested)
            {
            }
            finally
            {
                State = WebServerState.Stopped;
            }
        }

        protected virtual void Prepare(CancellationToken cancellationToken)
        {

        }

        protected abstract Task ProcessRequestsAsync(CancellationToken cancellationToken);

        protected abstract void OnFatalException();


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
                return;
        }
    }
}
