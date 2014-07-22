using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;

namespace JudoPayDotNet.Clients.Market
{
    internal class MarketTransactions : Transactions
    {
        private const string Address = "market/transactions";

        public MarketTransactions(ILog logger, IClient client)
            : base(logger, client, Address)
        {
        }
    }
}
