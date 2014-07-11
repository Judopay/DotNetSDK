using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JudoPayDotNet.Client;
using JudoPayDotNet.Clients;
using JudoPayDotNet.Http;
using JudoPayDotNet.Models;

namespace JudoPayDotNet
{
    internal class Transactions : JudoPayClient, ITransactions
    {
        private const string ADDRESS = "";

        public Transactions(IClient client) : base(client, ADDRESS)
        {
        }

        public Task<IResult<PaymentReceiptModel>>  Get(string receiptId)
        {
            var address = string.Format("{0}/{1}", Address, receiptId);
            return GetInternal<PaymentReceiptModel>(address);
        }

        public Task<IResult<PaymentReceiptResults>> Get(string transactionType = null, 
                                                        long? pageSize = null, 
                                                        long? offset = null, 
                                                        string sort = null)
        {
            var address = string.Format("{0}/{1}", Address, transactionType);
            Dictionary<string, string> parameters = new Dictionary<string, string>();
                
            AddParameter(parameters, "pageSize", pageSize);
            AddParameter(parameters, "offset", offset);
            AddParameter(parameters, "sort", sort);

            return GetInternal<PaymentReceiptResults>(address, parameters);
        }
    }
}
