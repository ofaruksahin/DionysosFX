using System.Threading;

namespace DionysosFX.Swan.Net
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHttpContext
    {
        /// <summary>
        /// 
        /// </summary>
        string Id
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        CancellationToken CancellationToken
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        IHttpRequest Request { get; }

        /// <summary>
        /// 
        /// </summary>
        IHttpResponse Response { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IsHandled { get; }

        /// <summary>
        /// 
        /// </summary>
        void Close();

        /// <summary>
        /// 
        /// </summary>
        void SetHandled();
    }
}
