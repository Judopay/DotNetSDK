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
        private readonly IValidator<CardPaymentModel> CardPaymentValidator = new CardPaymentValidator();

        private readonly IValidator<TokenPaymentModel> TokenPaymentValidator = new TokenPaymentValidator();

        private readonly IValidator<PKPaymentModel> PKPaymentValidator = new PKPaymentValidator();

        private readonly IValidator<AndroidPaymentModel> AndroidPaymentValidator = new AndroidPaymentValidator();

        private readonly IValidator<OneTimePaymentModel> OneTimePaymentValidator = new OneTimePaymentValidator();

        private const string CREATE_ADDRESS = "transactions/payments";

        public Payments(ILog logger, IClient client, bool deDuplicate = false) : base(logger, client)
        {
            _deDuplicateTransactions = deDuplicate;
        }

        public Task<IResult<ITransactionResult>> Create(CardPaymentModel cardPayment)
        {
            var validationError = Validate<CardPaymentModel, ITransactionResult>(CardPaymentValidator, cardPayment);
            return validationError ?? PostInternal<CardPaymentModel, ITransactionResult>(CREATE_ADDRESS, cardPayment);
        }

        public Task<IResult<ITransactionResult>> Create(TokenPaymentModel tokenPayment)
        {
            var validationError = Validate<TokenPaymentModel, ITransactionResult>(TokenPaymentValidator, tokenPayment);
            return validationError ?? PostInternal<TokenPaymentModel, ITransactionResult>(CREATE_ADDRESS, tokenPayment);
        }

        public Task<IResult<ITransactionResult>> Create(PKPaymentModel pkPayment)
        {
            var validationError = Validate<PKPaymentModel, ITransactionResult>(PKPaymentValidator, pkPayment);
            return validationError ?? PostInternal<PKPaymentModel, ITransactionResult>(CREATE_ADDRESS, pkPayment);
        }

        public Task<IResult<ITransactionResult>> Create(AndroidPaymentModel androidPayment)
        {
            var validationError = Validate<AndroidPaymentModel, ITransactionResult>(AndroidPaymentValidator, androidPayment);
            return validationError ?? PostInternal<AndroidPaymentModel, ITransactionResult>(CREATE_ADDRESS, androidPayment);
        }

        public Task<IResult<ITransactionResult>> Create(OneTimePaymentModel oneTimePayment)
        {
            var validationError = Validate<OneTimePaymentModel, ITransactionResult>(OneTimePaymentValidator, oneTimePayment);
            return validationError ?? PostInternal<OneTimePaymentModel, ITransactionResult>(CREATE_ADDRESS, oneTimePayment);
        }
    }
}