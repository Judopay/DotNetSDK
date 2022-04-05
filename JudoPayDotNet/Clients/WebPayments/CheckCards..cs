using System.Threading.Tasks;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients.WebPayments
{
	/// <summary>
	/// Allows you to create a webpayment before passing Judo your customer to complete the payment.
	/// </summary>
	/// <remarks>This is the CheckCard variation of a webpayment, to check if a card is valid.</remarks>
    internal class CheckCards : BasePayments, ICheckCards
    {
        private const string Transactiontype = "checkcard";

        public CheckCards(ILog logger, IClient client) : base(logger, client)
        {
        }

        /// <summary>
        /// Creates the webpayment CheckCard.
        /// </summary>
        /// <param name="model">The webpayment CheckCard.</param>
        /// <returns>The information required to finalize the webpayment CheckCard</returns>
        public Task<IResult<WebPaymentResponseModel>> Create(WebPaymentRequestModel model)
        {
            return Create(model, Transactiontype);
        }
    }
}
