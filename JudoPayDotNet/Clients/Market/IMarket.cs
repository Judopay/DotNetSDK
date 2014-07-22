namespace JudoPayDotNet.Clients.Market
{
    /// <summary>
    /// Provides judo payment operations for markets
    /// </summary>
    public interface IMarket
    {
        /// <summary>
        /// The entity rseponsible for providing payments operations
        /// </summary>
        IMarketPayments Payments { get; set; }

        /// <summary>
        /// The entity responsible for providing refunds operations
        /// </summary>
        IMarketRefunds Refunds { get; set; }

        /// <summary>
        /// The entity responsible for providing pre authorizations operations
        /// </summary>
        IMarketPreAuths PreAuths { get; set; }

        /// <summary>
        /// The entity responsible for providing transactions operations
        /// </summary>
        /// <value>
        /// The transactions.
        /// </value>
        ITransactions Transactions { get; set; }

        /// <summary>
        /// The entity responsible for providing collections operations
        /// </summary>
        IMarketCollections Collections { get; set; }

        /// <summary>
        /// The entity responsible for providing merchants operations
        /// </summary>
        IMarketMerchants Merchants { get; set; }
    }
}