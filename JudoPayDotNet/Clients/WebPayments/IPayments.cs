using System.Threading.Tasks;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients.WebPayments
{
    /// <summary>
    /// Allows you to create a webpayment before passing Judo your customer to complete the payment.
    /// </summary>
    /// <remarks>This is the payment variation of a webpayment, where the transaction amount is immediately collected</remarks>
    public interface IPayments
    {
        /// <summary>
		/// Creates the webpayment payment.
        /// </summary>
		/// <param name="model">The webpayment payment.</param>
		/// <returns>The information required to finalize the webpayment payment</returns>
        Task<IResult<WebPaymentResponseModel>> Create(WebPaymentRequestModel model);

        /// <summary>
		/// Cancels the specific payment session
        /// </summary>
		/// <param name="model">The Judopay reference for the payment session returned on creation</param>
		/// <returns>The webpayment payment updated</returns>
        Task<IResult<WebPaymentRequestModel>> Cancel(string reference);
    }
}