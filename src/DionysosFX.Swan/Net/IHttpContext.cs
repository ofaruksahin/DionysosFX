using Autofac;
using System.Threading;

namespace DionysosFX.Swan.Net
{
    /// <summary>
    /// HttpContext
    /// </summary>
    public interface IHttpContext
    {
        /// <summary>
        /// Request Unique Id
        /// </summary>
        string Id
        {
            get;
        }

        /// <summary>
        /// Request Cancellation Token
        /// </summary>
        CancellationToken CancellationToken
        {
            get;
        }

        /// <summary>
        /// Request Object
        /// </summary>
        IHttpRequest Request 
        { 
            get;
        }

        /// <summary>
        /// Response Object
        /// </summary>
        IHttpResponse Response 
        { 
            get;
        }

        /// <summary>
        /// Request IsHandled
        /// </summary>
        bool IsHandled 
        { 
            get;
        }

        /// <summary>
        /// Request Close
        /// </summary>
        void Close();

        /// <summary>
        /// Request Set Handled
        /// </summary>
        void SetHandled();

        /// <summary>
        /// Web Server Dependency Injection Container
        /// </summary>
        IContainer Container
        {
            get;
            set;
        }
    }
}
