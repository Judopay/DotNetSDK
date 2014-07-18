using System.Threading.Tasks;
using FluentValidation;
using JudoPayDotNet.Errors;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;
using JudoPayDotNet.Models.Validations;

namespace JudoPayDotNet.Clients
{
    internal class Payments : BasePayments, IPayments
    {
        private const string CREATEADDRESS = "transactions/payments";
        private const string VALIDATEADDRESS = "transactions/payments/validate";
        private readonly IValidator<CardPaymentModel> cardPaymentValidator = new CardPaymentValidator();
        private readonly IValidator<TokenPaymentModel> tokenPaymentValidator = new TokenPaymentValidator();

        protected readonly string ValidateAddress;

        public Payments(ILog logger, IClient client)
            : this(logger, client, CREATEADDRESS, VALIDATEADDRESS)
        {
        }

        public Payments(ILog logger, IClient client, string createAddress, string validateAddress) : base(logger, client, createAddress)
        {
            ValidateAddress = validateAddress;
        }

        public Task<IResult<JudoApiErrorModel>> Validate(CardPaymentModel cardPayment)
        {
            var validationError = Validate<CardPaymentModel, JudoApiErrorModel>(cardPaymentValidator, cardPayment);

            return validationError ?? PostInternal<CardPaymentModel, JudoApiErrorModel>(ValidateAddress, cardPayment);
        }

        public Task<IResult<JudoApiErrorModel>> Validate(TokenPaymentModel tokenPayment)
        {
            var validationError = Validate<TokenPaymentModel, JudoApiErrorModel>(tokenPaymentValidator, tokenPayment);

            return validationError ?? PostInternal<TokenPaymentModel, JudoApiErrorModel>(ValidateAddress, tokenPayment);
        }
    }
}
