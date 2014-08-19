using System.Threading.Tasks;
using FluentValidation;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;
using JudoPayDotNet.Models.Validations;

namespace JudoPayDotNet.Clients.WebPayments
{
	internal abstract class BasePayments : JudoPayClient
    {
        private const string Baseaddress = "webpayments";
        private readonly IValidator<WebPaymentRequestModel> _webPaymentValidator = new WebPaymentRequestModelValidator();

        protected BasePayments(ILog logger, IClient client) : base(logger, client)
        {
        }

        protected Task<IResult<WebPaymentResponseModel>> Create(WebPaymentRequestModel model, string transactionType)
        {
            var validationError = Validate<WebPaymentRequestModel, WebPaymentResponseModel>(_webPaymentValidator, model);

            var address = string.Format("{0}/{1}", Baseaddress, transactionType);

            return validationError ?? PostInternal<WebPaymentRequestModel, WebPaymentResponseModel>(address, model);
        }

        protected Task<IResult<WebPaymentRequestModel>> Update(WebPaymentRequestModel model, string transactionType)
        {
            var validationError = Validate<WebPaymentRequestModel, WebPaymentRequestModel>(_webPaymentValidator, model);

            var address = string.Format("{0}/{1}", Baseaddress, transactionType);

            return validationError ?? PutInternal<WebPaymentRequestModel, WebPaymentRequestModel>(address, model);
        }
    }
}
