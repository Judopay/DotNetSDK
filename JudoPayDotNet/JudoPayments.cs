using System;
using JudoPayDotNet.Clients;
using JudoPayDotNet.Clients.Consumer;
using JudoPayDotNet.Clients.Market;
using JudoPayDotNet.Clients.WebPayments;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using IPayments = JudoPayDotNet.Clients.IPayments;
using IPreAuths = JudoPayDotNet.Clients.IPreAuths;
using ITransactions = JudoPayDotNet.Clients.ITransactions;
using Payments = JudoPayDotNet.Clients.Payments;
using PreAuths = JudoPayDotNet.Clients.PreAuths;
using Transactions = JudoPayDotNet.Clients.Transactions;

namespace JudoPayDotNet
{
    public class JudoPayments : IJudoPayments
    {
        public IMarket Market { get; set; }

        public IWebPayments WebPayments { get; set; }

        public IConsumers Consumers { get; set; }

        public IPayments Payments { get; set; }
        public IRefunds Refunds { get; set; }
        public IPreAuths PreAuths { get; set; }
        public ITransactions Transactions { get; set; }
        public ICollections Collections { get; set; }
        public IThreeDs ThreeDs { get; set; }

        public JudoPayments(Func<Type, ILog> logger, IClient client)
        {
            Payments = new Payments(logger(typeof(Payments)), client);
            Refunds = new Refunds(logger(typeof(Refunds)), client);
            PreAuths = new PreAuths(logger(typeof(PreAuths)), client);
            Transactions = new Transactions(logger(typeof(Transactions)), client);
            Collections = new Collections(logger(typeof(Collections)), client);
            ThreeDs = new ThreeDs(logger(typeof(ThreeDs)), client);

            Market = new Market
            {
                Refunds = new MarketRefunds(logger(typeof(MarketRefunds)), client),
                Transactions = new MarketTransactions(logger(typeof(MarketTransactions)), client),
                Collections = new MarketCollections(logger(typeof(MarketCollections)), client),
                Merchants = new MarketMerchants(logger(typeof(MarketMerchants)), client)
            };

            WebPayments = new WebPayments
            {
                Payments = new Clients.WebPayments.Payments(logger(typeof(Clients.WebPayments.Payments)), client),
                PreAuths = new Clients.WebPayments.PreAuths(logger(typeof(Clients.WebPayments.PreAuths)), client),
                Transactions = new Clients.WebPayments.Transactions(logger(typeof(Clients.WebPayments.Transactions)), client)
            };

            Consumers = new Consumers(logger(typeof(Consumers)), client);
        }

    }
}
