using System.Threading.Tasks;
using FluentValidation;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;
using JudoPayDotNet.Models.Validations;

namespace JudoPayDotNet.Clients
{
    internal class Payments : JudoPayClient, IPayments
    {
        // Keep PKPaymentValidator as FieldErrors not returned for invalid pkPayment.token attributes
        private readonly IValidator<ApplePayPaymentModel> PKPaymentValidator = new PKPaymentValidator();
        // Other validation is done in the PartnerApi to ensure error codes are up to date

        private const string CREATE_ADDRESS = "transactions/payments";

        public Payments(ILog logger, IClient client, bool deDuplicate = false) : base(logger, client)
        {
            _deDuplicateTransactions = deDuplicate;
        }

        public Task<IResult<ITransactionResult>> Create(CardPaymentModel cardPayment)
        {
            return PostInternal<CardPaymentModel, ITransactionResult>(CREATE_ADDRESS, cardPayment);
        }

        public Task<IResult<ITransactionResult>> Create(TokenPaymentModel tokenPayment)
        {
            return PostInternal<TokenPaymentModel, ITransactionResult>(CREATE_ADDRESS, tokenPayment);
        }

        public Task<IResult<ITransactionResult>> Create(ApplePayPaymentModel pkPayment)
        {
            var validationError = Validate<ApplePayPaymentModel, ITransactionResult>(PKPaymentValidator, pkPayment);
            return validationError != null ? Task.FromResult(validationError) :
                PostInternal<ApplePayPaymentModel, ITransactionResult>(CREATE_ADDRESS, pkPayment);
        }

        public Task<IResult<ITransactionResult>> Create(GooglePayPaymentModel googlePayPayment)
        {
            return PostInternal<GooglePayPaymentModel, ITransactionResult>(CREATE_ADDRESS, googlePayPayment);
        }
    }
}