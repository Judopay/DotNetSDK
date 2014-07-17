using System.Threading.Tasks;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients
{
    /// <summary>
    /// The entity reponsible for providing transactions operations
    /// </summary>
    public interface ITransactions
    {
        /// <summary>
        /// Gets the specified receipt by it's identifier.
        /// </summary>
        /// <param name="receiptId">The receipt identifier.</param>
        /// <returns>The receipt</returns>
        Task<IResult<ITransactionResult>> Get(string receiptId);

        /// <summary>
        /// Gets the receipts that match the request parameters.
        /// </summary>
        /// <param name="transactionType">Type of the transaction.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="sort">The sort.</param>
        /// <returns>The receipts</returns>
        Task<IResult<PaymentReceiptResults>> Get(string transactionType, long? pageSize = null, 
                                                    long? offset = null, string sort = null);
    }
}