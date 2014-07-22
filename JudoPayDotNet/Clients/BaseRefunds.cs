using System.Threading.Tasks;
using FluentValidation;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;
using JudoPayDotNet.Models.Validations;

namespace JudoPayDotNet.Clients
{
    internal class BaseRefunds : JudoPayClient
    {
        protected readonly IValidator<RefundModel> RefundValidator = new RefundsValidator();
        protected readonly string CreateRefundsAddress;

        public BaseRefunds(ILog logger, IClient client, string createRefundsAddress) : base(logger, client)
        {
            CreateRefundsAddress = createRefundsAddress;
        }

        public Task<IResult<ITransactionResult>> Create(RefundModel refund)
        {
            var validationError = Validate<RefundModel, ITransactionResult>(RefundValidator, refund);

            return validationError ?? PostInternal<RefundModel, ITransactionResult>(CreateRefundsAddress, refund);
        }
    }
}
