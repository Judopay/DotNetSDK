using System.Threading.Tasks;
using FluentValidation;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;
using JudoPayDotNet.Models.Validations;

namespace JudoPayDotNet.Clients
{
    internal class PreAuths : JudoPayClient, IPreAuths
    {
        private readonly IValidator<CardPaymentModel> CardPaymentValidator = new CardPaymentValidator();

        private readonly IValidator<TokenPaymentModel> TokenPaymentValidator = new TokenPaymentValidator();

        private readonly IValidator<PKPaymentModel> PKPaymentValidator = new PKPaymentValidator();

        private readonly IValidator<OneTimePaymentModel> OneTimePaymentValidator = new OneTimePaymentValidator();

        private const string CREATE_PREAUTH_ADDRESS = "transactions/preauths";

        public PreAuths(ILog logger, IClient client, bool deDuplicate = false)
            : base(logger, client)
        {
            _deDuplicateTransactions = deDuplicate;
        }

        public Task<IResult<ITransactionResult>> Create(CardPaymentModel cardPreAuth)
        {
            var validationError = Validate<CardPaymentModel, ITransactionResult>(CardPaymentValidator, cardPreAuth);
            return validationError ?? PostInternal<CardPaymentModel, ITransactionResult>(CREATE_PREAUTH_ADDRESS, cardPreAuth);
        }

        public Task<IResult<ITransactionResult>> Create(TokenPaymentModel tokenPreAuth)
        {
            var validationError = Validate<TokenPaymentModel, ITransactionResult>(TokenPaymentValidator, tokenPreAuth);
            return validationError ?? PostInternal<TokenPaymentModel, ITransactionResult>(CREATE_PREAUTH_ADDRESS, tokenPreAuth);
        }

        public Task<IResult<ITransactionResult>> Create(PKPaymentModel pkPreAuth)
        {
            var validationError = Validate<PKPaymentModel, ITransactionResult>(PKPaymentValidator, pkPreAuth);
            return validationError ?? PostInternal<PKPaymentModel, ITransactionResult>(CREATE_PREAUTH_ADDRESS, pkPreAuth);
        }

        public Task<IResult<ITransactionResult>> Create(OneTimePaymentModel oneTimePayment)
        {
            var validationError = Validate<OneTimePaymentModel, ITransactionResult>(OneTimePaymentValidator, oneTimePayment);
            return validationError ?? PostInternal<OneTimePaymentModel, ITransactionResult>(CREATE_PREAUTH_ADDRESS, oneTimePayment);
        }
    }
}
