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
            var address = $"{_usedAddress}/{receiptId}";
            return GetInternal<ITransactionResult>(address);
        }

        // ReSharper disable once MethodOverloadWithOptionalParameter
        public Task<IResult<PaymentReceiptResults>> Get(TransactionType? transactionType = null, 
                                                        long? pageSize = null,
                                                        long? offset = null, 
                                                        TransactionListSorts? sort = null,
                                                        string from = null,
                                                        string to = null,
                                                        string yourPaymentReference = null,
                                                        string yourConsumerReference = null)
        {
            var address = _usedAddress;

            if (transactionType.HasValue)
            {
                address = $"{_usedAddress}/{transactionType.ToString().ToLower() + "s"}";
            }

            var parameters = new Dictionary<string, string>();
                
            AddParameter(parameters, "pageSize", pageSize);
            AddParameter(parameters, "offset", offset);
            AddParameter(parameters, "sort", sort);
            AddParameter(parameters, "from", from);
            AddParameter(parameters, "to", to);
            AddParameter(parameters, "yourPaymentReference", yourPaymentReference);
            AddParameter(parameters, "yourConsumerReference", yourConsumerReference);


            return GetInternal<PaymentReceiptResults>(address, parameters);
        }
    }
}
