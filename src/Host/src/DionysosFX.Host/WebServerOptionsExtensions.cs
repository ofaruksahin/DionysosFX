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

        public static WebServerOptions WithCertificate(this WebServerOptions @this, X509Certificate2 certificate)
        {
            @this.Certificate = certificate;
            return @this;
        }

        public static WebServerOptions WithCertificateThumbprint(this WebServerOptions @this, string thumbprint)
        {
            @this.CertificateThumbprint = thumbprint;
            return @this;
        }

        public static WebServerOptions WithAutoLoadCertificate(this WebServerOptions @this, bool autoLoad)
        {
            @this.AutoLoadCertificate = autoLoad;
            return @this;
        }

        public static WebServerOptions WithoutAutoLoadCertificate(this WebServerOptions @this)
        {
            @this.AutoLoadCertificate = false;
            return @this;
        }

        public static WebServerOptions WithAutoRegisterCertificate(this WebServerOptions @this, bool autoRegister)
        {
            @this.AutoRegisterCertificate = autoRegister;
            return @this;
        }

        public static WebServerOptions WithoutAutoRegisterCertificate(this WebServerOptions @this)
        {
            @this.AutoRegisterCertificate = false;
            return @this;
        }

        public static WebServerOptions WithStoreName(this WebServerOptions @this, StoreName storeName)
        {
            @this.StoreName = storeName;
            return @this;
        }

        public static WebServerOptions WithStoreLocation(this WebServerOptions @this, StoreLocation storeLocation)
        {
            @this.StoreLocation = storeLocation;
            return @this;
        }

        public static WebServerOptions WithStore(this WebServerOptions @this, StoreName storeName, StoreLocation @storeLocation)
        {
            @this.StoreName = storeName;
            @this.StoreLocation = storeLocation;
            return @this;
        }
    }
}
