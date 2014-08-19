using System.Configuration;
using JudoPayDotNet;
using JudoPayDotNet.Authentication;
using JudoPayDotNet.Enums;
using JudoPayDotNet.Http;
using JudoPayDotNetDotNet.Logging;

namespace JudoPayDotNetDotNet
{
    // ReSharper disable UnusedMember.Global
	/// <summary>
	/// This factory creates an instance of the JudoPay client using the supplied credentials
	/// </summary>
    public static class JudoPaymentsFactory
    {
        private const string Apiversionheader = "api-version";
        private const string SandboxUrlKey = "SandboxUrl";
        private const string LiveUrlKey = "LiveUrl";

        private static JudoPayments Create(Credentials credentials, string baseUrl)
        {
            var apiVersion = ConfigurationManager.AppSettings["ApiVersion"];

            var httpClient = new HttpClientWrapper(new AuthorizationHandler(credentials,
                                                    DotNetLoggerFactory.Create(typeof(AuthorizationHandler))),
                                                    new VersioningHandler(Apiversionheader, apiVersion));
            var connection = new Connection(httpClient,
                                            DotNetLoggerFactory.Create,
                                            baseUrl);
            var client = new Client(connection);

            return new JudoPayments(DotNetLoggerFactory.Create, client);
        }

		/// <summary>
		/// Returns the url of a pre-configured environment
		/// </summary>
		/// <param name="environment"></param>
		/// <returns></returns>
        private static string GetEnvironmentUrl(Environment environment)
        {
            string url = null;

            switch (environment)
            {
                case Environment.Sandbox:
                    url = ConfigurationManager.AppSettings[SandboxUrlKey];
                    break;
                case Environment.Live:
                    url = ConfigurationManager.AppSettings[LiveUrlKey];
                    break;
            }

            return url;
        }

		/// <summary>
		/// Creates an instance of the judopay api client with a custom base url, that will authenticate with your api token and secret.
		/// </summary>
		/// <param name="environment">Either the sandbox (development/testing) or live environments</param>
		/// <param name="token">Your API token (from our merchant dashboard)</param>
		/// <param name="secret">Your API secret (from our merchant dashboard)</param>
		public static JudoPayments Create(Environment environment, string token, string secret)
        {
            return Create(token, secret, GetEnvironmentUrl(environment));
        }

		/// <summary>
		/// Creates an instance of the judopay api client with a custom base url, that will authenticate with your api token and secret.
		/// </summary>
		/// <remarks>This is intented for development/sandbox environments</remarks>
		/// <param name="token"></param>
		/// <param name="secret"></param>
		/// <param name="baseUrl"></param>
		/// <returns></returns>
		internal static JudoPayments Create(string token, string secret, string baseUrl)
        {
            var credentials = new Credentials(token, secret);
            return Create(credentials, baseUrl);
        }

		/// <summary>
		/// Creates an instance of the judopay api client with a custom base url, that will authenticate with the supplied OAuth access token
		/// </summary>
		/// <param name="environment">Either the sandbox (development/testing) or live environments</param>
		/// <param name="oauthAccessToken">Your marketplace seller's access token</param>
		/// <returns></returns>
        public static JudoPayments Create(Environment environment, string oauthAccessToken)
        {
            return Create(oauthAccessToken, GetEnvironmentUrl(environment));
        }

		/// <summary>
		/// Creates an instance of the judopay api client with a custom base url, that will authenticate with the supplied OAuth access token
		/// </summary>
		/// <remarks>This is intented for development/sandbox environments</remarks>
		/// <param name="oauthAccessToken">Your marketplace seller's access token</param>
		/// <param name="baseUrl"></param>
		/// <returns></returns>
		internal static JudoPayments Create(string oauthAccessToken, string baseUrl)
        {
            var credentials = new Credentials(oauthAccessToken);
            return Create(credentials, baseUrl);
        }
    }
    // ReSharper restore UnusedMember.Global
}
