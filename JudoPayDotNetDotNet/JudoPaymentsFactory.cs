using System.Configuration;
using JudoPayDotNet;
using JudoPayDotNet.Autentication;
using JudoPayDotNet.Http;
using JudoPayDotNetDotNet.Logging;

namespace JudoPayDotNetDotNet
{
    public static class JudoPaymentsFactory
    {
        private const string APIVERSIONHEADER = "api-version";

        private static JudoPayments Create(Credentials credentials, string address)
        {
            var apiVersion = ConfigurationManager.AppSettings["ApiVersion"];

            var httpClient = new HttpClientWrapper(new AuthorizationHandler(credentials,
                                                    DotNetLoggerFactory.Create(typeof(AuthorizationHandler))),
                                                    new VersioningHandler(APIVERSIONHEADER, apiVersion));
            var connection = new Connection(httpClient,
                                            DotNetLoggerFactory.Create(typeof(Connection)),
                                            address);
            var client = new Client(connection);

            return new JudoPayments(type => DotNetLoggerFactory.Create(type), credentials, client);
        }

        public static JudoPayments Create(string token, string secret, string address)
        {
            var credentials = new Credentials(token, secret);
            return Create(credentials, address);
        }

        public static JudoPayments Create(string oauthAccessToken, string address)
        {
            var credentials = new Credentials(oauthAccessToken);
            return Create(credentials, address);
        }
    }
}
