using System.Threading.Tasks;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients.Consumer
{
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
            return GetTransactions(consumerToken, "payments", pageSize, offset, sort);
        }

        public Task<IResult<PaymentReceiptResults>> GetPreAuths(string consumerToken, long? pageSize = null,
            long? offset = null, string sort = null)
        {
            return GetTransactions(consumerToken, "preauths", pageSize, offset, sort);
        }

        public Task<IResult<PaymentReceiptResults>> GetCollections(string consumerToken, long? pageSize = null,
            long? offset = null, string sort = null)
        {
            return GetTransactions(consumerToken, "collections", pageSize, offset, sort);
        }

        public Task<IResult<PaymentReceiptResults>> GetRefunds(string consumerToken, long? pageSize = null,
            long? offset = null, string sort = null)
        {
            return GetTransactions(consumerToken, "refunds", pageSize, offset, sort);
        }
    }
}