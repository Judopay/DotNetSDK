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
        protected readonly IValidator<PKPaymentModel> PKPaymentValidator = new PKPaymentValidator();
        protected readonly IValidator<AndroidPaymentModel> AndroidPaymentValidator = new AndroidPaymentValidator();

        private readonly string _createAddress;

        protected BasePayments(ILog logger, IClient client, string createAddress) : base(logger, client)
        {
            _createAddress = createAddress;
        }

        public Task<IResult<ITransactionResult>> Create(CardPaymentModel cardPayment)
        {

            var validationError = Validate<CardPaymentModel, ITransactionResult>(CardPaymentValidator, cardPayment);
            cardPayment.ProvisionSDKVersion();
            return validationError ?? PostInternal<CardPaymentModel, ITransactionResult>(_createAddress, cardPayment);
        }

        public Task<IResult<ITransactionResult>> Create(TokenPaymentModel tokenPayment)
        {
            var validationError = Validate<TokenPaymentModel, ITransactionResult>(TokenPaymentValidator, tokenPayment);
            tokenPayment.ProvisionSDKVersion();
            return validationError ?? PostInternal<TokenPaymentModel, ITransactionResult>(_createAddress, tokenPayment);
        }

        public Task<IResult<ITransactionResult>> Create(PKPaymentModel pkPayment)
        {
            var validationError = Validate<PKPaymentModel, ITransactionResult>(PKPaymentValidator, pkPayment);
            pkPayment.ProvisionSDKVersion();
            return validationError ?? PostInternal<PKPaymentModel, ITransactionResult>(_createAddress, pkPayment);
        }
   
        public Task<IResult<ITransactionResult>> Create(AndroidPaymentModel androidPayment)
        {
            var validationError = Validate<AndroidPaymentModel, ITransactionResult>(AndroidPaymentValidator, androidPayment);
            androidPayment.ProvisionSDKVersion();
            return validationError ?? PostInternal<AndroidPaymentModel, ITransactionResult>(_createAddress, androidPayment);
        }
    }
}
