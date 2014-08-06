using System.Configuration;
using JudoPayDotNet;
using JudoPayDotNet.Autentication;
using JudoPayDotNet.Enums;
using JudoPayDotNet.Http;
using JudoPayDotNetDotNet.Logging;

namespace JudoPayDotNetDotNet
{
    // ReSharper disable UnusedMember.Global
    public static class JudoPaymentsFactory
    {
        private const string Apiversionheader = "api-version";
        private const string SandboxUrlKey = "SandboxUrl";
        private const string LiveUrlKey = "LiveUrl";

        private static JudoPayments Create(Credentials credentials, string address)
        {
            var apiVersion = ConfigurationManager.AppSettings["ApiVersion"];

            var httpClient = new HttpClientWrapper(new AuthorizationHandler(credentials,
                                                    DotNetLoggerFactory.Create(typeof(AuthorizationHandler))),
                                                    new VersioningHandler(Apiversionheader, apiVersion));
            var connection = new Connection(httpClient,
                                            DotNetLoggerFactory.Create,
                                            address);
            var client = new Client(connection);

            return new JudoPayments(DotNetLoggerFactory.Create, client);
        }

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

        public static JudoPayments Create(Environment environment, string token, string secret)
        {
            return Create(token, secret, GetEnvironmentUrl(environment));
        }

        public static JudoPayments Create(string token, string secret, string address)
        {
            var credentials = new Credentials(token, secret);
            return Create(credentials, address);
        }

        public static JudoPayments Create(Environment environment, string oauthAccessToken)
        {
            return Create(oauthAccessToken, GetEnvironmentUrl(environment));
        }

        public static JudoPayments Create(string oauthAccessToken, string address)
        {
            var credentials = new Credentials(oauthAccessToken);
            return Create(credentials, address);
        }
    }
    // ReSharper restore UnusedMember.Global
}
