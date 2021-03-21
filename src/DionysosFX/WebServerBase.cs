using DionysosFX.Shared;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX
{
    public abstract class WebServerBase<TOptions> : ConfiguredObject, IWebServer,IHttpContextHandler
        where TOptions :WebServerOptionsBase,new()
    {

        private WebServerState _state = WebServerState.Created;
        public TOptions Options { get; set; }


        protected WebServerBase([NotNull]TOptions options)
        {
            Options = options;
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
                    catch (OperationCanceledException) when(context.CancellationToken.IsCancellationRequested)
                    {
                        throw;
                    }
                    catch (HttpListenerException)
                    {
                        throw;
                    }catch(Exception exception)
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
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
                OnFatalException();
            }
        }

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

        public Task HandleContextAsync(IHttpContextImpl context)
        {
            throw new NotImplementedException();
        }
    }
}
