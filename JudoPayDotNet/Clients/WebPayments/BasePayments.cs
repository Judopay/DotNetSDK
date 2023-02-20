using System.Threading.Tasks;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients.WebPayments
{
	internal abstract class BasePayments : JudoPayClient
    {
        private const string Baseaddress = "webpayments";

        protected BasePayments(ILog logger, IClient client) : base(logger, client)
        {
        }

        protected Task<IResult<WebPaymentResponseModel>> Create(WebPaymentRequestModel model, string transactionType)
        {
            var address = $"{Baseaddress}/{transactionType}";

            return PostInternal<WebPaymentRequestModel, WebPaymentResponseModel>(address, model);
        }

        /// <summary>
        /// Cancel the webpayment preauth. Used in conjunction with 3D secure
        /// </summary>
        /// <param name="model">The updated information of webpayment preauth</param>
        /// <returns>The webpayment preauth updated</returns>
        public Task<IResult<CancelWebPaymentResponseModel>> Cancel(string reference)
        {
            var address = $"paymentsession/{reference}/cancel";

            return PutInternal<string, CancelWebPaymentResponseModel>(address, null);
        }
    }
}
