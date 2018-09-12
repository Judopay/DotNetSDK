using System;
using JudoPayDotNet.Clients;
using JudoPayDotNet.Clients.Consumer;
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
	/// <summary>
	/// The JudoPay API client, the main entry point for the SDK
	/// </summary>
    public class JudoPayApi : IJudoPayApi
    {
        public IWebPayments WebPayments { get; set; }

        public IConsumers Consumers { get; set; }

        public IPayments Payments { get; set; }
        public IRefunds Refunds { get; set; }
        public IPreAuths PreAuths { get; set; }
        public ITransactions Transactions { get; set; }
        public ICollections Collections { get; set; }
        public IThreeDs ThreeDs { get; set; }

        [Obsolete("Register card is currently doing an authorisation to check that the card is valid. We now have two new methods. One is the save card, which doesnt check that a card is valid and check card, which checks if a card is valid. Neither do an authorisation")]
        public IRegisterCards RegisterCards { get; set; }
        public ISaveCard SaveCards { get; set; }
        public IVoids Voids { get; set; }
        public Connection Connection { get; }
        public ICheckCard CheckCards { get; set; }

        public JudoPayApi(Func<Type, ILog> logger, IClient client)
        {
            Payments = new Payments(logger(typeof(Payments)), client, true);
            Refunds = new Refunds(logger(typeof(Refunds)), client);
            PreAuths = new PreAuths(logger(typeof(PreAuths)), client, true);
            Transactions = new Transactions(logger(typeof(Transactions)), client);
            Collections = new Collections(logger(typeof(Collections)), client);
            ThreeDs = new ThreeDs(logger(typeof(ThreeDs)), client);
            RegisterCards = new RegisterCards(logger(typeof(RegisterCards)), client, true);
            CheckCards = new CheckCards(logger(typeof(CheckCards)), client, true);
            SaveCards = new SaveCard(logger(typeof(SaveCard)), client);
            Voids = new Voids(logger(typeof(Voids)), client);

            WebPayments = new WebPayments
            {
                Payments = new Clients.WebPayments.Payments(logger(typeof(Clients.WebPayments.Payments)), client),
                PreAuths = new Clients.WebPayments.PreAuths(logger(typeof(Clients.WebPayments.PreAuths)), client),
                Transactions = new Clients.WebPayments.Transactions(logger(typeof(Clients.WebPayments.Transactions)), client)
            };

            Consumers = new Consumers(logger(typeof(Consumers)), client);
            Connection = client.Connection;
        }

    }
}
