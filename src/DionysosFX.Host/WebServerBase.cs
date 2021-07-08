using DionysosFX.Swan;
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
        /// 
        /// </summary>
        private WebServerState _state = WebServerState.Created;

        /// <summary>
        /// 
        /// </summary>
        private IHostBuilder _hostBuilder;

        /// <summary>
        /// 
        /// </summary>
        public IHostBuilder HostBuilder => _hostBuilder;

        public WebServerBase(IHostBuilder hostBuilder)
        {
            _hostBuilder = hostBuilder;
        }

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
            try
            {
                try
                {
                    if (context.CancellationToken.IsCancellationRequested)
                        return;
                    
                    try
                    {
                        using (var writer = new StreamWriter(context.Response.Body))
                            writer.WriteLine("<html><head><body><h1>First Web Server Application</h1></body></head></html>");
                    }
                    catch (OperationCanceledException) when (context.CancellationToken.IsCancellationRequested)
                    {
                        throw;
                    }
                    catch (HttpListenerException)
                    {
                        throw;
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                    }

                }
                finally
                {
                    await context.Response.Body.FlushAsync(context.CancellationToken).ConfigureAwait(false);
                    context.Close();
                }
            }
            catch (OperationCanceledException) when(context.CancellationToken.IsCancellationRequested)
            {

            }catch(HttpListenerException ex)
            {
                Console.WriteLine(ex);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                OnFatalException();
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
        /// 
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
