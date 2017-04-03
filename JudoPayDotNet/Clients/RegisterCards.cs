using System.Threading.Tasks;
using FluentValidation;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;
using JudoPayDotNet.Models.Validations;

namespace JudoPayDotNet.Clients
{
    internal class RegisterCards : JudoPayClient, IRegisterCards
    {
        private readonly IValidator<CardPaymentModel> RegisterCardValidator = new CardPaymentValidator();

        private const string REGISTER_CARD_ADDRESS = "transactions/registercard";

        public RegisterCards(ILog logger, IClient client, bool deDuplicate = false)
            : base(logger, client)
        {
            _deDuplicateTransactions = deDuplicate;
        }

        public Task<IResult<ITransactionResult>> Create(CardPaymentModel registerCard)
        {
            var validationError = Validate<CardPaymentModel, ITransactionResult>(RegisterCardValidator, registerCard);
            return validationError ?? PostInternal<CardPaymentModel, ITransactionResult>(REGISTER_CARD_ADDRESS, registerCard);
        }
    }
}
