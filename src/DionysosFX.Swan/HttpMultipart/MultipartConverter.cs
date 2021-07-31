using DionysosFX.Swan.Net;
using System;

namespace DionysosFX.Swan.HttpMultipart
{
    public static class MultipartConverter
    {
        public static object ToFormObject(this IHttpContext @this,Type destType)
        {
            return new { };
        }
    }
}
