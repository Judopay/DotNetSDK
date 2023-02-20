using System.Threading.Tasks;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients
{
    /// <summary>
    /// The entity responsible for providing transactions operations
    /// </summary>
    public interface ITransactions
    {
        /// <summary>
        /// Gets the specified receipt by its identifier.
        /// </summary>
        /// <param name="receiptId">The transaction identifier.</param>
        /// <returns>The receipt</returns>
        Task<IResult<ITransactionResult>> Get(long receiptId);

        /// <summary>
        /// Gets the receipts that match the request parameters.
        /// </summary>
        /// <param name="transactionType">Type of the transaction.</param>
        /// <param name="pageSize">The number of records to display per page.</param>
        /// <param name="offset">The zero-based index in the sorted list of records from which the results will
        /// start.</param>
        /// <param name="sort">The sort order (time-descending, time-ascending)</param>
        /// <param name="from">Earliest date used to find transactions (DD/MM/YYYY)</param>
        /// <param name="to">Latest date used to find transactions (DD/MM/YYYY)</param>
        /// <param name="yourPaymentReference">If specified, will only match transactions with this
        /// yourPaymentReference.</param>
        /// <param name="yourConsumerReference">If specified, will only match transactions with this
        /// yourConsumerReference.</param>
        /// <returns>The receipts</returns>
        // ReSharper disable once MethodOverloadWithOptionalParameter
        Task<IResult<PaymentReceiptResults>> Get(TransactionType? transactionType = null,
            long? pageSize = null,
            long? offset = null,
            TransactionListSorts? sort = null,
            string from = null,
            string to = null,
            string yourPaymentReference = null,
            string yourConsumerReference = null);
    }
}