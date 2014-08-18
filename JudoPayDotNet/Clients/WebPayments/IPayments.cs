using System.Threading.Tasks;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients.WebPayments
{
    /// <summary>
    /// The entity responsible for providing webpayments payment operations
    /// </summary>
    public interface IPayments
    {
        /// <summary>
		/// Creates the webpayment payment.
        /// </summary>
		/// <param name="model">The webpayment payment.</param>
		/// <returns>The information required to finalize the webpayment payment</returns>
        Task<IResult<WebPaymentResponseModel>> Create(WebPaymentRequestModel model);

        /// <summary>
		/// Updates the webpayment payment.
        /// </summary>
		/// <param name="model">The updated information of webpayment payment</param>
		/// <returns>The webpayment payment updated</returns>
        Task<IResult<WebPaymentRequestModel>> Update(WebPaymentRequestModel model);
    }
}