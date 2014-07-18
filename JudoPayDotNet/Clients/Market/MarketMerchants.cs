﻿using System.Collections.Generic;
using System.Threading.Tasks;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients.Market
{
    internal class MarketMerchants : JudoPayClient, IMarketMerchants
    {
        private const string ADDRESS = "market/merchants";

        public MarketMerchants(ILog logger, IClient client) : base(logger, client)
        {
        }

        public Task<IResult<MerchantSearchResults>> Get(string locatorId)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            AddParameter(parameters, "locatorId", locatorId);

            return GetInternal<MerchantSearchResults>(ADDRESS, parameters);
        }

        public Task<IResult<MerchantSearchResults>> Get(long? pageSize = null,
                                                        long? offset = null,
                                                        string sort = null)
        {
            var address = ADDRESS;

            Dictionary<string, string> parameters = new Dictionary<string, string>();

            AddParameter(parameters, "pageSize", pageSize);
            AddParameter(parameters, "offset", offset);
            AddParameter(parameters, "sort", sort);

            return GetInternal<MerchantSearchResults>(address, parameters);
        }

    }
}