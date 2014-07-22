using System;
using JudoPayDotNet.Autentication;
using JudoPayDotNet.Clients;
using JudoPayDotNet.Clients.Consumer;
using JudoPayDotNet.Clients.Market;
using JudoPayDotNet.Clients.Merchant;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;

namespace JudoPayDotNet
{
    public class JudoPayments : IJudoPayments
    {
        public IMarket Market { get; set; }

        public IMerchants Merchants { get; set; }

        public IWebPayments WebPayments { get; set; }

        public IConsumers Consumers { get; set; }

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

            Market = new Market()
            {
                Payments = new MarketPayments(logger(typeof(MarketPayments)), client),
                Refunds = new MarketRefunds(logger(typeof(MarketRefunds)), client),
                PreAuths = new MarketPreAuths(logger(typeof(MarketPreAuths)), client),
                Transactions = new MarketTransactions(logger(typeof(MarketTransactions)), client),
                Collections = new MarketCollections(logger(typeof(MarketCollections)), client),
                Merchants = new MarketMerchants(logger(typeof(MarketMerchants)), client)
            };

            Merchants = new Merchants(logger(typeof(Merchants)), client);

            WebPayments = new WebPayments()
            {
                Payments = new Clients.WebPayments.Payments(logger(typeof(Clients.WebPayments.Payments)), client),
                PreAuths = new Clients.WebPayments.PreAuths(logger(typeof(Clients.WebPayments.PreAuths)), client),
                Transactions = new Clients.WebPayments.Transactions(logger(typeof(Clients.WebPayments.Transactions)), client)
            };

            Consumers = new Consumers(logger(typeof(Clients.Consumer.Consumers)), client);
        }

    }
}
