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
        private X509Certificate2 _certificate;
        private string? _certificateThumbprint;
        private bool _autoLoadCertificate;
        private bool _autoRegisterCertificate;
        private StoreName _storeName = StoreName.My;
        private StoreLocation _storeLocation = StoreLocation.LocalMachine;

        public IReadOnlyList<string> UrlPrefixes => _urlPrefixes;

        public X509Certificate2 Certificate
        {
            get
            {
                if (AutoRegisterCertificate)
                    return TryRegisterCertificate() ? _certificate : null;
                return _certificate ?? (AutoLoadCertificate ? LoadCertificate() : null);
            }
            set
            {
                EnsureConfigurationNotLocked();

                _certificate = value;
            }
        }

        public string? CertificateThumbprint
        {
            get => _certificateThumbprint;
            set
            {
                EnsureConfigurationNotLocked();

                _certificateThumbprint = value == null
                    ? null : Regex.Replace(value, @"[^\da-fA-F]", string.Empty).ToUpper(CultureInfo.InvariantCulture);
            }
        }

        public bool AutoLoadCertificate
        {
            get => _autoLoadCertificate;
            set
            {
                EnsureConfigurationNotLocked();
                if (value && !DionysosFXRuntime.IsWindows)
                    throw new PlatformNotSupportedException(_autoLoadCertificate.GetType().Name);
            }
        }

        public bool AutoRegisterCertificate
        {
            get => _autoRegisterCertificate;
            set
            {
                EnsureConfigurationNotLocked();
                if (value && !DionysosFXRuntime.IsWindows)
                    throw new PlatformNotSupportedException(_autoRegisterCertificate.GetType().Name);

                _autoRegisterCertificate = value;
            }
        }

        public StoreName StoreName
        {
            get => _storeName;
            set
            {
                EnsureConfigurationNotLocked();
                _storeName = value;
            }
        }

        public StoreLocation StoreLocation
        {
            get => _storeLocation;
            set
            {
                EnsureConfigurationNotLocked();
                _storeLocation = value;
            }
        }

        public void AddUrlPrefix([NotNull] string prefix)
        {
            EnsureConfigurationNotLocked();
            if (_urlPrefixes.Contains(prefix))
                throw new ArgumentException("URL Prefix is already registered.", nameof(prefix));

            _urlPrefixes.Add(prefix);
        }

        private X509Certificate2? LoadCertificate()
        {
            if (!DionysosFXRuntime.IsWindows)
                return null;

            if (!string.IsNullOrWhiteSpace(_certificateThumbprint))
                return GetCertificate(_certificateThumbprint);

            using var netsh = GetNetsh("show");

            string? thumbprint = null;

            if (!netsh.Start())
                return null;

            netsh.BeginOutputReadLine();
            netsh.BeginErrorReadLine();
            netsh.WaitForExit();

            return netsh.ExitCode == 0 && !string.IsNullOrEmpty(thumbprint)
                 ? GetCertificate(thumbprint)
                 : null;
        }

        private X509Certificate2? GetCertificate(string? thumbprint = null)
        {
            using var store = new X509Store(StoreName, StoreLocation);
            store.Open(OpenFlags.ReadOnly);
            var signinCert = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint ?? _certificateThumbprint, false);
            return signinCert.Count == 0 ? null : signinCert[0];
        }

        private bool AddCertificateToStore()
        {
            using var store = new X509Store(StoreName, StoreLocation);
            try
            {
                store.Open(OpenFlags.ReadWrite);
                store.Add(_certificate);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool TryRegisterCertificate()
        {
            if (!DionysosFXRuntime.IsWindows)
                return false;

            if (_certificate == null)
                throw new InvalidOperationException("A certificate is required to auto AutoRegister");

            if (GetCertificate(_certificate.Thumbprint) == null && !AddCertificateToStore())
                throw new InvalidOperationException("The provided certificate cannot be added to the default store, add it manually");

            using var netsh = GetNetsh("add", $"certhash={_certificate.Thumbprint} appid={{adaa04bb-8b63-4073-a12f-d6f8c0b4383f}}");

            var sb = new StringBuilder();

            void PushLine(object sender, DataReceivedEventArgs e)
            {
                if (string.IsNullOrWhiteSpace(e.Data))
                    return;

                sb.AppendLine(e.Data);
            }

            netsh.OutputDataReceived += PushLine;
            netsh.ErrorDataReceived += PushLine;

            if (!netsh.Start())
                return false;

            netsh.BeginOutputReadLine();
            netsh.BeginErrorReadLine();
            netsh.WaitForExit();

            return netsh.ExitCode == 0 ? true : throw new InvalidOperationException($"NetSh Error: {sb}");
        }

        private int GetSslPort()
        {
            var port = 443;

            foreach (var url in UrlPrefixes.Where(x => x.StartsWith("https:", StringComparison.OrdinalIgnoreCase)))
            {
                var match = Regex.Match(url, @":(\d+)");

                if (match.Success && int.TryParse(match.Groups[1].Value, out port))
                    break;
            }

            return port;
        }

        private Process GetNetsh(string verb, string options = "") => new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "netsh",
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                Arguments = $"http {verb} sslcert ipport=0.0.0.0:{GetSslPort()} {options}",
            },
        };
    }
}
