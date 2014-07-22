using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients.Consumer
{
    public interface IConsumers
    {
        Task<IResult<PaymentReceiptResults>> GetTransactions(string consumerToken, long? pageSize = null,
            long? offset = null, string sort = null);

        Task<IResult<PaymentReceiptResults>> GetPayments(string consumerToken, long? pageSize = null,
            long? offset = null, string sort = null);

        Task<IResult<PaymentReceiptResults>> GetPreAuths(string consumerToken, long? pageSize = null,
            long? offset = null, string sort = null);

        Task<IResult<PaymentReceiptResults>> GetCollections(string consumerToken, long? pageSize = null,
            long? offset = null, string sort = null);

        Task<IResult<PaymentReceiptResults>> GetRefunds(string consumerToken, long? pageSize = null,
            long? offset = null, string sort = null);
    }

    internal abstract class BaseConsumers : JudoPayClient
    {
        private const string ADDRESS = "consumers";

        public BaseConsumers(ILog logger, IClient client) : base(logger, client)
        {
        }

        public Task<IResult<PaymentReceiptResults>> GetTransactions(string consumerToken, string transactionType = null,
                                                                    long? pageSize = null, long? offset = null, string sort = null)
        {
            var address = string.Format("{0}/{1}", ADDRESS, consumerToken);

            if (!String.IsNullOrEmpty(transactionType))
            {
                address = String.Format("{0}/{1}", address, transactionType);
            }

            Dictionary<string, string> parameters = new Dictionary<string, string>();

            AddParameter(parameters, "pageSize", pageSize);
            AddParameter(parameters, "offset", offset);
            AddParameter(parameters, "sort", sort);

            return GetInternal<PaymentReceiptResults>(address, parameters);
        }
    }

    internal class Consumers : BaseConsumers, IConsumers
    {
        public Consumers(ILog logger, IClient client) : base(logger, client)
        {
        }

        public Task<IResult<PaymentReceiptResults>> GetTransactions(string consumerToken, long? pageSize = null,
            long? offset = null, string sort = null)
        {
            return base.GetTransactions(consumerToken, pageSize: pageSize, offset: offset, sort: sort);
        }

        public Task<IResult<PaymentReceiptResults>> GetPayments(string consumerToken, long? pageSize = null,
            long? offset = null, string sort = null)
        {
            return base.GetTransactions(consumerToken, "payments", pageSize, offset, sort);
        }

        public Task<IResult<PaymentReceiptResults>> GetPreAuths(string consumerToken, long? pageSize = null,
            long? offset = null, string sort = null)
        {
            return base.GetTransactions(consumerToken, "preauths", pageSize, offset, sort);
        }

        public Task<IResult<PaymentReceiptResults>> GetCollections(string consumerToken, long? pageSize = null,
            long? offset = null, string sort = null)
        {
            return base.GetTransactions(consumerToken, "collections", pageSize, offset, sort);
        }

        public Task<IResult<PaymentReceiptResults>> GetRefunds(string consumerToken, long? pageSize = null,
            long? offset = null, string sort = null)
        {
            return base.GetTransactions(consumerToken, "refunds", pageSize, offset, sort);
        }
    }
}
