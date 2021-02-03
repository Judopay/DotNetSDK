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

        
        private const string ResumeThreeDSecureTwoAddress = "resume3ds";

        private const string CompleteThreeDSecureTwoAddress = "complete3ds";


        private readonly IValidator<ThreeDResultModel> _threeDResultValidator = new InlineValidator<ThreeDResultModel>();

        private readonly IValidator<ResumeThreeDSecureModel> _resumeThreeDSecureValidator = new InlineValidator<ResumeThreeDSecureModel>();

        private readonly IValidator<CompleteThreeDSecureModel> _completeThreeDSecureValidator = new InlineValidator<CompleteThreeDSecureModel>();

        public ThreeDs(ILog logger, IClient client)
            : base(logger, client)
        {
        }

        public Task<IResult<PaymentRequiresThreeDSecureModel>> GetThreeDAuthorization(string md)
        {
            var address = $"{GetThreeDAuthorizationAddress}/{md}";

            return GetInternal<PaymentRequiresThreeDSecureModel>(address, new Dictionary<string, string> { { "md", md } });
        }

        /*
        *  To be called to complete a ThreeDSecure One transaction
        */
        public Task<IResult<PaymentReceiptModel>> Complete3DSecure(long receiptId, ThreeDResultModel model)
        {
            var validationError = Validate<ThreeDResultModel, PaymentReceiptModel>(_threeDResultValidator, model);

            var address = $"{CompleteThreeDAuthorizationAddress}/{receiptId}";

            // Do not call the API if validation fail
            return validationError ?? PutInternal<ThreeDResultModel, PaymentReceiptModel>(address, model);
        }



        /*
         *  To be called after device details gathering following the Issuer ACS request for a ThreeDSecure Two transaction
         */
        public Task<IResult<PaymentReceiptModel>> Resume3DSecureTwo(long receiptId, ResumeThreeDSecureModel model)
        {
            var validationError = Validate<ResumeThreeDSecureModel, PaymentReceiptModel>(_resumeThreeDSecureValidator, model);

            var address = $"transactions/{receiptId}/{ResumeThreeDSecureTwoAddress}";

            // Do not call the API if validation fail 
            return validationError ?? PutInternal<ResumeThreeDSecureModel, PaymentReceiptModel>(address, model);
        }

        /*
        *  To be called after the Issuer ACS challenge has been completed for a ThreeDSecure Two transaction
        */
        public Task<IResult<PaymentReceiptModel>> Complete3DSecureTwo(long receiptId, CompleteThreeDSecureModel model)
        {
            var validationError = Validate<CompleteThreeDSecureModel, PaymentReceiptModel>(_completeThreeDSecureValidator, model);

            var address = $"transactions/{receiptId}/{CompleteThreeDSecureTwoAddress}";

            // Do not call the API if validation fail 
            return validationError ?? PutInternal<CompleteThreeDSecureModel, PaymentReceiptModel>(address, model);
        }
    }
}