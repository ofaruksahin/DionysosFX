using DionysosFX.Template.WebAPI.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DionysosFX.Template.WebAPI.Entities
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="statusCode"></param>
        public BaseResult(string message, HttpStatusCode statusCode)
        {
            Message = message;
            StatusCode = statusCode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <param name="statusCode"></param>
        public BaseResult(TResult data, string message, HttpStatusCode statusCode)
        {
            Data = data;
            Message = message;
            StatusCode = statusCode;
        }
    }
}
