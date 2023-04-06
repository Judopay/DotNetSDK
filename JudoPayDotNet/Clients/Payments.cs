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
        private readonly IValidator<PKPaymentModel> PKPaymentValidator = new PKPaymentValidator();
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

        public Task<IResult<ITransactionResult>> Create(PKPaymentModel pkPayment)
        {
            var validationError = Validate<PKPaymentModel, ITransactionResult>(PKPaymentValidator, pkPayment);
            return validationError != null ? Task.FromResult(validationError) :
                PostInternal<PKPaymentModel, ITransactionResult>(CREATE_ADDRESS, pkPayment);
        }
    }
}