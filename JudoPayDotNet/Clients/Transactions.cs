using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients
{
    internal class Transactions : JudoPayClient, ITransactions
    {
        private const string BaseAddress = "transactions";

        protected readonly string UsedAddress;

        public Transactions(ILog logger, IClient client)
            : this(logger, client, BaseAddress)
        {
        }

        public Transactions(ILog logger, IClient client, string usedAddress)
            : base(logger, client)
        {
            UsedAddress = usedAddress;
        }

        public Task<IResult<ITransactionResult>> Get(string receiptId)
        {
            var address = string.Format("{0}/{1}", UsedAddress, receiptId);
            return GetInternal<ITransactionResult>(address);
        }

        // ReSharper disable once MethodOverloadWithOptionalParameter
        public Task<IResult<PaymentReceiptResults>> Get(string transactionType = null, 
                                                        long? pageSize = null, 
                                                        long? offset = null, 
                                                        string sort = null)
        {
            var address = UsedAddress;

            if (!String.IsNullOrEmpty(transactionType))
            {
                address = string.Format("{0}/{1}", UsedAddress, transactionType);
            }

            var parameters = new Dictionary<string, string>();
                
            AddParameter(parameters, "pageSize", pageSize);
            AddParameter(parameters, "offset", offset);
            AddParameter(parameters, "sort", sort);

            return GetInternal<PaymentReceiptResults>(address, parameters);
        }
    }
}
