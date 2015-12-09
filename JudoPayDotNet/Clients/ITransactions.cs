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
        /// Gets the specified receipt by it's identifier.
        /// </summary>
        /// <param name="receiptId">The transaction identifier.</param>
        /// <returns>The receipt</returns>
        Task<IResult<ITransactionResult>> Get(long receiptId);

        /// <summary>
        /// Gets the receipts that match the request parameters.
        /// </summary>
        /// <param name="transactionType">Type of the transaction.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="sort">The sort.</param>
        /// <returns>The receipts</returns>
        // ReSharper disable once MethodOverloadWithOptionalParameter
        Task<IResult<PaymentReceiptResults>> Get(TransactionType? transactionType = null, long? pageSize = null, 
                                                    long? offset = null, TransactionListSorts? sort = null);
    }
}