using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients
{
    internal class ThreeDs : JudoPayClient, IThreeDs
    {
        private const string GetThreeDAuthorizationAddress = "transactions/threedauthorisations";
        private const string CompleteThreeDAuthorizationAddress = "transactions";
        private readonly IValidator<ThreeDResultModel> threeDResultValidator = new InlineValidator<ThreeDResultModel>();

        public ThreeDs(ILog logger, IClient client)
            : base(logger, client)
        {
        }

        public Task<IResult<PaymentRequiresThreeDSecureModel>> GetThreeDAuthorization(string md)
        {
            var address = string.Format("{0}/{1}", GetThreeDAuthorizationAddress, md);

            return GetInternal<PaymentRequiresThreeDSecureModel>(address, 
                new Dictionary<string, string>() {{"md", md}});
        }

        public Task<IResult<PaymentReceiptModel>> Complete3DSecure(string receiptId, ThreeDResultModel model)
        {
            var validationError = Validate<ThreeDResultModel, PaymentReceiptModel>(threeDResultValidator, model);

            var address = string.Format("{0}/{1}", CompleteThreeDAuthorizationAddress, receiptId);

            return validationError ?? PutInternal<ThreeDResultModel, PaymentReceiptModel>(address, model);
        }
    }
}