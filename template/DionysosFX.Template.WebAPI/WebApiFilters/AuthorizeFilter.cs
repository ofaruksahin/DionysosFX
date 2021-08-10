using DionysosFX.Swan.Net;
using DionysosFX.Swan.Routing;
using System;

namespace DionysosFX.Template.WebAPI.WebApiFilters
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method,AllowMultiple = false)]
    public class AuthorizeFilter : Attribute, IEndpointFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="resolveResponse"></param>
        public void OnAfter(object sender,IHttpContext httpContext)
        {            
            Console.WriteLine("On After Method : {0} {1}",DateTime.Now.ToLongTimeString(),httpContext.Request.Url);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="resolveResponse"></param>
        public void OnBefore(object sender,IHttpContext httpContext)
        {
            Console.WriteLine("On Before Method : {0} {1}", DateTime.Now.ToLongTimeString(), httpContext.Request.Url);
        }
    }
}
