using System;
using System.Configuration;
using JudoPayDotNet;
using JudoPayDotNet.Authentication;
using JudoPayDotNet.Enums;
using JudoPayDotNet.Http;
using JudoPayDotNetDotNet.Configuration;
using JudoPayDotNetDotNet.Logging;

namespace JudoPayDotNetDotNet
{
    using System.Net.Http.Headers;

    using JudoPayDotNet.Clients;

    // ReSharper disable UnusedMember.Global
    /// <summary>
    /// This factory creates an instance of the JudoPay client using the supplied credentials
    /// </summary>
    public static class JudoPaymentsFactory
    {
        private const string ApiVersionKey = "ApiVersion";

        private const string SandboxUrlKey = "SandboxUrl";

        private const string LiveUrlKey = "LiveUrl";

        private const string DEFAULT_SANDBOX_URL = "https://gw1.judopay-sandbox.com/";

        private const string DEFAULT_LIVE_URL = "https://gw1.judopay.com/";

        private static readonly IJudoConfiguration defaultConfigurationAccess = new JudoConfiguration();

        private static readonly Func<string, string, IJudoConfiguration, string> GetConfigValue = (key, defaultValue, configuration) =>
            {
                string configurationSetting;
                return string.IsNullOrWhiteSpace(configurationSetting = configuration[key]) ? defaultValue : configurationSetting;
            };


        private static JudoPayApi Create(Credentials credentials, string baseUrl, string apiVersion, ProductInfoHeaderValue userAgent)
        {
            var httpClient = new HttpClientWrapper(userAgent,
                                 new AuthorizationHandler(credentials, DotNetLoggerFactory.Create(typeof(AuthorizationHandler))),
                                 new VersioningHandler(apiVersion));

            var connection = new Connection(httpClient, DotNetLoggerFactory.Create, baseUrl);
            var client = new Client(connection);

            return new JudoPayApi(DotNetLoggerFactory.Create, client);
        }

        private static JudoPayApi Create(Credentials credentials, string baseUrl, IJudoConfiguration configuration, ProductInfoHeaderValue userAgent)
        {
            var apiVersion = GetConfigValue(ApiVersionKey, VersioningHandler.DEFAULT_API_VERSION, configuration);
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

        internal static string GetEnvironmentUrl(JudoEnvironment judoEnvironment, IJudoConfiguration configuration = null)
        {
            string key = null;
            string defaultValue = null;

            switch (judoEnvironment)
            {
                case JudoEnvironment.Sandbox:
                    key = SandboxUrlKey;
                    defaultValue = DEFAULT_SANDBOX_URL;
                    break;
                case JudoEnvironment.Live:
                    key = LiveUrlKey;
                    defaultValue = DEFAULT_LIVE_URL;
                    break;
            }

            return GetConfigValue(key, defaultValue, configuration ?? defaultConfigurationAccess);
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
            return Create(token, secret, GetEnvironmentUrl(judoEnvironment), null, defaultConfigurationAccess);
        }

        /// <summary>
        /// Creates an instance of the judopay api client with a custom base url, that will authenticate with your api token and secret.
        /// </summary>
        /// <remarks>This is intented for development/sandbox environments</remarks>
        /// <param name="token">Your API token (from our merchant dashboard)</param>
        /// <param name="secret">Your API secret (from our merchant dashboard)</param>
        /// <param name="baseUrl">Base URL for the Judopay api</param>
        /// <returns>Initialized instance of the Judopay api client</returns>
        public static JudoPayApi Create(string token, string secret, string baseUrl)
        {
            return Create(token, secret, baseUrl, null, defaultConfigurationAccess);
        }

        /// <summary>
        /// Creates an instance of the judopay api client with a custom base url, that will authenticate with your api token and secret.
        /// </summary>
        /// <remarks>This is intented for development/sandbox environments</remarks>
        /// <param name="token">Your API token (from our merchant dashboard)</param>
        /// <param name="secret">Your API secret (from our merchant dashboard)</param>
        /// <param name="baseUrl">Base URL for the Judopay api</param>
        /// <param name="configuration">Application configuration accessor</param>
        /// <returns>Initialized instance of the Judopay api client</returns>
        public static JudoPayApi Create(string token, string secret, string baseUrl, IJudoConfiguration configuration)
        {
            return Create(token, secret, baseUrl, null, configuration);
        }

        /// <summary>
        /// Creates an instance of the judopay api client with a custom base url, that will authenticate with your api token and secret.
        /// </summary>
        /// <remarks>This is intented for development/sandbox environments</remarks>
        /// <param name="token">Your API token (from our merchant dashboard)</param>
        /// <param name="secret">Your API secret (from our merchant dashboard)</param>
        /// <param name="baseUrl">Base URL for the Judopay api</param>
        /// <param name="userAgent">The name and version number of the calling application, should be in the form PRODUCT/VERSION</param>
        /// <param name="configuration">Application configuration accessor</param>
        /// <returns>Initialized instance of the Judopay api client</returns>
        public static JudoPayApi Create(string token, string secret, string baseUrl, ProductInfoHeaderValue userAgent, IJudoConfiguration configuration = null)
        {
            var credentials = new Credentials(token, secret);
            return Create(credentials, baseUrl, configuration ?? defaultConfigurationAccess, userAgent);
        }
    }

    // ReSharper restore UnusedMember.Global
}
