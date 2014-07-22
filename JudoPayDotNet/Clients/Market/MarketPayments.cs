using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;

namespace JudoPayDotNet.Clients.Market
{
    internal class MarketPayments : BasePayments, IMarketPayments
    {
        private const string Createaddress = "market/transactions/payments";

        public MarketPayments(ILog logger, IClient client)
            : base(logger, client, Createaddress)
        {
        }
    }
}
