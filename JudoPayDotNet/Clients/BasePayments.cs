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

        protected readonly IValidator<CardPaymentModel> cardPaymentValidator = new CardPaymentValidator();
        protected readonly IValidator<TokenPaymentModel> tokenPaymentValidator = new TokenPaymentValidator();

        protected readonly string CreateAddress;

        protected BasePayments(ILog logger, IClient client, string createAddress) : base(logger, client)
        {
            CreateAddress = createAddress;
        }

        public Task<IResult<ITransactionResult>> Create(CardPaymentModel cardPayment)
        {
            var validationError = Validate<CardPaymentModel, ITransactionResult>(cardPaymentValidator, cardPayment);

            return validationError ?? PostInternal<CardPaymentModel, ITransactionResult>(CreateAddress, cardPayment);
        }

        public Task<IResult<ITransactionResult>> Create(TokenPaymentModel tokenPayment)
        {
            var validationError = Validate<TokenPaymentModel, ITransactionResult>(tokenPaymentValidator, tokenPayment);

            return validationError ?? PostInternal<TokenPaymentModel, ITransactionResult>(CreateAddress, tokenPayment);
        }
    }
}
