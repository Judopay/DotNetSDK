using JudoPayDotNet;
using JudoPayDotNet.Autentication;
using JudoPayDotNet.Client;
using JudoPayDotNet.Http;
using JudoPayDotNetDotNet.Logging;

namespace JudoPayDotNetDotNet
{
    public static class JudoPaymentsFactory
    {
        public static JudoPayments Create(string token, string secret, string address)
        {
            var credentials = new Credentials(token, secret);
            var httpClient = new HttpClientWrapper();
            var connection = new Connection(httpClient,
                                            DotNetLoggerFactory.Create(typeof(Connection)),  
                                            address);
            var client = new Client(connection);

            return new JudoPayments(credentials, client);
        }
    }
}
