using JudoPayDotNet.Clients;
using JudoPayDotNet.Clients.Consumer;
using JudoPayDotNet.Clients.Market;
using JudoPayDotNet.Clients.Merchant;
using JudoPayDotNet.Clients.WebPayments;
using IPayments = JudoPayDotNet.Clients.IPayments;
using IPreAuths = JudoPayDotNet.Clients.IPreAuths;
using ITransactions = JudoPayDotNet.Clients.ITransactions;

namespace JudoPayDotNet
{
    /// <summary>
    /// Judo payments sdk client
    /// </summary>
    public interface IJudoPayments
    {
        /// <summary>
        /// Gets or sets the market operations.
        /// </summary>
        /// <value>
        /// The market.
        /// </value>
        IMarket Market { get; set; }

        /// <summary>
        /// Gets or sets the merchant operations.
        /// </summary>
        /// <value>
        /// The merchants.
        /// </value>
        IMerchants Merchants { get; set; }

        /// <summary>
        /// Gets or sets the web payments operations.
        /// </summary>
        /// <value>
        /// The web payments.
        /// </value>
        IWebPayments WebPayments { get; set; }

        /// <summary>
        /// Gets or sets the consumers operations.
        /// </summary>
        /// <value>
        /// The consumers.
        /// </value>
        IConsumers Consumers { get; set; }

        /// <summary>
        /// The entity reponsible for providing payments operations
        /// </summary>
        IPayments Payments { get; set; }

        /// <summary>
        /// The entity reponsible for providing refunds operations
        /// </summary>
        IRefunds Refunds { get; set; }

        /// <summary>
        /// The entity reponsible for providing pre authorizations operations
        /// </summary>
        IPreAuths PreAuths { get; set; }

        /// <summary>
        /// The entity reponsible for providing transactions operations
        /// </summary>
        /// <value>
        /// The transactions.
        /// </value>
        ITransactions Transactions { get; set; }

        /// <summary>
        /// The entity reponsible for providing collections operations
        /// </summary>
        ICollections Collections { get; set; }

        /// <summary>
        /// The entity reponsible for providing threeD authorization operations
        /// </summary>
        IThreeDs ThreeDs { get; set; }
    }
}