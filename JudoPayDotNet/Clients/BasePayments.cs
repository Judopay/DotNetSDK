using System.Threading.Tasks;
using FluentValidation;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;
using JudoPayDotNet.Models.Validations;

namespace JudoPayDotNet.Clients
{
    internal abstract class BasePayments : JudoPayClient
    {

        protected readonly IValidator<CardPaymentModel> CardPaymentValidator = new CardPaymentValidator();
        protected readonly IValidator<TokenPaymentModel> TokenPaymentValidator = new TokenPaymentValidator();

        protected readonly string CreateAddress;

        protected BasePayments(ILog logger, IClient client, string createAddress) : base(logger, client)
        {
            CreateAddress = createAddress;
        }

        public Task<IResult<ITransactionResult>> Create(CardPaymentModel cardPayment)
        {
            var validationError = Validate<CardPaymentModel, ITransactionResult>(CardPaymentValidator, cardPayment);

            return validationError ?? PostInternal<CardPaymentModel, ITransactionResult>(CreateAddress, cardPayment);
        }

        public Task<IResult<ITransactionResult>> Create(TokenPaymentModel tokenPayment)
        {
            var validationError = Validate<TokenPaymentModel, ITransactionResult>(TokenPaymentValidator, tokenPayment);

            return validationError ?? PostInternal<TokenPaymentModel, ITransactionResult>(CreateAddress, tokenPayment);
        }
    }
}
