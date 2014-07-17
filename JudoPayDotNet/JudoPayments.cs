using System;
using JudoPayDotNet.Autentication;
using JudoPayDotNet.Clients;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;

namespace JudoPayDotNet
{
    public class JudoPayments : IJudoPayments
    {
        public IPayments Payments { get; set; }
        public IRefunds Refunds { get; set; }
        public IPreAuths PreAuths { get; set; }
        public ITransactions Transactions { get; set; }
        public ICollections Collections { get; set; }
        public IThreeDs ThreeDs { get; set; }

        private readonly IClient client;
        private readonly ICredentials credentials;
        private readonly Func<Type, ILog> logger;

        public JudoPayments(Func<Type, ILog> log, ICredentials credentials, IClient client)
        {
            this.client = client;
            this.credentials = credentials;
            this.logger = log;

            Payments = new Payments(logger(typeof(Payments)), client);
            Refunds = new Refunds(logger(typeof(Refunds)), client);
            PreAuths = new PreAuths(logger(typeof(PreAuths)), client);
            Transactions = new Transactions(logger(typeof(Transactions)), client);
            Collections = new Collections(logger(typeof(Collections)), client);
            ThreeDs = new ThreeDs(logger(typeof(ThreeDs)), client);
        }

    }
}
