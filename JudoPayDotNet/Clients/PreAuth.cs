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
        // Keep PKPaymentValidator as FieldErrors not returned for invalid pkPayment.token attributes
        private readonly IValidator<PKPaymentModel> PKPaymentValidator = new PKPaymentValidator();
        // Other validation is done in the PartnerApi to ensure error codes are up to date

        private const string CREATE_PREAUTH_ADDRESS = "transactions/preauths";

        public PreAuths(ILog logger, IClient client, bool deDuplicate = false)
            : base(logger, client)
        {
            _deDuplicateTransactions = deDuplicate;
        }

        public Task<IResult<ITransactionResult>> Create(CardPaymentModel cardPreAuth)
        {
            return PostInternal<CardPaymentModel, ITransactionResult>(CREATE_PREAUTH_ADDRESS, cardPreAuth);
        }

        public Task<IResult<ITransactionResult>> Create(TokenPaymentModel tokenPreAuth)
        {
            return PostInternal<TokenPaymentModel, ITransactionResult>(CREATE_PREAUTH_ADDRESS, tokenPreAuth);
        }

        public Task<IResult<ITransactionResult>> Create(PKPaymentModel pkPreAuth)
        {
            var validationError = Validate<PKPaymentModel, ITransactionResult>(PKPaymentValidator, pkPreAuth);
            return validationError != null ? Task.FromResult(validationError) :
                PostInternal<PKPaymentModel, ITransactionResult>(CREATE_PREAUTH_ADDRESS, pkPreAuth);
        }

        public Task<IResult<ITransactionResult>> Create(OneTimePaymentModel oneTimePayment)
        {
            return PostInternal<OneTimePaymentModel, ITransactionResult>(CREATE_PREAUTH_ADDRESS, oneTimePayment);
        }
    }
}
