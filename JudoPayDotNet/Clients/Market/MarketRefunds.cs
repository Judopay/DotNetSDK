using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;

namespace JudoPayDotNet.Clients.Market
{
    internal class MarketRefunds : BaseRefunds, IMarketRefunds
    {
        private const string CREATE_REFUND_ADDRESS = "market/transactions/refunds";

        public MarketRefunds(ILog logger, IClient client)
            : base(logger, client, CREATE_REFUND_ADDRESS)
        {
        }
    }
}
