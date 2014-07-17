using System.Threading.Tasks;
using FluentValidation;
using JudoPayDotNet.Errors;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;
using JudoPayDotNet.Models.Validations;

namespace JudoPayDotNet.Clients
{
    internal class Refunds : JudoPayClient, IRefunds
    {
        private const string CREATEREFUNDSADDRESS = "transactions/refunds";
        private const string VALIDATEREFUNDSADDRESS = "transactions/refunds/validate";
        private readonly IValidator<RefundModel> refundValidator = new RefundsValidator();

        public Refunds(ILog logger, IClient client)
            : base(logger, client)
        {
        }

        public Task<IResult<ITransactionResult>> Create(RefundModel refund)
        {
            var validationError = Validate<RefundModel, ITransactionResult>(refundValidator, refund);

            return validationError ?? PostInternal<RefundModel, ITransactionResult>(CREATEREFUNDSADDRESS, refund);
        }

        public Task<IResult<JudoApiErrorModel>> Validate(RefundModel refund)
        {
            var validationError = Validate<RefundModel, JudoApiErrorModel>(refundValidator, refund);

            return validationError ?? PostInternal<RefundModel, JudoApiErrorModel>(VALIDATEREFUNDSADDRESS, refund);
        }
    }
}
