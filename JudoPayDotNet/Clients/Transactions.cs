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

        private readonly string _usedAddress;

        public Transactions(ILog logger, IClient client, string usedAddress = BaseAddress)
            : base(logger, client)
        {
            _usedAddress = usedAddress;
        }

        public Task<IResult<ITransactionResult>> Get(string receiptId)
        {
            var address = string.Format("{0}/{1}", _usedAddress, receiptId);
            return GetInternal<ITransactionResult>(address);
        }

        // ReSharper disable once MethodOverloadWithOptionalParameter
        public Task<IResult<PaymentReceiptResults>> Get(string transactionType = null, 
                                                        long? pageSize = null, 
                                                        long? offset = null, 
                                                        string sort = null)
        {
            var address = _usedAddress;

            if (!String.IsNullOrEmpty(transactionType))
            {
                address = string.Format("{0}/{1}", _usedAddress, transactionType);
            }

            var parameters = new Dictionary<string, string>();
                
            AddParameter(parameters, "pageSize", pageSize);
            AddParameter(parameters, "offset", offset);
            AddParameter(parameters, "sort", sort);

            return GetInternal<PaymentReceiptResults>(address, parameters);
        }
    }
}
