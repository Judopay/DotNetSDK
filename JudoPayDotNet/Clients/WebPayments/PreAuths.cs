using System.Threading.Tasks;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients.WebPayments
{
	/// <summary>
	/// Allows you to create a payment session before passing Judo your customer to complete the payment.
	/// </summary>
	/// <remarks>This is the preauth variation of a webpayment, where the transaction amount is reserved, but not collected</remarks>
    internal class PreAuths : BasePayments, IPreAuths
    {
        private const string Transactiontype = "preauths";

        public PreAuths(ILog logger, IClient client) : base(logger, client)
        {
        }

		/// <summary>
		/// Creates the webpayment preauth.
		/// </summary>
		/// <param name="model">The webpayment preauth.</param>
		/// <returns>The information required to finalize the webpayment preauth</returns>
		public Task<IResult<WebPaymentResponseModel>> Create(WebPaymentRequestModel model)
        {
            return Create(model, Transactiontype);
        }

    }
}
