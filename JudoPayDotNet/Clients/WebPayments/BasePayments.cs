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

        protected Task<IResult<WebPaymentRequestModel>> Update(WebPaymentRequestModel model, string transactionType)
        {
            var address = $"{Baseaddress}/{transactionType}";

            return PutInternal<WebPaymentRequestModel, WebPaymentRequestModel>(address, model);
        }
    }
}
