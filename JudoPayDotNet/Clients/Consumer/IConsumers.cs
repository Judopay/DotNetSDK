using System.Threading.Tasks;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients.Consumer
{
    /// <summary>
    /// The entity responsible for providing consumer operations
    /// </summary>
    public interface IConsumers
    {
        /// <summary>
        /// Gets all types of transactions of the consumer.
        /// </summary>
        /// <param name="consumerToken">The consumer token.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="sort">The sort order.</param>
        /// <returns>Consumer transactions of all types as requested</returns>
        Task<IResult<PaymentReceiptResults>> GetTransactions(string consumerToken, long? pageSize = null,
            long? offset = null, string sort = null);

        /// <summary>
        /// Gets payment transactions of the consumer.
        /// </summary>
        /// <param name="consumerToken">The consumer token.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="sort">The sort order.</param>
        /// <returns>Consumer payment transactions as requested</returns>
        Task<IResult<PaymentReceiptResults>> GetPayments(string consumerToken, long? pageSize = null,
            long? offset = null, string sort = null);

        /// <summary>
        /// Gets preauths transactions of the consumer.
        /// </summary>
        /// <param name="consumerToken">The consumer token.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="sort">The sort order.</param>
        /// <returns>Consumer preauths transactions as requested</returns>
        Task<IResult<PaymentReceiptResults>> GetPreAuths(string consumerToken, long? pageSize = null,
            long? offset = null, string sort = null);

        /// <summary>
        /// Gets collections transactions of the consumer.
        /// </summary>
        /// <param name="consumerToken">The consumer token.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="sort">The sort order.</param>
        /// <returns>Consumer collections transactions as requested</returns>
        Task<IResult<PaymentReceiptResults>> GetCollections(string consumerToken, long? pageSize = null,
            long? offset = null, string sort = null);

        /// <summary>
        /// Gets refunds transactions of the consumer.
        /// </summary>
        /// <param name="consumerToken">The consumer token.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="sort">The sort order.</param>
        /// <returns>Consumer refunds transactions as requested</returns>
        Task<IResult<PaymentReceiptResults>> GetRefunds(string consumerToken, long? pageSize = null,
            long? offset = null, string sort = null);
    }
}