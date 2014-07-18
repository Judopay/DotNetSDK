using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;

namespace JudoPayDotNet.Clients.Market
{
    internal class MarketPreAuths : BasePreAuth, IMarketPreAuths
    {
        private const string CREATEPREAUTHADDRESS = "market/transactions/preauths";

        public MarketPreAuths(ILog logger, IClient client)
            : base(logger, client, CREATEPREAUTHADDRESS)
        {
        }
    }
}
