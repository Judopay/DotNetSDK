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

		/// <summary>
		/// Returns previous transactions for a consumer, paged and optionally filtered by transaction type
		/// </summary>
        public Task<IResult<PaymentReceiptResults>> GetTransactions(string consumerToken, long? pageSize = null,
            long? offset = null, TransactionListSorts? sort = null)
        {
            return base.GetTransactions(consumerToken, pageSize: pageSize, offset: offset, sort: sort);
        }

		/// <summary>
		/// Returns previous payments for a consumer, paged.
		/// </summary>
        public Task<IResult<PaymentReceiptResults>> GetPayments(string consumerToken, long? pageSize = null,
            long? offset = null, TransactionListSorts? sort = null)
        {
            return GetTransactions(consumerToken, "payments", pageSize, offset, sort);
        }

		/// <summary>
		/// Returns previous preauth transactions for a consumer, paged.
		/// </summary>
        public Task<IResult<PaymentReceiptResults>> GetPreAuths(string consumerToken, long? pageSize = null,
            long? offset = null, TransactionListSorts? sort = null)
        {
            return GetTransactions(consumerToken, "preauths", pageSize, offset, sort);
        }

		/// <summary>
		/// Returns previous collection transactions for a consumer, paged.
		/// </summary>
        public Task<IResult<PaymentReceiptResults>> GetCollections(string consumerToken, long? pageSize = null,
            long? offset = null, TransactionListSorts? sort = null)
        {
            return GetTransactions(consumerToken, "collections", pageSize, offset, sort);
        }

		/// <summary>
		/// Returns previous refunds transactions for a consumer, paged.
		/// </summary>
        public Task<IResult<PaymentReceiptResults>> GetRefunds(string consumerToken, long? pageSize = null,
            long? offset = null, TransactionListSorts? sort = null)
        {
            return GetTransactions(consumerToken, "refunds", pageSize, offset, sort);
        }
    }
}