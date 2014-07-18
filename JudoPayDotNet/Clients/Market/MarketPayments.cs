using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;

namespace JudoPayDotNet.Clients.Market
{
    internal class MarketPayments : BasePayments, IMarketPayments
    {
        private const string CREATEADDRESS = "market/transactions/payments";

        public MarketPayments(ILog logger, IClient client)
            : base(logger, client, CREATEADDRESS)
        {
        }
    }
}
