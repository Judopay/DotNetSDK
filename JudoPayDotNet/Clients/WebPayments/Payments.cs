using System.Threading.Tasks;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients.WebPayments
{
	/// <summary>
	/// Allows you to create a webpayment before passing Judo your customer to complete the payment
	/// </summary>
	internal class Payments : BasePayments, IPayments
    {
        private const string Transactiontype = "payments";

        public Payments(ILog logger, IClient client)
            : base(logger, client)
        {
        }

	    /// <summary>
	    /// Creates the webpayment payment.
	    /// </summary>
	    /// <param name="model">The webpayment payment.</param>
	    /// <returns>The information required to finalize the webpayment payment</returns>
	    public Task<IResult<WebPaymentResponseModel>> Create(WebPaymentRequestModel model)
        {
            return Create(model, Transactiontype);
        }

	    /// <summary>
		/// Updates the webpayment payment.  Used in conjunction with 3D secure
	    /// </summary>
	    /// <param name="model">The updated information of webpayment payment</param>
	    /// <returns>The webpayment payment updated</returns>
	    public Task<IResult<WebPaymentRequestModel>> Update(WebPaymentRequestModel model)
        {
            return Update(model, Transactiontype);
        }
    }
}
