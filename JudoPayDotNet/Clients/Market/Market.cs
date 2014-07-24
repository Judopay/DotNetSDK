namespace JudoPayDotNet.Clients.Market
{
    class Market : IMarket
    {
        public IMarketRefunds Refunds { get; set; }

        public ITransactions Transactions { get; set; }

        public IMarketCollections Collections { get; set; }

        public IMarketMerchants Merchants { get; set; }
    }
}