using System.Threading.Tasks;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients.WebPayments
{
    /// <summary>
    /// The entity responsible for providing the retrieval of webpayments transactions operations
    /// </summary>
    public interface ITransactions
    {
        /// <summary>
        /// Gets a webpayment transaction by reference.
        /// </summary>
        /// <param name="reference">The reference.</param>
        /// <returns>The webpayment transaction</returns>
        Task<IResult<WebPaymentRequestModel>> Get(string reference);

        /// <summary>
        /// Gets a webpayment transaction by receipt identifier.
        /// </summary>
        /// <param name="receiptId">The receipt identifier.</param>
        /// <returns>The webpayment transaction</returns>
        Task<IResult<WebPaymentRequestModel>> GetByReceipt(string receiptId);
    }
}