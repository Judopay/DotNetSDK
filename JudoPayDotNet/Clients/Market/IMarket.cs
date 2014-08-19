namespace JudoPayDotNet.Clients.Market
{
    // ReSharper disable UnusedMember.Global
    // ReSharper disable UnusedMemberInSuper.Global

	/// <summary>
	/// Provides payment operations and visibility into your judo Marketplace
	/// </summary>
    public interface IMarket
    {
        /// <summary>
        /// Refunds for seller's in your marketplace
        /// </summary>
        IMarketRefunds Refunds { get; set; }

        /// <summary>
        /// All transactions in your marketplace
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
    // ReSharper restore UnusedMemberInSuper.Global
    // ReSharper restore UnusedMember.Global
}