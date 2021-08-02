using DionysosFX.Module.WebApi;
using DionysosFX.Swan.Net;
using System;

namespace DionysosFX.Template.WebAPI.WebApiFilters
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method,AllowMultiple = false)]
    public class AuthorizeFilter : Attribute, IWebApiFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        public void OnAfter(IHttpContext httpContext)
        {
            Console.WriteLine("OnAfter");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        public void OnBefore(IHttpContext httpContext)
        {
            Console.WriteLine("OnBefore");
        }
    }
}
