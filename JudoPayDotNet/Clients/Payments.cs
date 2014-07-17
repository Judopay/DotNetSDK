using System.Threading.Tasks;
using FluentValidation;
using JudoPayDotNet.Errors;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;
using JudoPayDotNet.Models.Validations;

namespace JudoPayDotNet.Clients
{
    internal class Payments : JudoPayClient, IPayments
    {
        private const string CREATEADDRESS = "transactions/payments";
        private const string VALIDATEADDRESS = "transactions/payments/validate";
        private readonly IValidator<CardPaymentModel> cardPaymentValidator = new CardPaymentValidator();
        private readonly IValidator<TokenPaymentModel> tokenPaymentValidator = new TokenPaymentValidator();

        public Payments(ILog logger, IClient client)
            : base(logger, client)
        {
        }

        public Task<IResult<ITransactionResult>> Create(CardPaymentModel cardPayment)
        {
            var validationError = Validate<CardPaymentModel, ITransactionResult>(cardPaymentValidator, cardPayment);

            return validationError ?? PostInternal<CardPaymentModel, ITransactionResult>(CREATEADDRESS, cardPayment);
        }

        public Task<IResult<ITransactionResult>> Create(TokenPaymentModel tokenPayment)
        {
            var validationError = Validate<TokenPaymentModel, ITransactionResult>(tokenPaymentValidator, tokenPayment);

            return validationError ?? PostInternal<TokenPaymentModel, ITransactionResult>(CREATEADDRESS, tokenPayment);
        }

        public Task<IResult<JudoApiErrorModel>> Validate(CardPaymentModel cardPayment)
        {
            var validationError = Validate<CardPaymentModel, JudoApiErrorModel>(cardPaymentValidator, cardPayment);

            return validationError ?? PostInternal<CardPaymentModel, JudoApiErrorModel>(VALIDATEADDRESS, cardPayment);
        }

        public Task<IResult<JudoApiErrorModel>> Validate(TokenPaymentModel tokenPayment)
        {
            var validationError = Validate<TokenPaymentModel, JudoApiErrorModel>(tokenPaymentValidator, tokenPayment);

            return validationError ?? PostInternal<TokenPaymentModel, JudoApiErrorModel>(VALIDATEADDRESS, tokenPayment);
        }
    }
}
