namespace JudoPayDotNet.Clients.Market
{
	/// <summary>
	/// Provides payment operations and visibility into your judo Marketplace
	/// </summary>
    class Market : IMarket
    {
		/// <summary>
		/// Refunds for seller's in your marketplace
		/// </summary>
		public IMarketRefunds Refunds { get; set; }

		/// <summary>
		/// All transactions in your marketplace
		/// </summary>
		/// <value>
		/// The transactions.
		/// </value>
		public ITransactions Transactions { get; set; }

		/// <summary>
		/// The entity responsible for providing collections operations
		/// </summary>
		public IMarketCollections Collections { get; set; }

		/// <summary>
		/// The entity responsible for providing merchants operations
		/// </summary>
		public IMarketMerchants Merchants { get; set; }
    }
}