using DionysosFX.Swan;
using DionysosFX.Swan.Modules;
using DionysosFX.Swan.Net;
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.Host
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TStarup"></typeparam>
    public abstract class WebServerBase<TStarup> : ConfiguredObject, IWebServer, IHttpContextHandler
        where TStarup : IHostBuilder
    {
        /// <summary>
        /// Store web listening status
        /// </summary>
        private WebServerState _state = WebServerState.Created;

        /// <summary>
        /// DionysosFX App Instance Configs
        /// </summary>
        private IHostBuilder _hostBuilder;

        /// <summary>
        /// DionysosFX App Instance Configs
        /// </summary>
        public IHostBuilder HostBuilder => _hostBuilder;      

        /// <summary>
        /// Store web listening status
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

        public WebServerBase(IHostBuilder hostBuilder)
        {
            _hostBuilder = hostBuilder;
        }

        /// <summary>
        /// Server state change method
        /// </summary>
        public event EventHandler<WebServerStateChangeEventArgs> StateChanged;

        /// <summary>
        /// Server handle the exception and trigger a 'OnFatalException' event
        /// </summary>
        public event EventHandler<OnFatalExceptionEventArgs> OnFatalException;

        /// <summary>
        /// DionysosFX App Prepare Method
        /// </summary>
        /// <param name="cancellationToken"></param>
        protected virtual void Prepare(CancellationToken cancellationToken)
        {

        }

        /// <summary>
        /// Handle a new web request after than process request method
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected abstract Task ProcessRequestAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Handled web request and process request method
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected async Task DoHandleContextAsync(IHttpContextImpl context)
        {
            try
            {
                if (context.CancellationToken.IsCancellationRequested)
                    return;

                context.Container = HostBuilder.Container;
                await _hostBuilder.ModuleCollection.DispatchRequestAsync(context).ConfigureAwait(false);
                if (!context.IsHandled)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    context.SetHandled();
                    context.Close();
                }
            }
            catch (OperationCanceledException) when (context.CancellationToken.IsCancellationRequested)
            {

            }
            catch (HttpListenerException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                OnFatalException?.Invoke(this, new OnFatalExceptionEventArgs(context, ex));
            }
            finally
            {
                if (!context.IsHandled)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                }
                context.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task HandleContextAsync(IHttpContextImpl context)
        {
        }

        /// <summary>
        /// Start DionysosFX App
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task RunAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                State = WebServerState.Loading;
                Prepare(cancellationToken);
                State = WebServerState.Listening;
                _hostBuilder.ModuleCollection.Start(cancellationToken);
                await ProcessRequestAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
            }
            finally
            {
                State = WebServerState.Stopped;
            }
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
