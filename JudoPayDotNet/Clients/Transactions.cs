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
        private const string ADDRESS = "transactions";

        protected readonly string Address;

        public Transactions(ILog logger, IClient client)
            : this(logger, client, ADDRESS)
        {
        }

        public Transactions(ILog logger, IClient client, string address)
            : base(logger, client)
        {
            Address = address;
        }

        public Task<IResult<ITransactionResult>> Get(string receiptId)
        {
            var address = string.Format("{0}/{1}", Address, receiptId);
            return GetInternal<ITransactionResult>(address);
        }

        public Task<IResult<PaymentReceiptResults>> Get(string transactionType = null, 
                                                        long? pageSize = null, 
                                                        long? offset = null, 
                                                        string sort = null)
        {
            var address = Address;

            if (!String.IsNullOrEmpty(transactionType))
            {
                address = string.Format("{0}/{1}", Address, transactionType);
            }

            Dictionary<string, string> parameters = new Dictionary<string, string>();
                
            AddParameter(parameters, "pageSize", pageSize);
            AddParameter(parameters, "offset", offset);
            AddParameter(parameters, "sort", sort);

            return GetInternal<PaymentReceiptResults>(address, parameters);
        }
    }
}
