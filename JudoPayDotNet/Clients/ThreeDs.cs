using System.Threading.Tasks;
using FluentValidation;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;
using JudoPayDotNet.Models.Internal;
using JudoPayDotNet.Models.Validations;

namespace JudoPayDotNet.Clients
{
    internal class ThreeDs : JudoPayClient, IThreeDs
    {
        private const string CompleteThreeDAuthorizationAddress = "transactions";

        private const string ResumeThreeDSecureTwoAddress = "resume3ds";

        private const string CompleteThreeDSecureTwoAddress = "complete3ds";

        // Keep 3DS2 validation until JR-4706 is released
        private readonly IValidator<ResumeThreeDSecureTwoModel> _resumeThreeDSecureValidator = new ResumeThreeDSecureTwoValidator();

        private readonly IValidator<CompleteThreeDSecureTwoModel> _completeThreeDSecureValidator = new CompleteThreeDSecureTwoValidator();

        public ThreeDs(ILog logger, IClient client)
            : base(logger, client)
        {
        }

        /*
        *  To be called to complete a ThreeDSecure One transaction
        */
        public Task<IResult<PaymentReceiptModel>> Complete3DSecure(long receiptId, ThreeDResultModel model)
        {
            var address = $"{CompleteThreeDAuthorizationAddress}/{receiptId}";

            // Do not call the API if validation fail
            return PutInternal<ThreeDResultModel, PaymentReceiptModel>(address, model);
        }

        /*
         *  To be called after device details gathering following the Issuer ACS request for a ThreeDSecure Two transaction
         */
        public Task<IResult<ITransactionResult>> Resume3DSecureTwo(long receiptId, ResumeThreeDSecureTwoModel model)
        {

            // Validate in DotNetSDK until JR-4706 is released
            var validationError = Validate<ResumeThreeDSecureTwoModel, ITransactionResult>(_resumeThreeDSecureValidator, model);

            var address = $"transactions/{receiptId}/{ResumeThreeDSecureTwoAddress}";

            // Convert SDK model to internal model
            var internalResumeThreeDSecureTwoModel = InternalResumeThreeDSecureTwoModel.From(model);

            // Do not call the API if validation fail 
            return validationError != null ? Task.FromResult(validationError) :
                PutInternal<InternalResumeThreeDSecureTwoModel, ITransactionResult>(address, internalResumeThreeDSecureTwoModel);
        }

        /*
        *  To be called after the Issuer ACS challenge has been completed for a ThreeDSecure Two transaction
        */
        public Task<IResult<PaymentReceiptModel>> Complete3DSecureTwo(long receiptId, CompleteThreeDSecureTwoModel model)
        {
            // Validate in DotNetSDK until JR-4706 is released
            var validationError = Validate<CompleteThreeDSecureTwoModel, PaymentReceiptModel>(_completeThreeDSecureValidator, model);

            var address = $"transactions/{receiptId}/{CompleteThreeDSecureTwoAddress}";

            // Convert SDK model to internal model
            var internalCompleteThreeDSecureTwoModel = InternalCompleteThreeDSecureTwoModel.From(model);

            // Do not call the API if validation fail 
            return validationError != null ? Task.FromResult(validationError) :
                PutInternal<InternalCompleteThreeDSecureTwoModel, PaymentReceiptModel>(address, internalCompleteThreeDSecureTwoModel);
        }
    }
}