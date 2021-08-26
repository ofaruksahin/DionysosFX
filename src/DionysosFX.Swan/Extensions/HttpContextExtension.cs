using DionysosFX.Swan.Net;

namespace DionysosFX.Swan.Extensions
{
    public static class HttpContextExtension
    {
        public static void AddCacheExpire(this IHttpContext context, double expire) => context.Response.Headers.Add("Cache-Control", $"private,max-age={expire}");
    }
}
