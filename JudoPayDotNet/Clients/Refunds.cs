using System.Threading.Tasks;
using FluentValidation;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;
using JudoPayDotNet.Models.Validations;

namespace JudoPayDotNet.Clients
{
    internal class Refunds : JudoPayClient, IRefunds
    {
        private readonly IValidator<RefundModel> RefundValidator = new RefundsValidator();

        private const string CREATE_REFUNDS_ADDRESS = "transactions/refunds";

        public Refunds(ILog logger, IClient client)
            : base(logger, client)
        {
        }
        
        public Task<IResult<ITransactionResult>> Create(RefundModel refund)
        {
            var validationError = Validate<RefundModel, ITransactionResult>(RefundValidator, refund);
            return validationError ?? PostInternal<RefundModel, ITransactionResult>(CREATE_REFUNDS_ADDRESS, refund);
        }
    }
}
