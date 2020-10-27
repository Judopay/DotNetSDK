using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using JudoPayDotNet.Authentication;
using JudoPayDotNet.Enums;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;

namespace JudoPayDotNet
{
    using System.Collections.Generic;
    using System.Net.Http.Headers;

    // ReSharper disable UnusedMember.Global
    /// <summary>
    /// This factory creates an instance of the JudoPay client using the supplied credentials
    /// </summary>
    public static class JudoPaymentsFactory
    {
        // Future state will use https://api-sandbox.judopay.com/ with *.judopay.com certificate
        private const string DefaultSandboxUrl = "https://gw1.judopay-sandbox.com/";

        // Future state will use https://api.judopay.com/ with *.judopay.com certificate
        private const string DefaultLiveUrl = "https://gw1.judopay.com/";
        
        private static readonly List<string> LiveCertificatePublicKeys = new List<string>
        {
            // Public key for gw1.judopay.com
            "MIIBCgKCAQEAqyx7Fg8FkI7Q2yaai//AXURuithFkoPfBliXOpGQ8O8vo+foXTLVpWmStnCUfzhm5dIJEgKn/FVK+/M5vGQSJ+aqhAL6A9Eq+UazOY2X65QweOQiQmcC6WELYBO+wx8oXQLp/PVYLlAfaljFRBqo3c4kfeLwd4VISJuFs941B7vTrkgZ0t6TSbnwUZNpSLr53pNyR4QJ/OSPsoxtdec7z+38dPUW0Ah9tscXa4lns5h3FvqEaY6bduYl7xQwO7LGGVaaYFmj4kMLn1Fyd+gw8vdRBd4NC7VCRJ2NxshMHdKwW4sS5YK+MT+s/3yAXlkhj9vXPczJAXBVNjn3jX4CWQIDAQAB",
            // Fallback public key for gw1.judopay.com
            "MIIBCAKCAQEAn5miKk6Db6bAofUTUU4BMfbQ5YL8OqzwVxbTqsbebECh+NdkB1v38+2yLll3brjF1fGPqgHYHmKO90ZLrgOh/CYAnhNH472v4+UAr0xFTQUEcZe0oTjI5wvRBnQeOZk182n5DaZvyOoOdDhZ6l8nfrAX58fO2DmRduQ0+GFBSrnh6dbzc4Z9XmmomLR6YOVF9AFe4ns0lP7uEW0wdh7p5sGRXqwQXhpfXHS+gZkgp4zPgpD7iHtaO+DlzJTMBZKDF8jVaGeFYO2Tj4s244TbeM8dClSr8oosAsu+5t5zXgZYrn0XkcQv2kyjnZ3wfs9j2OKbTzj1xNw8JSf7MOcnIwIBJQ==",
            // Public key for *.judopay.com
            "MIIBCgKCAQEAqyx7Fg8FkI7Q2yaai//AXURuithFkoPfBliXOpGQ8O8vo+foXTLVpWmStnCUfzhm5dIJEgKn/FVK+/M5vGQSJ+aqhAL6A9Eq+UazOY2X65QweOQiQmcC6WELYBO+wx8oXQLp/PVYLlAfaljFRBqo3c4kfeLwd4VISJuFs941B7vTrkgZ0t6TSbnwUZNpSLr53pNyR4QJ/OSPsoxtdec7z+38dPUW0Ah9tscXa4lns5h3FvqEaY6bduYl7xQwO7LGGVaaYFmj4kMLn1Fyd+gw8vdRBd4NC7VCRJ2NxshMHdKwW4sS5YK+MT+s/3yAXlkhj9vXPczJAXBVNjn3jX4CWQIDAQAB"
        };

        private static readonly List<string> SandboxCertificatePublicKeys = new List<string>
        {
            // Public key for *.judopay-sandbox.com used by gw1.judopay-sandbox.com 
            "MIIBCgKCAQEAmMrGJkxm/vvfZ/IU0EuhljWlgxzdRnkgWzkzB1NGpOoZw1AJWYq3Lg1uOvphltQ+oq3athGIhoXYuQrOh7BsMpw2vXj1VTwGP9/1AkNOXXCzTVKATw+AwuBwdIYg0yOqTB4wImvLqDVFuuO6f0SnFZ3ntqlNFvOBzxGHKlr6Y20fsiXzv95vRfkwtb5exNUy9bnKn81GyPONWVeLgqFEM7TQO7eUbLEMcnEwgPCvMhYKggSN/i99wqcMomEBlfsfFxcYG7R6P8GmiXBkHKaPO2JXf4OMOMcLOmG7kyRZYBPWNTlQsUsgatTUTO1oFJuMYRIcUE+G51C2FLraCH2YqQIDAQAB",
            // Public key for *.judopay.com
            "MIIBCgKCAQEAqyx7Fg8FkI7Q2yaai//AXURuithFkoPfBliXOpGQ8O8vo+foXTLVpWmStnCUfzhm5dIJEgKn/FVK+/M5vGQSJ+aqhAL6A9Eq+UazOY2X65QweOQiQmcC6WELYBO+wx8oXQLp/PVYLlAfaljFRBqo3c4kfeLwd4VISJuFs941B7vTrkgZ0t6TSbnwUZNpSLr53pNyR4QJ/OSPsoxtdec7z+38dPUW0Ah9tscXa4lns5h3FvqEaY6bduYl7xQwO7LGGVaaYFmj4kMLn1Fyd+gw8vdRBd4NC7VCRJ2NxshMHdKwW4sS5YK+MT+s/3yAXlkhj9vXPczJAXBVNjn3jX4CWQIDAQAB"
        };

        static JudoPaymentsFactory()
        {
            ServicePointManager.ServerCertificateValidationCallback = PinPublicKey;
        }

        /// <summary>
        /// Factory method for the benefit of platform tests that need to have finer grained control of the API version
        /// </summary>
        /// <param name="credentials">The api token and secret to use</param>
        /// <param name="baseUrl">Base URL for the host</param>
        /// <param name="apiVersion">The api version to use</param>
        /// <param name="userAgent">User-Agent details to set in the header for each request</param>
        /// <returns>Initialized instance of the Judopay api client</returns>
        internal static JudoPayApi Create(Credentials credentials, string baseUrl, string apiVersion, ProductInfoHeaderValue userAgent)
        {
            var userAgentCollection = new List<ProductInfoHeaderValue>();
            userAgentCollection.Add(new ProductInfoHeaderValue("DotNetCLR", Environment.Version.ToString()));
            userAgentCollection.Add(new ProductInfoHeaderValue(Environment.OSVersion.Platform.ToString(), Environment.OSVersion.Version.ToString()));
            if (userAgent != null) userAgentCollection.Add(userAgent);
            var httpClient = new HttpClientWrapper(
                                 userAgentCollection,
                                 new AuthorizationHandler(credentials, DotNetLoggerFactory.Create(typeof(AuthorizationHandler))),
                                 new VersioningHandler(apiVersion));

            var connection = new Connection(httpClient, DotNetLoggerFactory.Create, baseUrl);
            var client = new Client(connection);

            return new JudoPayApi(DotNetLoggerFactory.Create, client);
        }

        private static JudoPayApi Create(Credentials credentials, string baseUrl, ProductInfoHeaderValue userAgent)
        {
            //var apiVersion = GetConfigValue(ApiVersionKey, VersioningHandler.DEFAULT_API_VERSION, configuration);
            var apiVersion = VersioningHandler.DEFAULT_API_VERSION;
            return Create(credentials, baseUrl, apiVersion, userAgent);
        }

        /// <summary>
        /// Factory method for the benefit of platform tests that need to have finer grained control of the API version
        /// </summary>
        /// <param name="credentials">The api token and secret to use</param>
        /// <param name="baseUrl">Base URL for the host</param>
        /// <param name="apiVersion">The api version to use</param>
        /// <returns>Initialized instance of the Judopay api client</returns>
        internal static JudoPayApi Create(Credentials credentials, string baseUrl, string apiVersion)
        {
            return Create(credentials, baseUrl, apiVersion, null);
        }

        internal static JudoPayApi Create(Credentials credentials, JudoEnvironment judoEnvironment, string apiVersion)
        {
            return Create(credentials, GetEnvironmentUrl(judoEnvironment), apiVersion, null);
        }

        internal static string GetEnvironmentUrl(JudoEnvironment judoEnvironment)
        {
            string defaultValue = null;

            switch (judoEnvironment)
            {
                case JudoEnvironment.Sandbox:
                    defaultValue = DefaultSandboxUrl;
                    break;
                case JudoEnvironment.Live:
                    defaultValue = DefaultLiveUrl;
                    break;
            }

            return defaultValue;
        }

        internal static bool PinPublicKey(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (null == certificate)
                return false;

            if (sender is HttpWebRequest webRequest)
            {
                // If we're connecting to the live environment
                if (webRequest.Address.ToString().StartsWith(DefaultLiveUrl, StringComparison.InvariantCultureIgnoreCase))
                {
                    var serviceKey = Convert.ToBase64String(certificate.GetPublicKey());

                    // If the certifcate public key doesn't match fail the validation
                    if (!LiveCertificatePublicKeys.Contains(serviceKey))
                        return false;
                }
                // If we're connecting to the sandbox environment
                else if (webRequest.Address.ToString().StartsWith(DefaultSandboxUrl, StringComparison.InvariantCultureIgnoreCase))
                {
                    var serviceKey = Convert.ToBase64String(certificate.GetPublicKey());

                    // If the certificate public key doesn't match fail the validation
                    if (!SandboxCertificatePublicKeys.Contains(serviceKey))
                        return false;
                }
            }

            // Propagate any previous errors
            return sslPolicyErrors == SslPolicyErrors.None;
        }

        /// <summary>
        /// Creates an instance of the judopay api client with a custom base url, that will authenticate with your api token and secret.
        /// </summary>
        /// <param name="judoEnvironment">Either the sandbox (development/testing) or live environments</param>
        /// <param name="token">Your API token (from our merchant dashboard)</param>
        /// <param name="secret">Your API secret (from our merchant dashboard)</param>
        /// <returns>Initialized instance of the Judopay api client</returns>
        public static JudoPayApi Create(JudoEnvironment judoEnvironment, string token, string secret)
        {
            return Create(token, secret, GetEnvironmentUrl(judoEnvironment));
        }

        /// <summary>
        /// Creates an instance of the judopay api client with a custom base url, that will authenticate with your api token and secret.
        /// </summary>
        /// <param name="judoEnvironment">Either the sandbox (development/testing) or live environments</param>
        /// <param name="token">Your API token (from our merchant dashboard)</param>
        /// <param name="secret">Your API secret (from our merchant dashboard)</param>
        /// <param name="userAgent">The name and version number of the calling application, should be in the form PRODUCT/VERSION</param>
        /// <returns>Initialized instance of the Judopay api client</returns>
        public static JudoPayApi Create(JudoEnvironment judoEnvironment, string token, string secret, ProductInfoHeaderValue userAgent)
        {
            return Create(new Credentials(token, secret), GetEnvironmentUrl(judoEnvironment), userAgent);
        }

        /// <summary>
        /// Creates an instance of the judopay API client with a custom base url, that will authenticate with your API token and secret.
        /// </summary>
        /// <remarks>This is intended for development/sandbox environments</remarks>
        /// <param name="token">Your API token (from our merchant dashboard)</param>
        /// <param name="secret">Your API secret (from our merchant dashboard)</param>
        /// <param name="baseUrl">Base URL for the Judopay api</param>
        /// <returns>Initialized instance of the Judopay api client</returns>
        public static JudoPayApi Create(string token, string secret, string baseUrl)
        {
            var credentials = new Credentials(token, secret);
            return Create(credentials, baseUrl, (ProductInfoHeaderValue)null);
        }
    }

    // ReSharper restore UnusedMember.Global
}
