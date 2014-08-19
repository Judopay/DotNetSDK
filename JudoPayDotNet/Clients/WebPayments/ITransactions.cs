using System.Threading.Tasks;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients.WebPayments
{
    /// <summary>
    /// This entity allows you to fetch details of an individual webpayment (either by receipt id or reference)
    /// </summary>
    public interface ITransactions
    {
        /// <summary>
        /// Gets a webpayment transaction by it's reference.
        /// </summary>
        /// <param name="reference">The reference.</param>
        /// <returns>The webpayment transaction</returns>
        Task<IResult<WebPaymentRequestModel>> Get(string reference);

        /// <summary>
        /// Gets a webpayment transaction by transaction identifier (ReceiptId).
        /// </summary>
        /// <param name="receiptId">The transaction identifier.</param>
        /// <returns>The webpayment transaction</returns>
        Task<IResult<WebPaymentRequestModel>> GetByReceipt(string receiptId);
    }
}