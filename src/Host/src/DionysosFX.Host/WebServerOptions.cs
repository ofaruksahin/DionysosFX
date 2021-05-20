using DionysosFX.Swan;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PlatformNotSupportedException = DionysosFX.Swan.PlatformNotSupportedException;

namespace DionysosFX.Host
{
    public sealed class WebServerOptions : WebServerOptionsBase
    {
        private readonly List<string> _urlPrefixes = new();

        public IReadOnlyList<string> UrlPrefixes => _urlPrefixes;

        public void AddUrlPrefix([NotNull] string prefix)
        {
            EnsureConfigurationNotLocked();
            if (_urlPrefixes.Contains(prefix))
                throw new ArgumentException("URL Prefix is already registered.", nameof(prefix));

            _urlPrefixes.Add(prefix);
        }
    }
}
