using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JudoPayDotNet.Clients;
using JudoPayDotNet.Http;
using JudoPayDotNet.Models;

namespace JudoPayDotNet
{
    internal class Transactions : JudoPayClient, ITransactions
    {
        private const string ADDRESS = "transactions";

        public Transactions(IClient client) : base(client)
        {
        }

        public Task<IResult<PaymentReceiptModel>>  Get(string receiptId)
        {
            var address = string.Format("{0}/{1}", ADDRESS, receiptId);
            return GetInternal<PaymentReceiptModel>(address);
        }

        public Task<IResult<PaymentReceiptResults>> Get(string transactionType = null, 
                                                        long? pageSize = null, 
                                                        long? offset = null, 
                                                        string sort = null)
        {
            var address = ADDRESS;

            if (!String.IsNullOrEmpty(transactionType))
            {
                address = string.Format("{0}/{1}", ADDRESS, transactionType);
            }

            Dictionary<string, string> parameters = new Dictionary<string, string>();
                
            AddParameter(parameters, "pageSize", pageSize);
            AddParameter(parameters, "offset", offset);
            AddParameter(parameters, "sort", sort);

            return GetInternal<PaymentReceiptResults>(address, parameters);
        }
    }
}
