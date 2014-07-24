using System.Collections.Generic;
using System.Threading.Tasks;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients.Market
{
    internal class MarketMerchants : JudoPayClient, IMarketMerchants
    {
        private const string Address = "market/merchants";

        public MarketMerchants(ILog logger, IClient client) : base(logger, client)
        {
        }

        public Task<IResult<MerchantSearchResults>> Get(string locatorId)
        {
            var parameters = new Dictionary<string, string>();

            AddParameter(parameters, "locatorId", locatorId);

            return GetInternal<MerchantSearchResults>(Address, parameters);
        }

        public Task<IResult<MerchantSearchResults>> Get(long? pageSize = null,
                                                        long? offset = null,
                                                        TransactionListSorts? sort = null)
        {
            var parameters = new Dictionary<string, string>();

            AddParameter(parameters, "pageSize", pageSize);
            AddParameter(parameters, "offset", offset);
            AddParameter(parameters, "sort", sort);

            return GetInternal<MerchantSearchResults>(Address, parameters);
        }

    }
}
