using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace DionysosFX.Host
{
    public static class WebServerOptionsExtensions
    {
        public static WebServerOptions WithUrlPrefix(this WebServerOptions @this, string prefix)
        {
            @this.AddUrlPrefix(prefix);
            return @this;
        }

        public static WebServerOptions WithUrlPrefixes(this WebServerOptions @this, IEnumerable<string> urlPrefixes)
        {
            foreach (var urlPrefix in urlPrefixes)
                @this.AddUrlPrefix(urlPrefix);
            return @this;
        }

        public static WebServerOptions WithUrlPrefixes(this WebServerOptions @this, params string[] urlPrefixes) => WithUrlPrefixes(@this, urlPrefixes as IEnumerable<string>);
    }
}
