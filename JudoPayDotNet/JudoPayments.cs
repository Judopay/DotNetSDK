using JudoPayDotNet.Autentication;
using JudoPayDotNet.Client;
using JudoPayDotNet.Clients;

namespace JudoPayDotNet
{
    public class JudoPayments : IJudoPayments
    {
        public IPayments Payments { get; set; }
        public IRefunds Refunds { get; set; }
        public IPreAuths PreAuths { get; set; }
        public ITransactions Transactions { get; set; }
        public ICollections Collections { get; set; }

        private readonly IClient client;
        private readonly ICredentials credentials;                 

        public JudoPayments(ICredentials credentials, IClient client)
        {
            this.client = client;
            this.credentials = credentials;

            Payments = new Payments(client);
            Refunds = new Refunds(client);
            PreAuths = new PreAuths(client);
            Transactions = new Transactions(client);
            Collections = new Collections(client);
        }

    }
}
