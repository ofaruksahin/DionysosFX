using System.Net;

namespace DionysosFX.Module.Test
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class BaseResult<TResult>
    {
        /// <summary>
        /// 
        /// </summary>
        public TResult Data
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Message
        {
            get;
            set;
        } = Messages.Success;

        /// <summary>
        /// 
        /// </summary>
        public HttpStatusCode StatusCode
        {
            get;
            set;
        } = HttpStatusCode.OK;
    }
}
