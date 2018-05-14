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

    using JudoPayDotNet.Clients;

    // ReSharper disable UnusedMember.Global
    /// <summary>
    /// This factory creates an instance of the JudoPay client using the supplied credentials
    /// </summary>
    public static class JudoPaymentsFactory
    {
        private const string DEFAULT_SANDBOX_URL = "https://gw1.judopay-sandbox.com/";

        private const string DEFAULT_LIVE_URL = "https://gw1.judopay.com/";
        
        private const string LiveCertificatePublicKey = "MIIBCgKCAQEAqyx7Fg8FkI7Q2yaai//AXURuithFkoPfBliXOpGQ8O8vo+foXTLVpWmStnCUfzhm5dIJEgKn/FVK+/M5vGQSJ+aqhAL6A9Eq+UazOY2X65QweOQiQmcC6WELYBO+wx8oXQLp/PVYLlAfaljFRBqo3c4kfeLwd4VISJuFs941B7vTrkgZ0t6TSbnwUZNpSLr53pNyR4QJ/OSPsoxtdec7z+38dPUW0Ah9tscXa4lns5h3FvqEaY6bduYl7xQwO7LGGVaaYFmj4kMLn1Fyd+gw8vdRBd4NC7VCRJ2NxshMHdKwW4sS5YK+MT+s/3yAXlkhj9vXPczJAXBVNjn3jX4CWQIDAQAB";

        private const string SandboxCertificatePublicKey = "MIIBCgKCAQEAmMrGJkxm/vvfZ/IU0EuhljWlgxzdRnkgWzkzB1NGpOoZw1AJWYq3Lg1uOvphltQ+oq3athGIhoXYuQrOh7BsMpw2vXj1VTwGP9/1AkNOXXCzTVKATw+AwuBwdIYg0yOqTB4wImvLqDVFuuO6f0SnFZ3ntqlNFvOBzxGHKlr6Y20fsiXzv95vRfkwtb5exNUy9bnKn81GyPONWVeLgqFEM7TQO7eUbLEMcnEwgPCvMhYKggSN/i99wqcMomEBlfsfFxcYG7R6P8GmiXBkHKaPO2JXf4OMOMcLOmG7kyRZYBPWNTlQsUsgatTUTO1oFJuMYRIcUE+G51C2FLraCH2YqQIDAQAB";

        static JudoPaymentsFactory()
        {
            ServicePointManager.ServerCertificateValidationCallback += PinPublicKey;
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
                    defaultValue = DEFAULT_SANDBOX_URL;
                    break;
                case JudoEnvironment.Live:
                    defaultValue = DEFAULT_LIVE_URL;
                    break;
            }

            return defaultValue;
        }

        private static bool PinPublicKey(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (null == certificate)
                return false;

            var webRequest = sender as HttpWebRequest;
            if (webRequest != null)
            {
                // If we're connecting to the live system vault
                if (webRequest.Address.ToString().StartsWith(DEFAULT_LIVE_URL, StringComparison.InvariantCultureIgnoreCase))
                {
                    var serviceKey = Convert.ToBase64String(certificate.GetPublicKey());

                    // If the certifcate public key doesn't match fail the validation
                    if (serviceKey != LiveCertificatePublicKey)
                        return false;
                }

                // If we're connecting to the live system vault
                if (webRequest.Address.ToString().StartsWith(DEFAULT_SANDBOX_URL, StringComparison.InvariantCultureIgnoreCase))
                {
                    var serviceKey = Convert.ToBase64String(certificate.GetPublicKey());

                    // If the certifcate public key doesn't match fail the validation
                    if (serviceKey != SandboxCertificatePublicKey)
                        return false;
                }
            }

            // Propogate any previous errors
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
