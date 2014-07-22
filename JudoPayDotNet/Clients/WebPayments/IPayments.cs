using System.Threading.Tasks;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients.WebPayments
{
    /// <summary>
    /// The entity responsible for providing webpayments preauth operations
    /// </summary>
    public interface IPayments
    {
        /// <summary>
        /// Creates the webpayment preauth.
        /// </summary>
        /// <param name="model">The webpayment preauth.</param>
        /// <returns>The information required to finalize the webpayment preauth</returns>
        Task<IResult<WebPaymentResponseModel>> Create(WebPaymentRequestModel model);

        /// <summary>
        /// Updates the webpayment preauth.
        /// </summary>
        /// <param name="model">The updated information of webpayment preauth</param>
        /// <returns>The webpayment preauth updated</returns>
        Task<IResult<WebPaymentRequestModel>> Update(WebPaymentRequestModel model);
    }
}