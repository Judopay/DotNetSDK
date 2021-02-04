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
        private const string DefaultSandboxUrl = "https://api-sandbox.judopay.com/"; // Uses *.judopay.com certificate
        private const string LegacySandboxUrl = "https://gw1.judopay-sandbox.com/"; // Uses *.judopay-sandbox.com certificate

        private const string DefaultLiveUrl = "https://api.judopay.com/"; // Uses *.judopay.com certificate
        private const string LegacyLiveUrl = "https://gw1.judopay.com/"; // Uses gw1.judopay.com certificate

        private static readonly List<string> ApiCertificatePublicKeys = new List<string>
        {
            // Public key for *.judopay.com (also used for gw1.judopay.com)
            "MIIBCgKCAQEAqyx7Fg8FkI7Q2yaai//AXURuithFkoPfBliXOpGQ8O8vo+foXTLVpWmStnCUfzhm5dIJEgKn/FVK+/M5vGQSJ+aqhAL6A9Eq+UazOY2X65QweOQiQmcC6WELYBO+wx8oXQLp/PVYLlAfaljFRBqo3c4kfeLwd4VISJuFs941B7vTrkgZ0t6TSbnwUZNpSLr53pNyR4QJ/OSPsoxtdec7z+38dPUW0Ah9tscXa4lns5h3FvqEaY6bduYl7xQwO7LGGVaaYFmj4kMLn1Fyd+gw8vdRBd4NC7VCRJ2NxshMHdKwW4sS5YK+MT+s/3yAXlkhj9vXPczJAXBVNjn3jX4CWQIDAQAB",
            // Fallback public key for *.judopay.com (also used for gw1.judopay.com)
            "MIIBCAKCAQEAn5miKk6Db6bAofUTUU4BMfbQ5YL8OqzwVxbTqsbebECh+NdkB1v38+2yLll3brjF1fGPqgHYHmKO90ZLrgOh/CYAnhNH472v4+UAr0xFTQUEcZe0oTjI5wvRBnQeOZk182n5DaZvyOoOdDhZ6l8nfrAX58fO2DmRduQ0+GFBSrnh6dbzc4Z9XmmomLR6YOVF9AFe4ns0lP7uEW0wdh7p5sGRXqwQXhpfXHS+gZkgp4zPgpD7iHtaO+DlzJTMBZKDF8jVaGeFYO2Tj4s244TbeM8dClSr8oosAsu+5t5zXgZYrn0XkcQv2kyjnZ3wfs9j2OKbTzj1xNw8JSf7MOcnIwIBJQ=="
        };

        private static readonly List<string> LegacySandboxCertificatePublicKeys = new List<string>
        {
            // Public key for *.judopay-sandbox.com used by gw1.judopay-sandbox.com
            "MIIBCgKCAQEAmMrGJkxm/vvfZ/IU0EuhljWlgxzdRnkgWzkzB1NGpOoZw1AJWYq3Lg1uOvphltQ+oq3athGIhoXYuQrOh7BsMpw2vXj1VTwGP9/1AkNOXXCzTVKATw+AwuBwdIYg0yOqTB4wImvLqDVFuuO6f0SnFZ3ntqlNFvOBzxGHKlr6Y20fsiXzv95vRfkwtb5exNUy9bnKn81GyPONWVeLgqFEM7TQO7eUbLEMcnEwgPCvMhYKggSN/i99wqcMomEBlfsfFxcYG7R6P8GmiXBkHKaPO2JXf4OMOMcLOmG7kyRZYBPWNTlQsUsgatTUTO1oFJuMYRIcUE+G51C2FLraCH2YqQIDAQAB",
            // Fallback public key for *.judopay-sandbox.com used by gw1.judopay-sandbox.com
            "MIIBCAKCAQEA4J29vB5RqkGKubPT4wjTn8Ww8StXSOAIP36d0YantOb03X1aiC/+WZQo3/z1D3H1anGn7gqJicdMNAJPC+JIpI2ov1cbCjSN+6Gm5j0+igi8Q8MWY/IdCPQjOVxdQb6P8wEaImieQQdJEeHZlSt3hVb+dIi9EhMX182GfSjXVhv1lnCjxvFYcZviMNF3zA6DcjBsLxoKWU0AbHF8VXvemp15WV+jAq0IeTTzr97vE+hGEeGIZfWqamms4nSTYXUlnFD3NgqhBmzBeBorr/MzWhUz5+qdmZYGcbUjNwKovZvR/TXGIwz9mE2IWjrbxhQllH5VDo2iHT5gLMUSCkefjwIBJQ=="
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
            var userAgentCollection = new List<ProductInfoHeaderValue>
            {
                new ProductInfoHeaderValue("DotNetCLR", Environment.Version.ToString()),
                new ProductInfoHeaderValue(Environment.OSVersion.Platform.ToString(), Environment.OSVersion.Version.ToString())
            };

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
            var apiVersion = VersioningHandler.DEFAULT_API_VERSION;
            //var apiVersion = GetConfigValue(ApiVersionKey, VersioningHandler.DEFAULT_API_VERSION, configuration);

            if (!string.IsNullOrEmpty(credentials.PaymentSession))
            {
                apiVersion = VersioningHandler.PAYMENT_SESSION_API_VERSION;
            }

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

        private static bool PinPublicKey(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (null == certificate)
                return false;

            if (sender is HttpWebRequest webRequest)
            {
                var serviceKey = Convert.ToBase64String(certificate.GetPublicKey());
                var publicKeys = GetPublicKeys(webRequest.Address.ToString());
                if (publicKeys.Count > 0 && !publicKeys.Contains(serviceKey))
                    return false;
            }

            // Propagate any previous errors
            return sslPolicyErrors == SslPolicyErrors.None;
        }

        private static List<string> GetPublicKeys(string baseUrl)
        {
            var publicKeys = new List<string>();
            if (baseUrl.StartsWith(DefaultLiveUrl, StringComparison.InvariantCultureIgnoreCase) ||
                baseUrl.StartsWith(DefaultSandboxUrl, StringComparison.InvariantCultureIgnoreCase) ||
                baseUrl.StartsWith(LegacyLiveUrl, StringComparison.InvariantCultureIgnoreCase))
            {
                // Default api.judopay.com and api-sandbox.judopay.com Urls use *.judopay.com certificate
                // Legacy live gw1.judopay.com certificate uses same public key as *.judopay.com certficate
                publicKeys = ApiCertificatePublicKeys;
            }
            else if (baseUrl.StartsWith(LegacySandboxUrl, StringComparison.InvariantCultureIgnoreCase))
            {
                // Legacy sandbox Url uses *.judopay-sandbox.com certificate - keep supporting for now as
                // merchants may still specify the LegacySandboxUrl using the Create method with baseUrl parameter
                publicKeys = LegacySandboxCertificatePublicKeys;
            }
            return publicKeys;
        }

        /// <summary>
        /// Creates an instance of the Judopay API client against a specified environment, that will authenticate with your api token and secret.
        /// </summary>
        /// <param name="judoEnvironment">Either the Sandbox (development/testing) or Live environments</param>
        /// <param name="token">Your API token (from our merchant dashboard)</param>
        /// <param name="secret">Your API secret (from our merchant dashboard)</param>
        /// <returns>Initialized instance of the Judopay API client</returns>
        public static JudoPayApi Create(JudoEnvironment judoEnvironment, string token, string secret)
        {
            return Create(token, secret, GetEnvironmentUrl(judoEnvironment));
        }

        /// <summary>
        /// Creates an instance of the judopay api client with a custom base url, that will authenticate with your api token and payment session.
        /// </summary>
        /// <param name="judoEnvironment">Either the sandbox (development/testing) or live environments</param>
        /// <param name="token">Your API token (from our merchant dashboard)</param>
        /// <param name="paymentSession">Your one-time payment session created with the same API token </param>
        /// <returns>Initialized instance of the Judopay api client</returns>
        public static JudoPayApi CreateWithPaymentSession(JudoEnvironment judoEnvironment, string token, string paymentSession)
        {
            return CreateWithPaymentSession(token, paymentSession, GetEnvironmentUrl(judoEnvironment));
        }

        /// <summary>
        /// Creates an instance of the Judopay API client against a specified environment using a custom userAgent,
        /// that will authenticate with your api token and secret.
        /// </summary>
        /// <param name="judoEnvironment">Either the Sandbox (development/testing) or Live environments</param>
        /// <param name="token">Your API token (from our merchant dashboard)</param>
        /// <param name="secret">Your API secret (from our merchant dashboard)</param>
        /// <param name="userAgent">The name and version number of the calling application, should be in the form PRODUCT/VERSION</param>
        /// <returns>Initialized instance of the Judopay API client</returns>
        public static JudoPayApi Create(JudoEnvironment judoEnvironment, string token, string secret, ProductInfoHeaderValue userAgent)
        {
            var credentials = new Credentials(token, secret);
            return Create(credentials, GetEnvironmentUrl(judoEnvironment), userAgent);
        }

        /// <summary>
        /// Creates an instance of the Judopay API client with a custom base url, that will authenticate with your API token and secret.
        /// </summary>
        /// <remarks>This is intended for development/sandbox environments</remarks>
        /// <param name="token">Your API token (from our merchant dashboard)</param>
        /// <param name="secret">Your API secret (from our merchant dashboard)</param>
        /// <param name="baseUrl">Base URL for the Judopay API</param>
        /// <returns>Initialized instance of the Judopay API client</returns>
        public static JudoPayApi Create(string token, string secret, string baseUrl)
        {
            var credentials = new Credentials(token, secret);
            return Create(credentials, baseUrl, (ProductInfoHeaderValue)null);
        }

        /// <summary>
        /// Creates an instance of the judopay API client with a custom base url, that will authenticate with your API token and payment session.
        /// </summary>
        /// <remarks>This is intended for development/sandbox environments</remarks>
        /// <param name="token">Your API token (from our merchant dashboard)</param>
        /// <param name="paymentSession">Your one-time payment session created with the same API token </param>
        /// <param name="baseUrl">Base URL for the Judopay api</param>
        /// <returns>Initialized instance of the Judopay api client</returns>
        public static JudoPayApi CreateWithPaymentSession(string token, string paymentSession, string baseUrl)
        {
            var credentials = new Credentials(token, null);
            credentials.SetPaymentSession(paymentSession);
            return Create(credentials, baseUrl, (ProductInfoHeaderValue)null);
        }
    }

    // ReSharper restore UnusedMember.Global
}
