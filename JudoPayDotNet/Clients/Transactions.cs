using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JudoPayDotNet.Client;
using JudoPayDotNet.Clients;
using JudoPayDotNet.Models;

namespace JudoPayDotNet
{
    internal class Transactions : JudoPayClient, ITransactions
    {
        private const string ADDRESS = "";

        public Transactions(IClient client) : base(client, ADDRESS)
        {
        }

        private void AddParameter(Dictionary<string, string> parameters, string key, object value)
        {
            string stringValue = value == null ? String.Empty: value.ToString();

            if (!string.IsNullOrEmpty(stringValue) && !parameters.ContainsKey(key))
            {
                parameters.Add(key, stringValue);
            }
        }

        private async Task<IResult<T>> DoGet<T>(string address, 
                                                 Dictionary<string, string> parameters = null) where T : class
        {
            T result = null;

            var response = await Client.Get<T>(address, parameters).ConfigureAwait(false);

            if (!response.ErrorResponse)
            {
                result = response.ResponseBodyObject;
            }

            return new Result<T>(result, response.JudoError);
        }

        public Task<IResult<PaymentReceiptModel>>  Get(string receiptId)
        {
            var address = string.Format("{0}/{1}", Address, receiptId);
            return DoGet<PaymentReceiptModel>(address);
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

            return DoGet<PaymentReceiptResults>(address, parameters);
        }
    }
}
