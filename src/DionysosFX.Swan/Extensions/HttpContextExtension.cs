using DionysosFX.Swan.Net;

namespace DionysosFX.Swan.Extensions
{
    public static class HttpContextExtension
    {
        /// <summary>
        /// Add http response headers and provide use browser caching
        /// </summary>
        /// <param name="context"></param>
        /// <param name="expire"></param>
        public static void AddCacheExpire(this IHttpContext context, double expire) => context.Response.Headers.Add("Cache-Control", $"private,max-age={expire}");
    }
}
